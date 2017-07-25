using Common.Helper;
using Common.Model;
using Persistence.Maestro;
using Persistence.Sistema;
using Service.Sistema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Maestro
{
    public class MedioDePagoBL
    {
        public MSTt01_medioDePago GetById(int id)
        {
            return new MedioDePagoDA().GetById(id);
        }

        public MSTt01_medioDePago GetMedioDePagoRapido()
        {
            return new MedioDePagoDA().GetMedioDePagoRapido();
        }
    }
}
