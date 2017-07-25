using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;

namespace Persistence.Maestro
{
    public class TipoSucursalDA
    {
        string error = "";
        private AsiriContext db = new AsiriContext();

        public List<MSTt05_tipoSucursal> getTipoSucursal()
        {
            return (db.MSTt05_tipoSucursal.ToList());
        }

        public void agregarTipoSucursal(MSTt05_tipoSucursal tipoSucursal)
        {
            db.MSTt05_tipoSucursal.Add(tipoSucursal);
            db.SaveChanges();
        }

        public MSTt05_tipoSucursal obtenerTipoSucursal(int id)
        {
            return db.MSTt05_tipoSucursal.Find(id);
        }

        public bool actualizarTipoSucursal(MSTt05_tipoSucursal tipoSucursal)
        {
            bool flag = false;
            try
            {
                db.Entry(tipoSucursal).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                flag = true;
            }
            catch (Exception ex)
            {
                error = ex.Message;
            }
            return flag;
        }

        public bool eliminarTipoSucursal(int id)
        {
            bool flag = false;
            MSTt05_tipoSucursal tipoSucursal = db.MSTt05_tipoSucursal.Find(id);
            if (tipoSucursal.activo)
            {
                tipoSucursal.activo = false;
                actualizarTipoSucursal(tipoSucursal);
            }
            else
            {
                tipoSucursal.activo = true;
                actualizarTipoSucursal(tipoSucursal);
            }
            return flag;
        }
    }
}
