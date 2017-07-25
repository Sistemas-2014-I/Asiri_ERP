using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Model;
using NLog;
using System.Data.Entity;
using Common.View.ViewModel;

namespace Persistence.Transaccion
{
    public class NumeracionDA
    {
        public IEnumerable<NumeracionVM> List()
        {
            var list = new List<NumeracionVM>();
            using (var ctx = new AsiriContext())
            {
                try
                {
                    //Corregir. Traer solo lo necesario.
                    list = ctx.TNSt07_numeracion.Select(x => new NumeracionVM()
                    {
                        idNumeracion = x.idNumeracion,
                        serie = x.serie,
                        num_corr_actual = x.num_corr_actual,
                        num_corr_final = x.num_corr_final,
                        num_corr_inicial = x.num_corr_inicial,
                        tipoComprobante = ctx.SNTt04_tipoComprobante.Where(t => t.idTipoComprobante == x.idTipoComprobante)
                                                                    .Select(t => new { t.descTipoComprobante })
                                                                    .FirstOrDefault().descTipoComprobante,
                        sucursal = ctx.MSTt04_sucursal.Where(s => s.idSucursal == x.idSucursal)
                                                        .Select(s => new { s.descSucursal }).FirstOrDefault().descSucursal
                    }).ToList();
                }
                catch (Exception e)
                {
                    var log = LogManager.GetLogger("fileLogger");
                    log.Error(e, "Excepción en: Listar numeración");
                }
            }
            return list;
        }

        public TNSt07_numeracion GetById(int id)
        {
            var obj = new TNSt07_numeracion();
            using (AsiriContext ctx = new AsiriContext())
            {
                try
                {
                    obj = ctx.TNSt07_numeracion.SingleOrDefault(x => x.idNumeracion == id);
                }
                catch (Exception e)
                {
                    var log = LogManager.GetLogger("fileLogger");
                    log.Error(e, "Excepción en: Traer una numeración por ID.");
                }
            }
            return obj;
        }

        public int Add(TNSt07_numeracion obj)
        {
            using (AsiriContext ctx = new AsiriContext())
            {
                using (var tns = ctx.Database.BeginTransaction())
                {
                    try
                    {
                        ctx.TNSt07_numeracion.Add(obj);
                        ctx.SaveChanges();
                        tns.Commit();
                    }
                    catch (Exception e)
                    {
                        var log = LogManager.GetLogger("fileLogger");
                        log.Error(e, "Excepción en: Agregar una numeración.");
                        tns.Rollback();
                    }
                }
            }
            return obj.idNumeracion;
        }

        public bool Edit(TNSt07_numeracion obj)
        {
            var success = false;
            using (AsiriContext ctx = new AsiriContext())
            {
                using (var tns = ctx.Database.BeginTransaction())
                {
                    try
                    {
                        ctx.Entry(obj).State = EntityState.Modified;
                        ctx.SaveChanges();
                        tns.Commit();
                        success = true;
                    }
                    catch (Exception e)
                    {
                        Logger log = LogManager.GetLogger("fileLogger");
                        log.Error(e, "Excepción en: Editar una numeración.");
                        tns.Rollback();
                    }
                }
            }
            return success;
        }
    }
}
