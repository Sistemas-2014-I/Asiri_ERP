using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Common.Reportes
{
    public class RepBusFecCitaBE
    {
        public Int64 idCita { get; set; }
        public DateTime fecCita { get; set; }
        public DateTime fecRegistro { get; set; }
        public String horaInicio { get; set; }
        public Decimal mtoTotal { get; set; }
        public String descEstadoCita { get; set; }
        public String Paciente { get; set; }
    }
}