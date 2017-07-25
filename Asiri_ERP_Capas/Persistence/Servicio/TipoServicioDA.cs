using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using NLog;
using Common;

namespace Persistence.Servicio
{
    public class TipoServicioDA
    {
        public IEnumerable<PROt05_tipoDeServicio> List()
        {
            IEnumerable<PROt05_tipoDeServicio> list = null;
            using (AsiriContext ctx = new AsiriContext())
            {
                try
                {
                    list = ctx.PROt05_tipoDeServicio.ToList();
                }
                catch (Exception ex)
                {
                    Logger log = LogManager.GetLogger("fileLogger");
                    log.Error(ex, "Error al tipos de servicios");
                }
            }
            return list;
        }
        public PROt05_tipoDeServicio GetTypeService(long? id)
        {
            using (AsiriContext ctx = new AsiriContext())
            {
                try
                {
                    return ctx.PROt05_tipoDeServicio.Find(id);
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }
        public void Add(PROt05_tipoDeServicio model)
        {
            using (AsiriContext ctx = new AsiriContext())
            {
                try
                {
                    //Añadir la fecha en la que se está registrando
                    model.fecRegistro = DateTime.Now;
                    //Por defecto al registrarse está activo
                    model.activo = true;
                    ctx.PROt05_tipoDeServicio.Add(model);
                    ctx.SaveChanges();
                }
                catch (Exception ex)
                {
                    Logger log = LogManager.GetLogger("fileLogger");
                    log.Error(ex, "Error al un tipo de servicio");
                }
            }
        }
        public void Edit(PROt05_tipoDeServicio model)
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
                    log.Error(ex, "Error al editar el tipo de servicio");
                }
            }
        }
        public void ChangeStatus(PROt05_tipoDeServicio model)
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
                        //Evalúa si es true, se ser así se guardará como eliminación lógica
                        
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
            catch (Exception ex)
            {
            }
        }
    }
}
