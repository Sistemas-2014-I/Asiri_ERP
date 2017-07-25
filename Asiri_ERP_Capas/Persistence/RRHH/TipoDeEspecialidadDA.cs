using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;

namespace Persistence.RRHH
{
    public class TipoDeEspecialidadDA
    {
        string error = "";
        private AsiriContext db = new AsiriContext();
        public List<RHUt13_tipoEspecialidad> getTipoDeEspecialidad()
        {
            return (db.RHUt13_tipoEspecialidad.ToList());
        }

        public void agregarTipoDeEspecialidad(RHUt13_tipoEspecialidad tipoDeEspecialidad)
        {
            db.RHUt13_tipoEspecialidad.Add(tipoDeEspecialidad);
            db.SaveChanges();
        }

        public RHUt13_tipoEspecialidad obtenerTipoDeEspecialidad(int id)
        {
            return db.RHUt13_tipoEspecialidad.Where(e => e.idTipoDeEspeciliadad == id).Single();
        }

        public bool actualizarTipoDeEspecialidad(RHUt13_tipoEspecialidad tipodeEspecialidad)
        {
            bool flag = false;
            try
            {
                db.Entry(tipodeEspecialidad).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                flag = true;
            }
            catch (Exception ex)
            {
                error = ex.Message;
            }
            return flag;
        }

        public bool eliminarTipoDeEspecialidad(int id)
        {
            bool flag = false;
            RHUt13_tipoEspecialidad tipoDeEspecialidad = db.RHUt13_tipoEspecialidad.Find(id);
            if (tipoDeEspecialidad.activo)
            {
                tipoDeEspecialidad.activo = false;
                actualizarTipoDeEspecialidad(tipoDeEspecialidad);
            }
            else
            {
                tipoDeEspecialidad.activo = true;
                actualizarTipoDeEspecialidad(tipoDeEspecialidad);
            }
            return flag;
        }
    }
}
