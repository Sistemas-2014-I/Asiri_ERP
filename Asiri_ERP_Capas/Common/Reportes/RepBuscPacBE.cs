using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Common.Reportes
{
    public class RepBuscPacBE
    {
        //Busqueda del paciente por documento o codigo
        public Int64 idCita { get; set; }
        public string codCita { get; set; }
        public string Paciente { get; set; }
        public string numDocIdentidad { get; set; }
    }
}