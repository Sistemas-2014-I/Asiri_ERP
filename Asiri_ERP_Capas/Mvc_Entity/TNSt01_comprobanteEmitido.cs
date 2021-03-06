//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Mvc_Entity
{
    using System;
    using System.Collections.Generic;
    
    public partial class TNSt01_comprobanteEmitido
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TNSt01_comprobanteEmitido()
        {
            this.TNSt02_comprobanteEmitidoDtl = new HashSet<TNSt02_comprobanteEmitidoDtl>();
            this.TNSt06_medioDePagoDtl = new HashSet<TNSt06_medioDePagoDtl>();
        }
    
        public long idComprobanteEmitido { get; set; }
        public string serie { get; set; }
        public string numComprobanteEmitido { get; set; }
        public System.DateTime fecRegistro { get; set; }
        public Nullable<System.DateTime> fecModificacion { get; set; }
        public Nullable<System.DateTime> fecAnulacion { get; set; }
        public string razonAnulacion { get; set; }
        public bool esAnulado { get; set; }
        public System.DateTime fecEmision { get; set; }
        public Nullable<System.DateTime> fecVencimiento { get; set; }
        public Nullable<System.DateTime> fecCancelacion { get; set; }
        public decimal mtoTotal { get; set; }
        public decimal mtoSubTotal { get; set; }
        public decimal mtoImpto { get; set; }
        public decimal mtoDescuento { get; set; }
        public decimal porcentajeImpto { get; set; }
        public decimal porcentajeDescuento { get; set; }
        public string obsvComprobanteEmitido { get; set; }
        public string info01 { get; set; }
        public string info02 { get; set; }
        public string info03 { get; set; }
        public Nullable<System.DateTime> fecha01 { get; set; }
        public Nullable<System.DateTime> fecha02 { get; set; }
        public Nullable<System.DateTime> fecha03 { get; set; }
        public long idCita { get; set; }
        public Nullable<short> idEstadoComprobante { get; set; }
        public int idSucursal { get; set; }
        public int idTipoComprobante { get; set; }
        public int idMoneda { get; set; }
        public short idImpto { get; set; }
        public string idUsuario { get; set; }
        public string idUsuarioModificar { get; set; }
        public string idUsuarioAnular { get; set; }
        public Nullable<long> idCuota { get; set; }
    
        public virtual CLlt05_cita CLlt05_cita { get; set; }
        public virtual MSTt04_sucursal MSTt04_sucursal { get; set; }
        public virtual SNTt02_impuesto SNTt02_impuesto { get; set; }
        public virtual SNTt03_moneda SNTt03_moneda { get; set; }
        public virtual SNTt04_tipoComprobante SNTt04_tipoComprobante { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TNSt02_comprobanteEmitidoDtl> TNSt02_comprobanteEmitidoDtl { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TNSt06_medioDePagoDtl> TNSt06_medioDePagoDtl { get; set; }
        public virtual TNSt03_cuota TNSt03_cuota { get; set; }
        public virtual TNSt04_estadoComprobante TNSt04_estadoComprobante { get; set; }
    }
}
