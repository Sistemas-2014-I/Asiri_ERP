using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Common;

namespace Persistence.Maestro
{
    public class PisoDA
    {
        string _error = "";
        private AsiriContext db = new AsiriContext();

        public List<MSTt02_piso> getPiso()
        {
            return (db.MSTt02_piso.ToList());
        }

        public void agregarPiso(MSTt02_piso piso)
        {
            db.MSTt02_piso.Add(piso);
            db.SaveChanges();
        }

        public MSTt02_piso obtenerPiso(int id)
        {
            return db.MSTt02_piso.Find(id);
        }

        public bool actualizarPiso(MSTt02_piso piso)
        {
            bool flag = false;
            try
            {
                db.Entry(piso).State = EntityState.Modified;
                db.SaveChanges();
                flag = true;
            }
            catch (Exception ex)
            {
                _error = ex.Message;
            }
            return flag;
        }

        public bool eliminarPiso(int id)
        {
            bool flag = false;
            MSTt02_piso piso = db.MSTt02_piso.Find(id);
            if (piso.activo)
            {
                piso.activo = false;
                actualizarPiso(piso);
            }
            else
            {
                piso.activo = true;
                actualizarPiso(piso);
            }
            return flag;
        }
    }
}
