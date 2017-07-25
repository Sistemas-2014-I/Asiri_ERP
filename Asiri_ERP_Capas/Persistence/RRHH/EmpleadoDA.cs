using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using System.Data.Entity;

namespace Persistence.RRHH
{
    public class EmpleadoDA
    {
        string error = "";
        private AsiriContext db = new AsiriContext();
        public List<RHUt01_empleado> getEmpleado()
        {
            return (db.RHUt01_empleado.Include(x=>x.CLlt07_consultorio).Include(x=>x.MSTt01_medioDePago).Include(x => x.RHUt02_empleadoEspecialidad).Include(x => x.RHUt09_persona).Include(x => x.RHUt11_tipoDeEmpleado).Include(x => x.SNTt01_entidadFinanciera).ToList());
        }

        public void agregarEmpleado(RHUt01_empleado empleado)
        {
            db.RHUt01_empleado.Add(empleado);
            db.SaveChanges();
        }

        public RHUt01_empleado obtenerEmpleado(long id)
        {
            return db.RHUt01_empleado.Find(id);
        }

        public bool actualizarEmpleado(RHUt01_empleado empleado)
        {
            bool flag = false;
            try
            {
                db.Entry(empleado).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                flag = true;
            }
            catch (Exception ex)
            {
                error = ex.Message;
            }
            return flag;
        }

        public bool eliminarEmpleado(int id)
        {
            bool flag = false;
            RHUt01_empleado empleado = db.RHUt01_empleado.Find(id);
            if (empleado.activo)
            {
                empleado.activo = false;
                actualizarEmpleado(empleado);
            }
            else
            {
                empleado.activo = true;
                actualizarEmpleado(empleado);
            }
            return flag;
        }
    }
}
