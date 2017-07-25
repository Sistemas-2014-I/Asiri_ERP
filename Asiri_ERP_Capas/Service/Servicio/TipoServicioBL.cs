using Common;
using Persistence.Servicio;
using System.Collections.Generic;

namespace Service.Servicio
{
    public class TipoServicioBL
    {
        public IEnumerable<PROt05_tipoDeServicio> List()
        {
            return new TipoServicioDA().List();
        }
        public PROt05_tipoDeServicio GetTypeService(long? id)
        {
            return new TipoServicioDA().GetTypeService(id);
        }
        public void Add(PROt05_tipoDeServicio model)
        {
            new TipoServicioDA().Add(model);
        }
        public void Edit(PROt05_tipoDeServicio model)
        {
            new TipoServicioDA().Edit(model);
        }
        public void ChangeStatus(PROt05_tipoDeServicio model)
        {
            new TipoServicioDA().ChangeStatus(model);
            if (!model.activo)
            {
                IEnumerable<PROt04_servicio> servicios = new ServicioBL().GetServicesByTypeService(model.idTipoDeServicio);
                foreach (var item in servicios)
                {
                    item.activo = false;
                    new ServicioBL().Edit(item);
                } 
            }
        }
    }
}
