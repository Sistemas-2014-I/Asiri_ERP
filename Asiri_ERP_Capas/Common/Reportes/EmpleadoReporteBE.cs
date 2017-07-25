using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Common.Reportes
{
    public class EmpleadoReporteBE
    {
        public string descTipoDeEmpleado { get; set; }
        public string codEmpleado { get; set; }
        public string nombrePersona { get; set; }
        public string apellidoPaterno { get; set; }
        public string apellidoMaterno { get; set; }
        public string numDocIdentidad { get; set; }
        public EmpleadoReporteBE emp { get; set; }
    }
}