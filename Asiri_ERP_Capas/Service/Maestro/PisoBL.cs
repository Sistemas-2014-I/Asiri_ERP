using System.Collections.Generic;
using Common;
using Persistence.Maestro;

namespace Service.Maestro
{
    public class PisoBL
    {
        private PisoDA pisoDA = new PisoDA();

        public List<MSTt02_piso> Listar()
        {
            return pisoDA.getPiso();
        }

        public void agregarPiso(MSTt02_piso piso)
        {
            pisoDA.agregarPiso(piso);
        }

        public MSTt02_piso obtenerPiso(int id)
        {
            return pisoDA.obtenerPiso(id);
        }

        public bool actualizarPiso(MSTt02_piso piso)
        {
            bool flag = pisoDA.actualizarPiso(piso);
            return flag;
        }

        public bool eliminarPiso(int id)
        {
            bool flag = pisoDA.eliminarPiso(id);
            return flag;
        }
    }
}
