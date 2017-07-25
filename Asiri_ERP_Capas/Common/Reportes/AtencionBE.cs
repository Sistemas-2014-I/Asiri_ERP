using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Drawing;

namespace Common.Reportes
{
    public class AtencionBE
    {
        public string nombreCompleto { get; set; }
        public string numDocIdentidad { get; set; }
        public string codCita { get; set; }
        public string nombreServicio { get; set; }
        //public int idAtencion { get; set; }
        public string codAtencion { get; set; }
        public DateTime fecRegistro { get; set; }
        public string descEvolucion { get; set; }
        public string descDiagnostico { get; set; }
        public string descAnamnesis { get; set; }
        public string descEstudioCompl { get; set; }
        public string descExamenFisico { get; set; }
        public long idFuncionVital { get; set; }
        public string sistole { get; set; }
        public string diastole { get; set; }
        public string pulsacion { get; set; }
        public string ritmoRespiratorio { get; set; }
        public string temperatura { get; set; }
        public string altura { get; set; }
        public string peso { get; set; }
        public string imc { get; set; }
        // public long idTratamiento { get; set; }
        public string descTratamiento { get; set; }
        public string indicacionServicios { get; set; }
        public string nombreProducto { get; set; }
        public string descCie10 { get; set; }
        public byte[] pathArchivo { get; set; }
        public string codEmpleado { get; set; }
        public string NombreCompleto1 { get; set; }

        public string Servicios_pro { get; set; }

        public long idExamenFisico { get; set; }
        public long idEstudioCompl { get; set; }
        public long idDiagnostico { get; set; }
        public long idEvolucion { get; set; }
        public long idTratamientoDtl { get; set; }




    }
}