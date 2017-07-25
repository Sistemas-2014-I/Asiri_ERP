using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using Persistence.Sistema;

namespace Service.Sistema
{
    public class ParametroBL
    {
        public SISt01_parametro GetByCod(string cod)
        {
            return new ParametroDA().GetByCod(cod);
        }
    }
}
