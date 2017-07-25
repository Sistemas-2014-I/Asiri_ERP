using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using Common;
using NLog;

namespace Persistence.Sunat
{
    public class ImpuestoDA
    {
        public IEnumerable<SNTt02_impuesto> List()
        {
            IEnumerable<SNTt02_impuesto> list;
            using (AsiriContext ctx = new AsiriContext())
            {
                try
                {
                    list = ctx.SNTt02_impuesto.ToList();
                }
                catch (Exception ex)
                {
                    Logger log = LogManager.GetLogger("fileLogger");
                    log.Error(ex, "Error al listar Impuestos");
                    list = null;
                }
            }
            return list;
        }
        public SNTt02_impuesto GetImpuesto(short? id)
        {
            SNTt02_impuesto product;
            using (AsiriContext ctx = new AsiriContext())
            {
                try
                {
                    // No se hace el include de la tabla moneda porque no se usa (se obtiene del parámetro)
                    product = ctx.SNTt02_impuesto.SingleOrDefault(x => x.idImpto == id);
                }
                catch (Exception)
                {
                    product = null;
                }
            }
            return product;
        }
        public void Add(SNTt02_impuesto model)
        {
            using (AsiriContext ctx = new AsiriContext())
            {
                try
                {
                    //Añadir la fecha en la que se está registrando
                    model.fecRegistro = DateTime.Now;
                    //registrar id del usuario que registró
                    /***********************************************/
                    model.idUsuario = "34";
                    //Por defecto al registrarse está activo
                    model.activo = true;
                    ctx.SNTt02_impuesto.Add(model);
                    ctx.SaveChanges();
                }
                catch (Exception ex)
                {
                    Logger log = LogManager.GetLogger("fileLogger");
                    log.Error(ex, "Error al agregar Impuesto");
                }
            }
        }
        public void Edit(SNTt02_impuesto model)
        {
            using (AsiriContext ctx = new AsiriContext())
            {
                try
                {
                    //Añadir usuario que modifica
                    /******************FALTA UNIR CON LO EL USURIO RE ROLES******************/
                    model.idUsuarioModificar = "34";

                    //Añadir fecha en la que se está editando
                    model.fecModificacion = DateTime.Now;
                    ctx.Entry(model).State = EntityState.Modified;
                    ctx.SaveChanges();
                }
                catch (Exception ex)
                {
                    Logger log = LogManager.GetLogger("fileLogger");
                    log.Error(ex, "Error al editar el producto");
                }
            }
        }
        public void ChangeStatus(SNTt02_impuesto model)
        {
            try
            {
                using (AsiriContext ctx = new AsiriContext())
                {
                    bool estado = model.activo;
                    // Inverir el estado
                    model.activo = !estado;
                    if (estado)
                    {
                        //Evalúa si es true, se ser así se guardará como eliminación lógica
                        model.idUsuarioEliminar = "34";
                        model.fecEliminacion = DateTime.Now;
                    }
                    else
                    {
                        //Si el estado fue false se activará y se tomará como una modificación
                        model.idUsuarioModificar = "34";
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
