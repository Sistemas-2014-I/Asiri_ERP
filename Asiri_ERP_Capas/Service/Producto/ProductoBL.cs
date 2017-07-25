using Common;
using Persistence.Producto;
using System.Collections.Generic;

namespace Service.Producto
{
    public class ProductoBL
    {
        public IEnumerable<Common.Model.PROt02_producto> List()
        {
            return new ProductoDA().List();
        }
        public Common.Model.PROt02_producto GetProduct(long? id)
        {
            return new ProductoDA().GetProduct(id);
        }
        public void Add(Common.Model.PROt02_producto model)
        {
            new ProductoDA().Add(model);
        }
        public void Edit(Common.Model.PROt02_producto model)
        {
            new ProductoDA().Edit(model);
        }
        public void ChangeStatus(Common.Model.PROt02_producto model)
        {
            new ProductoDA().ChangeStatus(model);
        }
    }
}
