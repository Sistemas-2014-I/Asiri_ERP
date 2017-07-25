namespace Common.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class TNSt01_comprobanteEmitido
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TNSt01_comprobanteEmitido()
        {
            TNSt02_comprobanteEmitidoDtl = new HashSet<TNSt02_comprobanteEmitidoDtl>();
            TNSt06_medioDePagoDtl = new HashSet<TNSt06_medioDePagoDtl>();
        }

        [Key]
        public long idComprobanteEmitido { get; set; }

        [Required]
        [StringLength(10)]
        public string serie { get; set; }

        [Required]
        [StringLength(10)]
        public string numComprobanteEmitido { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime fecRegistro { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? fecModificacion { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? fecAnulacion { get; set; }

        [StringLength(300)]
        public string razonAnulacion { get; set; }

        public bool esAnulado { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime fecEmision { get; set; }

        [Column(TypeName = "date")]
        public DateTime? fecVencimiento { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? fecCancelacion { get; set; }

        public decimal mtoTotal { get; set; }

        public decimal mtoSubTotal { get; set; }

        public decimal mtoImpto { get; set; }

        public decimal mtoDescuento { get; set; }

        public decimal porcentajeImpto { get; set; }

        public decimal porcentajeDescuento { get; set; }

        [StringLength(250)]
        public string obsvComprobanteEmitido { get; set; }

        [StringLength(250)]
        public string info01 { get; set; }

        [StringLength(250)]
        public string info02 { get; set; }

        [StringLength(250)]
        public string info03 { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? fecha01 { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? fecha02 { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? fecha03 { get; set; }

        public long idCita { get; set; }

        public short? idEstadoComprobante { get; set; }

        public int idSucursal { get; set; }

        public int idTipoComprobante { get; set; }

        public int idMoneda { get; set; }

        public short idImpto { get; set; }

        [Required]
        [StringLength(128)]
        public string idUsuario { get; set; }

        [StringLength(128)]
        public string idUsuarioModificar { get; set; }

        [StringLength(128)]
        public string idUsuarioAnular { get; set; }

        public long? idCuota { get; set; }

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
