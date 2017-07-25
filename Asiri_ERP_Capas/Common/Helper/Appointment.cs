using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Helper
{
    public class Appointment
    {
        public static string GetCitasxDia()
        {
            using (var context = new AsiriContext())
            {
                var resultado = "";
                try
                {
                    var paramCitasxDia = context.SISt01_parametro.SingleOrDefault(pm => pm.codParametro == CodParam.CitaxDia);
                    var citas = context.CLlt05_cita.Where(c => c.fecRegistro.Day == DateTime.Now.Day).ToList();
                    if (paramCitasxDia.valorNumerico== null)
                    {
                        paramCitasxDia.valorNumerico = paramCitasxDia.valorNumericoDefault;
                    }
                    if (citas.Count == paramCitasxDia.valorNumerico)
                    {
                        resultado = "Ha alcanzado el límite máximo de citas generadas por día";   
                    }
                    return resultado;
                }
                catch (Exception)
                {
                    
                    return resultado;
                }
                
            }
        }

        public static string GetReprogramacionxCita(long id)
        {
            using (var context = new AsiriContext())
            {
                var resultado = "";
                try
                {
                    var cita = context.CLlt05_cita.Find(id);
                    var paramReprogramacionxCita = context.SISt01_parametro.SingleOrDefault(pm => pm.codParametro == CodParam.NReprogramacionMax);
                    if (paramReprogramacionxCita.valorNumerico == null)
                    {
                        paramReprogramacionxCita.valorNumerico = paramReprogramacionxCita.valorNumericoDefault;
                    }
                    if (cita.numReprogramacion >= paramReprogramacionxCita.valorNumerico)
                    {
                        resultado = "Ha alcanzado el límite máximo de reprogramaciones permitidas";
                    }
                    return resultado;
                }
                catch (Exception)
                {

                    return resultado;
                }

            }
        }

        public static string HoraFinxDia()
        {
            using (var context = new AsiriContext())
            {
                var resultado = "";
                try
                {
                    var paramReprogramacionxCita = context.SISt01_parametro.SingleOrDefault(pm => pm.codParametro == CodParam.HoraFinDiaLaboral);
                    if (paramReprogramacionxCita.valorDeTexto == null)
                    {
                        paramReprogramacionxCita.valorDeTexto = paramReprogramacionxCita.valorTextoDefault;
                    }
                    resultado = paramReprogramacionxCita.valorDeTexto;
                    return resultado;
                }
                catch (Exception)
                {

                    return resultado;
                }

            }
        }
    }
}
