using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.RRHH
{
    public class EspecialidadDA
    {
        string error = "";
        private AsiriContext db = new AsiriContext();
        public List<RHUt04_especialidad> getEspecialidad()
        {
            return (db.RHUt04_especialidad.ToList());
        }

        public void agregarEspecialidad(RHUt04_especialidad especialidad)
        {
            db.RHUt04_especialidad.Add(especialidad);
            db.SaveChanges();
        }

        public RHUt04_especialidad obtenerEspecialidad(int id)
        {
            return db.RHUt04_especialidad.Where(e => e.idEspecialidad == id).Single();
        }

        public bool actualizarEspecialidad(RHUt04_especialidad especialidad)
        {
            bool flag = false;
            try
            {
                db.Entry(especialidad).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                flag = true;
            }
            catch (Exception ex)
            {
                error = ex.Message;
            }
            return flag;
        }

        public bool eliminarEspecialidad(int id)
        {
            bool flag = false;
            RHUt04_especialidad especialidad = db.RHUt04_especialidad.Find(id);
            if (especialidad.activo)
            {
                especialidad.activo = false;
                actualizarEspecialidad(especialidad);
            }
            else
            {
                especialidad.activo = true;
                actualizarEspecialidad(especialidad);
            }
            return flag;
        }
    }
}
