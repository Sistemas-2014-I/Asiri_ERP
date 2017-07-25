using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace   Common.Reportes
{
	public class EspecialidadBE
	{
        public string codEmpleado { get; set; }
        public string descTipoDeEmpleado { get; set; }
        public string nombrePersona { get; set; }
        public string apellidoPaterno { get; set; }
        public string apellidoMaterno { get; set; }
        public string numDocIdentidad { get; set; }
        public string nombreEspecialidad { get; set; }
        public int CantidadCita { get; set; }
    }
}