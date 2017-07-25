using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Common.Reportes
{
    public class RepEvoPacBE
    {
        public Int64 idAtencion { get; set; }
        public String descEvolucion { get; set; }
        public String codCita { get; set; }
        public String Paciente { get; set; }
        public String numDocIdentidad { get; set; }
        public String descDiagnostico { get; set; }
    }
}