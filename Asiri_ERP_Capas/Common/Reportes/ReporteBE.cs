using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Common.Reportes
{
    public class ReporteBE
    {
        public string nombreCompleto { get; set; }
        public string numDocIdentidad { get; set; }
        public string codPaciente { get; set; }
        public string codCita { get; set; }
        public DateTime fecCita { get; set; }
        public Decimal mtoSubtotal { get; set; }
        public Decimal debe { get; set; }
        public Decimal haber { get; set; }
        public Decimal saldo { get; set; }
        //public string descEstadoCita { get; set; }

        //para Estado Cuenta Particular
        public string nombreServicio { get; set; }
        public Decimal precio { get; set; }
        public Int32 idCuota { get; set; }
        public Decimal mtoDesembolso { get; set; }
        public Decimal mtoPago { get; set; }
        public DateTime fecDesembolso { get; set; }
        public DateTime fecPago { get; set; }

        // PARA COMPROBANTE EN PARTICULAR
        public string descTipoComprobante { get; set; }
        public Decimal cantidad { get; set; }
        public string serie { get; set; }
        public string numComprobanteEmitido { get; set; }
        public Decimal mtoTotal { get; set; }
        public Decimal mtoImpto { get; set; }
        public Decimal mtoDescuento { get; set; }
        public Decimal porcentajeImpto { get; set; }
        public Decimal porcentajeDescuento { get; set; }
        public DateTime fecEmision { get; set; }

    }
}