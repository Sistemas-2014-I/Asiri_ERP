using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;

namespace Persistence.RRHH
{
    public class PersonaDA
    {
        string error = "";
        private AsiriContext db = new AsiriContext();
        public List<RHUt09_persona> getPersona()
        {
            return (db.RHUt09_persona.ToList());
        }

        public bool agregarPersona(RHUt09_persona persona)
        {
            bool flag = false;
            try
            {
                db.RHUt09_persona.Add(persona);
                db.SaveChanges();
                flag = true;
            }
            catch (Exception ex)
            {
                error = ex.Message;
            }
            return flag;
        }

        public RHUt09_persona obtenerPersona(long id)
        {
            return db.RHUt09_persona.Find(id);
        }

        public bool actualizarPersona(RHUt09_persona persona)
        {
            bool flag = false;
            try
            {
                db.Entry(persona).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                flag = true;
            }
            catch (Exception ex)
            {
                error = ex.Message;
            }
            return flag;
        }

        public bool eliminarPersona(int id)
        {
            bool flag = false;
            RHUt09_persona persona = db.RHUt09_persona.Find(id);
            if (persona.activo)
            {
                persona.activo = false;
                actualizarPersona(persona);
            }
            else
            {
                persona.activo = true;
                actualizarPersona(persona);
            }
            return flag;
        }
    }
}
