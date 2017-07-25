using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;

namespace Persistence.Maestro
{
    public class RedSocialDA
    {
        string error = "";
        private AsiriContext db = new AsiriContext();

        public List<MSTt03_redSocial> getRedSocial()
        {
            return (db.MSTt03_redSocial.ToList());
        }

        public void agregarRedSocial(MSTt03_redSocial redSocial)
        {
            db.MSTt03_redSocial.Add(redSocial);
            db.SaveChanges();
        }

        public MSTt03_redSocial obtenerRedSocial(int id)
        {
            return db.MSTt03_redSocial.Find(id);
        }

        public bool actualizarRedSocial(MSTt03_redSocial redSocial)
        {
            bool flag = false;
            try
            {
                db.Entry(redSocial).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                flag = true;
            }
            catch (Exception ex)
            {
                error = ex.Message;
            }
            return flag;
        }

        public bool eliminarRedSocial(int id)
        {
            bool flag = false;
            MSTt03_redSocial redSocial = db.MSTt03_redSocial.Find(id);
            if (redSocial.activo)
            {
                redSocial.activo = false;
                actualizarRedSocial(redSocial);
            }
            else
            {
                redSocial.activo = true;
                actualizarRedSocial(redSocial);
            }
            return flag;
        }
    }
}
