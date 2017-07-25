using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using Persistence.Maestro;

namespace Service.Maestro
{
    public class SucursalBL
    {
        private SucursalDA sucursalDA = new SucursalDA();

        public List<MSTt04_sucursal> Listar()
        {
            return sucursalDA.getSucursal();
        }

        public void agregarSucursal(MSTt04_sucursal sucursal)
        {
            sucursalDA.agregarSucursal(sucursal);
        }

        public MSTt04_sucursal obtenerSucursal(int id)
        {
            return sucursalDA.obtenerSucursal(id);
        }

        public bool actualizarSucursal(MSTt04_sucursal sucursal)
        {
            bool flag = false;
            if (sucursalDA.actualizarSucursal(sucursal))
            {
                flag = true;
            }
            else
            {
                flag = false;
            }
            return flag;
        }

        public bool eliminarSucursal(int id)
        {
            bool flag = false;
            if (sucursalDA.eliminarSucursal(id))
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
