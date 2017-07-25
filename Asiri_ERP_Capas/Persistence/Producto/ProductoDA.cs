using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Common.Model;
using NLog;
using Common.Helper;

namespace Persistence.Producto
{
    public class ProductoDA
    {
        public IEnumerable<PROt02_producto> List()
        {
            IEnumerable<PROt02_producto> list;
            using (AsiriContext ctx = new AsiriContext())
            {
                try
                {
                    list = ctx.PROt02_producto.Include(p => p.PROt01_categoria).Include(p => p.SNTt03_moneda).ToList();
                }
                catch (Exception ex)
                {
                    Logger log = LogManager.GetLogger("fileLogger");
                    log.Error(ex, "Error al listar productos");
                    list = null;
                }
            }
            return list;
        }
        public PROt02_producto GetProduct(long? id)
        {
            PROt02_producto product;
            using (AsiriContext ctx = new AsiriContext())
            {
                try
                {
                    // No se hace el include de la tabla moneda porque no se usa (se obtiene del parámetro)
                    product = ctx.PROt02_producto.Include(p => p.PROt01_categoria).SingleOrDefault(x => x.idProducto == id);
                }
                catch (Exception)
                {
                    product = null;
                }
            }
            return product;
        }
        public void Add(PROt02_producto model)
        {
            using (AsiriContext ctx = new AsiriContext())
            {
                try
                {
                    // Evitar perder el id de moneda
                    model.idMoneda = Coin.GetCoin().idMoneda;
                    //Añadir la fecha en la que se está registrando el producto
                    model.fecRegistro = DateTime.Now;
                    //registrar id del usuario que registró el producto
                    /***********************************************/
                    //model.idUsuario = "34";
                    //Por defecto al registrarse está activo el producto
                    model.activo = true;
                    ctx.PROt02_producto.Add(model);
                    ctx.SaveChanges();
                }
                catch (Exception ex)
                {
                    Logger log = LogManager.GetLogger("fileLogger");
                    log.Error(ex, "Error al agregar producto");
                }
            }
        }
        public void Edit(PROt02_producto model)
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
                    model.idMoneda = Coin.GetCoin().idMoneda;
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
        public void ChangeStatus(PROt02_producto model)
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
