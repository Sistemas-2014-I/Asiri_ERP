using Persistence.Servicio;
using System.Collections.Generic;
using Common;

namespace Service.Servicio
{
    public class ServicioBL
    {
        public IEnumerable<PROt04_servicio> List()
        {
            return new ServicioDA().List();
        }
        public PROt04_servicio GetService(long? id)
        {
            return new ServicioDA().GetService(id);
        }
        public void Add(PROt04_servicio model)
        {
            new ServicioDA().Add(model);
        }
        public void Edit(PROt04_servicio model)
        {
            new ServicioDA().Edit(model);
        }
        public void ChangeStatus(PROt04_servicio model)
        {
            new ServicioDA().ChangeStatus(model);
        }
        public IEnumerable<PROt04_servicio> GetServicesByTypeService(int id)
        {
            return new ServicioDA().GetServicesByTypeService(id);
        }
    }
}
