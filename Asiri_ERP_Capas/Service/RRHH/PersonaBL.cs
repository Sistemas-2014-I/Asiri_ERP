using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using Persistence.RRHH;

namespace Service.RRHH
{
    public class PersonaBL
    {
        private PersonaDA personaDA = new PersonaDA();

        public List<RHUt09_persona> Listar()
        {
            return personaDA.getPersona();
        }

        public bool agregarPersona(RHUt09_persona persona)
        {
            bool flag = false;
            if (personaDA.agregarPersona(persona))
                flag = true;
            else
                flag = false;
            return flag;
        }

        public RHUt09_persona obtenerPersona(long id)
        {
            return personaDA.obtenerPersona(id);
        }

        public bool actualizarPersona(RHUt09_persona persona)
        {
            bool flag = false;
            if (personaDA.actualizarPersona(persona))
            {
                flag = true;
            }
            else
            {
                flag = false;
            }
            return flag;
        }

        public bool eliminarPersona(int id)
        {
            bool flag = false;
            if (personaDA.eliminarPersona(id))
            {
                flag = true;
            }
            else
            {
                flag = false;
            }
            return flag;
        }
    }
}
