using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Model;
using NLog;
using Persistence.Sistema;
using Common.Helper;

namespace Persistence.Maestro
{
    public class MedioDePagoDA
    {
        public MSTt01_medioDePago GetById(int id)
        {
            var obj = new MSTt01_medioDePago();
            using (var ctx = new AsiriContext())
            {
                try
                {
                    obj = ctx.MSTt01_medioDePago.SingleOrDefault(x => x.idMedioDePago == id);
                }
                catch (Exception e)
                {
                    var log = LogManager.GetLogger("fileLogger");
                    log.Error(e, "Excepción en: Consultar un medio de pago por ID.");
                }
            }
            return obj;
        }

        //public List<MSTt01_medioDePago> ListMedioDePagoRapido()
        //{
        //    var list = new List<MSTt01_medioDePago>();
        //    using (var ctx = new AsiriContext())
        //    {
        //        try
        //        {
        //            list = ctx.MSTt01_medioDePago.
        //                Where(x => x.medioDePagoRapido == true && x.activo == true).
        //                Select(x => new MSTt01_medioDePago {
        //                    idMedioDePago = x.idMedioDePago,
        //                    descMedioDePago = x.descMedioDePago,
        //                    photoPath = x.photoPath
        //                }).ToList();
        //        }
        //        catch (Exception e)
        //        {
        //            var log = LogManager.GetLogger("fileLogger");
        //            log.Error(e, "Excepción en: Listar medios de pago rápido.");
        //        }
        //        return list;
        //    }
        //}

        public MSTt01_medioDePago GetMedioDePagoRapido()

        {
            var obj = new MSTt01_medioDePago();
            var parametro = new ParametroDA().GetByCod(CodParam.MedioDePagoRapido);
            if (parametro != null && parametro.idParametro > 0 && parametro.valorNumerico != null)
            {
                try
                {
                    int id = int.Parse(parametro.valorNumerico.RemoveTrailingZeros());
                    obj = new MedioDePagoDA().GetById(id);

                }
                catch (Exception e)
                {
                    var log = LogManager.GetLogger("fileLogger");
                    log.Error(e, "Excepción en: Consultar el medio de pago rápido.");
                }
            }
            return obj;
        }
    }
}
