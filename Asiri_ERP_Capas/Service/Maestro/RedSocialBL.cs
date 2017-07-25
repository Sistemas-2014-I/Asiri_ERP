using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Persistence.Maestro;
using Common;

namespace Service.Maestro
{
    public class RedSocialBL
    {
        private RedSocialDA redSocialDA = new RedSocialDA();

        public List<MSTt03_redSocial> Listar()
        {
            return redSocialDA.getRedSocial();
        }

        public void agregarRedSocial(MSTt03_redSocial redSocial)
        {
            redSocialDA.agregarRedSocial(redSocial);
        }

        public MSTt03_redSocial obtenerRedSocial(int id)
        {
            return redSocialDA.obtenerRedSocial(id);
        }

        public bool actualizarRedSocial(MSTt03_redSocial redSocial)
        {
            bool flag = false;
            if (redSocialDA.actualizarRedSocial(redSocial))
            {
                flag = true;
            }
            else
            {
                flag = false;
            }
            return flag;
        }

        public bool eliminarRedSocial(int id)
        {
            bool flag = false;
            if (redSocialDA.eliminarRedSocial(id))
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
