using Common;
using Persistence.Producto;
using System.Collections.Generic;

namespace Service.Producto
{
    public class CategoriaBL
    {
        public IEnumerable<Common.Model.PROt01_categoria> List()
        {
            return new CategoriaDA().List();
        }
        public Common.Model.PROt01_categoria GetCategory(int? id)
        {
            return new CategoriaDA().GetCategory(id);
        }
        public void Add(Common.Model.PROt01_categoria model)
        {
            new CategoriaDA().Add(model);
        }
        public void Edit(Common.Model.PROt01_categoria model)
        {
            new CategoriaDA().Edit(model);
        }
        public void ChangeStatus(Common.Model.PROt01_categoria model)
        {
            new CategoriaDA().ChangeStatus(model);
        }
    }
}
