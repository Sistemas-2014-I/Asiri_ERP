using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Reportes
{
    public class CitaBE
    {
        public string nombreCompleto { get; set; }
        public string numDocIdentidad { get; set; }
        public string codPaciente { get; set; }
        public string nombreDistrito { get; set; }
        public DateTime fecRegistro { get; set; }
        public string descEstadoCita { get; set; }
    }
}
