using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Persistence.Maestro;
using Common;

namespace Service.Maestro
{
    public class TIpoSucursalBL
    {
        private TipoSucursalDA tipoSucursalDA = new TipoSucursalDA();

        public List<MSTt05_tipoSucursal> Listar()
        {
            return tipoSucursalDA.getTipoSucursal();
        }

        public void agregarTipoSucursal(MSTt05_tipoSucursal tipoSucursal)
        {
            tipoSucursalDA.agregarTipoSucursal(tipoSucursal);
        }

        public MSTt05_tipoSucursal obtenerTipoSucursal(int id)
        {
            return tipoSucursalDA.obtenerTipoSucursal(id);
        }

        public bool actualizarTipoSucursal(MSTt05_tipoSucursal tipoSucursal)
        {
            bool flag = false;
            if (tipoSucursalDA.actualizarTipoSucursal(tipoSucursal))
            {
                flag = true;
            }
            else
            {
                flag = false;
            }
            return flag;
        }

        public bool eliminarTipoSucursal(int id)
        {
            bool flag = false;
            if (tipoSucursalDA.eliminarTipoSucursal(id))
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
