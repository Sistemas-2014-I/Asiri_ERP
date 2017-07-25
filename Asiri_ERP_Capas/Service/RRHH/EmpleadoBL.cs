using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using Persistence.RRHH;

namespace Service.RRHH
{
    public class EmpleadoBL
    {
        private EmpleadoDA empleadoDA = new EmpleadoDA();

        public List<RHUt01_empleado> Listar()
        {
            return empleadoDA.getEmpleado();
        }

        public void agregarEmpleado(RHUt01_empleado empleado)
        {
            empleadoDA.agregarEmpleado(empleado);
        }

        public RHUt01_empleado obtenerEmpleado(long id)
        {
            return empleadoDA.obtenerEmpleado(id);
        }

        public bool actualizarEmpleado(RHUt01_empleado empleado)
        {
            bool flag = false;
            if (empleadoDA.actualizarEmpleado(empleado))
            {
                flag = true;
            }
            else
            {
                flag = false;
            }
            return flag;
        }

        public bool eliminarEmpleado(int id)
        {
            bool flag = false;
            if (empleadoDA.eliminarEmpleado(id))
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
