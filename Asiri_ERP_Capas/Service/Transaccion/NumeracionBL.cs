using Common.Model;
using Common.View.ViewModel;
using Persistence.Transaccion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Transaccion
{
    public class NumeracionBL
    {
        public IEnumerable<NumeracionVM> List()
        {
            return new NumeracionDA().List();
        }

        public TNSt07_numeracion GetById(int id)
        {
            return new NumeracionDA().GetById(id);
        }

        public int Add(TNSt07_numeracion obj)
        {
            return new NumeracionDA().Add(obj);
        }

        public bool Edit(TNSt07_numeracion obj)
        {
            return new NumeracionDA().Edit(obj);
        }
    }
}
