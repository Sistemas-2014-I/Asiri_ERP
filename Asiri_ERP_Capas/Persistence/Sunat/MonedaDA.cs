using Common;
using Common.Helper;
using System;
using System.Linq;

namespace Persistence.Sunat
{
    public class MonedaDA
    {
        public SNTt03_moneda GetMonedaSist()
        {
            using (var context = new AsiriContext())
            {
                try
                {
                    var paramMoneda = context.SISt01_parametro.SingleOrDefault(pm => pm.codParametro == CodParam.Moneda);
                    var moneda = paramMoneda != null ? context.SNTt03_moneda.SingleOrDefault(m => m.idMoneda == paramMoneda.valorNumerico) : null;
                    return moneda ?? new SNTt03_moneda { descMoneda = "-", simbolo = "-" };
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }
    }
}
