using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Persistence.RRHH;
using Common;

namespace Service.RRHH
{
    public class TipoDeEspecialidadBL
    {
        private TipoDeEspecialidadDA tipoDeEspecialidadDA = new TipoDeEspecialidadDA();

        public List<RHUt13_tipoEspecialidad> Listar()
        {
            return tipoDeEspecialidadDA.getTipoDeEspecialidad();
        }

        public void agregarTipoDeEspecialidad(RHUt13_tipoEspecialidad tipoDeEspecialidad)
        {
            tipoDeEspecialidadDA.agregarTipoDeEspecialidad(tipoDeEspecialidad);
        }

        public RHUt13_tipoEspecialidad obtenerTipoDeEspecialidad(int id)
        {
            return tipoDeEspecialidadDA.obtenerTipoDeEspecialidad(id);
        }

        public bool actualizarTipoDeEspecialidad(RHUt13_tipoEspecialidad tipoDeEspecialidad)
        {
            bool flag = false;
            if (tipoDeEspecialidadDA.actualizarTipoDeEspecialidad(tipoDeEspecialidad))
            {
                flag = true;
            }
            else
            {
                flag = false;
            }
            return flag;
        }

        public bool eliminarTipoDeEspecialidad(int id)
        {
            bool flag = false;
            if (tipoDeEspecialidadDA.eliminarTipoDeEspecialidad(id))
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
