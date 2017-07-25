using System;

namespace Common.Reportes
{
    public class RepBuscDocBE
    {
        //Busqueda de doctor por documento o codigo
        public Int64 idCita { get; set; }
        public string codCita { get; set; }
        public string Doctor { get; set; }
        public string numDocIdentidad { get; set; }
    }
}