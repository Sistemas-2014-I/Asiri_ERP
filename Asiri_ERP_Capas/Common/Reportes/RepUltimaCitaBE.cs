using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Common.Reportes
{
    public class RepUltimaCitaBE
    {
        public String Paciente { get; set; }
        public DateTime UltimaCita {get;set;}
        public String direccion01 { get; set; }
        public String numTelefonico01 { get; set; }
        public String direccion02 { get; set; }
        public String numTelefonico02 { get; set; }
    }
}