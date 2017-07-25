using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.View.View_Report
{
    public class ComprobanteVR
    {
        public long idComprobanteEmitido { get; set; }
        public string serieNumComp { get; set; }
        public string descTipoComprobante { get; set; }
        public string fecEmision { get; set; }
        public string fecVencimiento { get; set; }
        public string fecAnulacion { get; set; }
        public string fecCancelacion { get; set; }
        public string obsvComprobanteEmitido { get; set; }
        public string descSucursal { get; set; }
        public string direccionSucursal { get; set; }
        public string telefono01Sucural { get; set; }
        public string telefono02Sucural { get; set; }
        public string nombrePaciente { get; set; }
        public string numDocIdentidad { get; set; }
        public string descUbigeo { get; set; }
        public string direccionPaciente { get; set; }
        public string docIdentidad { get; set; }
        public string mtoSubtotal { get; set; }
        public string mtoImpto { get; set; }
        public string mtoDescuento { get; set; }
        public string mtoTotal { get; set; }
        public string porcImpto { get; set; }
        public string porcDescuento { get; set; }
        public string abrvImpto { get; set; }
        public string nombreServicio { get; set; }
        public string precio { get; set; }
        public decimal cantidad { get; set; }
        public string importe { get; set; }
    }
}
