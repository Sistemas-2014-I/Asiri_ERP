using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Persistence.RRHH;
using Common;

namespace Service.RRHH
{
    public class EspecialidadBL
    {
        private EspecialidadDA especialidadDA = new EspecialidadDA();

        public List<RHUt04_especialidad> Listar()
        {
            return especialidadDA.getEspecialidad();
        }

        public void agregarEspecialidad(RHUt04_especialidad especialidad)
        {
            this.especialidadDA.agregarEspecialidad(especialidad);
        }

        public RHUt04_especialidad obtenerEspecialidad(int id)
        {
            return especialidadDA.obtenerEspecialidad(id);
        }

        public bool actualizarEspecialidad(RHUt04_especialidad especialidad)
        {
            bool flag = false;
            if (this.especialidadDA.actualizarEspecialidad(especialidad))
            {
                flag = true;
            }
            else
            {
                flag = false;
            }
            return flag;
        }

        public bool eliminarEspecialidad(int id)
        {
            bool flag = false;
            if (especialidadDA.eliminarEspecialidad(id))
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
