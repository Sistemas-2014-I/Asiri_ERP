using Common.Model;
using NLog;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Persistence.Producto
{
    public class CategoriaDA
    {
        public IEnumerable<PROt01_categoria> List()
        {
            IEnumerable<PROt01_categoria> list = null;
            using (AsiriContext ctx = new AsiriContext())
            {
                try
                {
                    list = ctx.PROt01_categoria.ToList();
                }
                catch (Exception ex)
                {
                    Logger log = LogManager.GetLogger("fileLogger");
                    log.Error(ex, "Error al listar categorias de productos");
                }
            }
            return list;
        }
        public PROt01_categoria GetCategory(int? id)
        {
            PROt01_categoria category;
            using (AsiriContext ctx = new AsiriContext())
            {
                try
                {
                    category = ctx.PROt01_categoria.Find(id);
                }
                catch (Exception)
                {
                    category = null;
                }
            }
            return category;
        }
        public void Add(PROt01_categoria model)
        {
            using (AsiriContext ctx = new AsiriContext())
            {
                try
                {
                    //Añadir la fecha en la que se está registrando
                    model.fecRegistro = DateTime.Now;
                    //registrar id del usuario que registró
                    /***********************************************/
                    //model.idUsuario = "34";
                    //Por defecto al registrarse está activo
                    model.activo = true;
                    ctx.PROt01_categoria.Add(model);
                    ctx.SaveChanges();
                }
                catch (Exception ex)
                {
                    //StringBuilder st = new StringBuilder();
                    //foreach (var eve in ex.EntityValidationErrors)
                    //{
                    //    st.AppendLine($"Entidad: \"{eve.Entry.Entity.GetType().Name}\" Estado: \"{eve.Entry.State}\" se tienen los siguientes errores de validación:");
                    //    foreach (var ve in eve.ValidationErrors)
                    //    {
                    //        st.AppendLine($"- Propiedad: \"{ve.PropertyName}\", Error: \"{ve.ErrorMessage}\"");
                    //    }
                    //}
                    //System.Diagnostics.Debug.Write(ex.Message);
                    //Logger log = LogManager.GetLogger("fileLogger");
                    //log.Error(ex, "Error al agregar una categoria de producto");
                }
            }
        }
        public void Edit(PROt01_categoria model)
        {
            using (AsiriContext ctx = new AsiriContext())
            {
                try
                {
                    //Añadir usuario que modifica
                    /******************FALTA UNIR CON LO EL USURIO RE ROLES******************/
                    //model.idUsuarioModificar = "34";

                    //Añadir fecha en la que se está editando
                    model.fecModificacion = DateTime.Now;
                    ctx.Entry(model).State = EntityState.Modified;
                    ctx.SaveChanges();
                }
                catch (Exception ex)
                {
                    Logger log = LogManager.GetLogger("fileLogger");
                    log.Error(ex, "Error al editar una categoría de producto");
                }
            }
        }
        public void ChangeStatus(PROt01_categoria model)
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
                        //model.idUsuarioEliminar = "34";
                        model.fecEliminacion = DateTime.Now;
                    }
                    else
                    {
                        //Si el estado fue false se activará y se tomará como una modificación
                        //model.idUsuarioModificar = "34";
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
