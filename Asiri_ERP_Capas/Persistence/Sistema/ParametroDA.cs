using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using NLog;

namespace Persistence.Sistema
{
    public class ParametroDA
    {
        public SISt01_parametro GetByCod(string cod)
        {
            var obj = new SISt01_parametro();
            using (var ctx = new AsiriContext())
            {
                try
                {
                    obj = ctx.SISt01_parametro.SingleOrDefault(x => x.codParametro == cod);
                }
                catch (Exception e)
                {
                    var log = LogManager.GetLogger("fileLogger");
                    log.Error(e, "Excepción en: Consultar un parámetro por COD.");
                }
            }
            return obj;
        }
    }
}
