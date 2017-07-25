using Common;
using Persistence.Sunat;

namespace Service.Sunat
{
    public class MonedaBL
    {
        public SNTt03_moneda GetMonedaSist()
        {
            return new MonedaDA().GetMonedaSist();
        }
    }
}
