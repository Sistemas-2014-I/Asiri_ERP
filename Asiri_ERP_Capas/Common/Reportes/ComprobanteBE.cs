using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Reportes
{
    public class ComprobanteBE
    {
        public int idComprobanteEmitido { get; set; }
        public string numComp { get; set; }
        public string descTipoComprobante { get; set; }
        public DateTime fecEmision { get; set; }
        public DateTime fecVencimiento { get; set; }
        public DateTime fecAnulacion { get; set; }
        public DateTime fecCancelacion { get; set; }
        public string obsvComprobanteEmitido { get; set; }
        public string descSucursal { get; set; }
        public string direccionSucursal { get; set; }
        public string telefono01Sucural { get; set; }
        public string telefono02Sucural { get; set; }
        public string nombrePaciente { get; set; }
        public string razonSocial { get; set; }
        public string numDocIdentidad { get; set; }
        public string direccion01Paciente { get; set; }
        public string direccion02Paciente { get; set; }
        public string docIdentidad { get; set; }
        public decimal mtoSubtotal { get; set; }
        public decimal mtoImpto { get; set; }
        public decimal mtoDescuento { get; set; }
        public decimal mtoTotal { get; set; }
        public decimal porcImpto { get; set; }
        public decimal porcDescuento { get; set; } 
        public string abrvImpto { get; set; }
        public string nombreServicio { get; set; }
        public decimal precio { get; set; }
        public decimal cantidad { get; set; }
        public decimal importe { get; set; }

    }
}
