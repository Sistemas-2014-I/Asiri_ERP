using Common.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.View.ViewModel
{
    public class CitasPorCobrarVM
    {
        public long idCita { get; set; }
        public string codCita { get; set; }

        public string tipoPersoneria { get; set; }
        //public bool esCerrado { get; set; }

        public string nombrePaciente { get; set; }
        public string apellidoPaternoPaciente { get; set; }
        public string apellidoMaternoPaciente { get; set; }

        public string razonSocial { get; set; }
        public string nombreRepresentante { get; set; }

        public string tipoDoc { get; set; }
        public string numDoc { get; set; }
        public List<CitaServicioVM> servicios { get; set; }
        public DateTime fechaCita { get; set; }
        public string horaInicio { get; set; }
        public string estadoCita { get; set; }

        public CitasPorCobrarVM()
        {
            servicios = new List<CitaServicioVM>();
        }
        public string GetNombrePaciente()
        {
            return tipoPersoneria == ValuesSystem.PersonaNatural ?
                    string.Join(" ", new string[] { apellidoPaternoPaciente, apellidoMaternoPaciente, nombrePaciente }).ToUpper() :
                    tipoPersoneria == ValuesSystem.PersonaJuridica ? nombreRepresentante : "Tipo de personería incorrecto - ERRORx01";
        }

        public string GetFechaYHoraInicio()
        {
            return fechaCita.ToString("dd/MM/yyyy") + " " + horaInicio;
        }
    }

    public class CitaServicioVM
    {
        public int idServicio { get; set; }
        public decimal cantidad { get; set; }
        public decimal precio { get; set; }
        public string nombreServicio { get; set; }
    }
}
