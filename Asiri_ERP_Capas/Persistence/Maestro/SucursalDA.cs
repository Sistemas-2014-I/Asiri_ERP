using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;

namespace Persistence.Maestro
{
    public class SucursalDA
    {
        string error = "";
        private AsiriContext db = new AsiriContext();

        public List<MSTt04_sucursal> getSucursal()
        {
            return (db.MSTt04_sucursal.ToList());
        }

        public void agregarSucursal(MSTt04_sucursal sucursal)
        {
            db.MSTt04_sucursal.Add(sucursal);
            db.SaveChanges();
        }

        public MSTt04_sucursal obtenerSucursal(int id)
        {
            return db.MSTt04_sucursal.Find(id);
        }

        public bool actualizarSucursal(MSTt04_sucursal sucursal)
        {
            bool flag = false;
            try
            {
                db.Entry(sucursal).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                flag = true;
            }
            catch (Exception ex)
            {
                error = ex.Message;
            }
            return flag;
        }

        public bool eliminarSucursal(int id)
        {
            bool flag = false;
            MSTt04_sucursal sucursal = db.MSTt04_sucursal.Find(id);
            if (sucursal.activo)
            {
                sucursal.activo = false;
                actualizarSucursal(sucursal);
            }
            else
            {
                sucursal.activo = true;
                actualizarSucursal(sucursal);
            }
            return flag;
        }
    }
}
