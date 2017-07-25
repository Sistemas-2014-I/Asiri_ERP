using System;
using System.Collections.Generic;
using System.Linq;
using NLog;
using System.Data.Entity;
using Common;

namespace Persistence.Servicio
{
    public class ServicioDA
    {
        public IEnumerable<PROt04_servicio> List()
        {
            IEnumerable<PROt04_servicio> list = null;
            using (AsiriContext ctx = new AsiriContext())
            {
                try
                {
                    list = ctx.PROt04_servicio.Include(p => p.PROt05_tipoDeServicio).ToList();
                }
                catch (Exception ex)
                {
                    Logger log = LogManager.GetLogger("fileLogger");
                    log.Error(ex, "Error al listar servicios");
                }
            }
            return list;
        }
        public IEnumerable<PROt04_servicio> GetServicesByTypeService(int id)
        {
            IEnumerable<PROt04_servicio> list = null;
            using (AsiriContext ctx = new AsiriContext())
            {
                try
                {
                    list = ctx.PROt04_servicio.Include(p => p.PROt05_tipoDeServicio).Where(p=>p.idTipoDeServicio == id).ToList();
                }
                catch (Exception ex)
                {
                    Logger log = LogManager.GetLogger("fileLogger");
                    log.Error(ex, "Error al listar servicios por tipo de servicios");
                }
            }
            return list;
        }
        
        public PROt04_servicio GetService(long? id)
        {
            PROt04_servicio service;
            using (AsiriContext ctx = new AsiriContext())
            {
                try
                {
                    service = ctx.PROt04_servicio.Include(p => p.PROt05_tipoDeServicio).Include(x => x.RHUt04_especialidad).SingleOrDefault(x => x.idServicio == id);
                }
                catch (Exception)
                {
                    service = null;
                }
            }
            return service;
        }
        public void Add(PROt04_servicio model)
        {
            using (AsiriContext ctx = new AsiriContext())
            {
                try
                {
                    //Añadir la fecha en la que se está registrando el producto
                    model.fecRegistro = DateTime.Now;
                    //Por defecto al registrarse está activo el producto
                    model.activo = true;
                    ctx.PROt04_servicio.Add(model);
                    ctx.SaveChanges();
                }
                catch (Exception ex)
                {
                    Logger log = LogManager.GetLogger("fileLogger");
                    log.Error(ex, "Error al agregar servicio");
                }
            }
        }
        public void Edit(PROt04_servicio model)
        {
            using (AsiriContext ctx = new AsiriContext())
            {
                try
                {
                    //Añadir fecha en la que se está editando
                    model.fecModificacion = DateTime.Now;
                    ctx.Entry(model).State = EntityState.Modified;
                    ctx.SaveChanges();
                }
                catch (Exception ex)
                {
                    Logger log = LogManager.GetLogger("fileLogger");
                    log.Error(ex, "Error al editar el servicio");
                }
            }
        }
        public void ChangeStatus(PROt04_servicio model)
        {
            try
            {
                using (AsiriContext ctx = new AsiriContext())
                {
                    var estado = model.activo;
                    // Inverir el estado
                    model.activo = !estado;
                    if (estado)
                    {
                        model.fecEliminacion = DateTime.Now;
                    }
                    else
                    {
                        model.fecModificacion = DateTime.Now;
                    }
                    ctx.Entry(model).State = EntityState.Modified;
                    ctx.SaveChanges();
                }
            }
            catch (Exception)
            {
                // ignored
            }
        }
    }
}
