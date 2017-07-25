using Persistence.Sunat;
using System.Collections.Generic;
using Common;

namespace Service.Sunat
{
    public class ImpuestoBL
    {
        public IEnumerable<SNTt02_impuesto> List()
        {
            return new ImpuestoDA().List();
        }
        public SNTt02_impuesto GetImpuesto(short? id)
        {
            return new ImpuestoDA().GetImpuesto(id);
        }
        public void Add(SNTt02_impuesto model)
        {
            new ImpuestoDA().Add(model);
        }
        public void Edit(SNTt02_impuesto model)
        {
            new ImpuestoDA().Edit(model);
        }
        public void ChangeStatus(SNTt02_impuesto model)
        {
            new ImpuestoDA().ChangeStatus(model);
        }
    }
}
