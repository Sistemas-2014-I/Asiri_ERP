using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Common.Reportes
{
    public class PacienteBE
    {
        public string codPaciente { get; set; }
        public string descEstadoCivil { get; set; }
        public string nombreDistrito { get; set; }
        public string nombreProvincia { get; set; }
        public string nombreRegion { get; set; }
        public string descZona { get; set; }
        public string nombreCompleto { get; set; }
        public string numDocIdentidad { get; set; }
        public string direccion01 { get; set; }
        public DateTime fecRegistro { get; set; }

        //para top n
        public string direccion { get; set; }
        public int NumeroCitas { get; set; }
    }
}