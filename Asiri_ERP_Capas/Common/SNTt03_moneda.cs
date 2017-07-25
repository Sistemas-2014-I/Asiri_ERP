namespace Common
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class SNTt03_moneda
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SNTt03_moneda()
        {
            PROt02_producto = new HashSet<PROt02_producto>();
            TNSt06_medioDePagoDtl = new HashSet<TNSt06_medioDePagoDtl>();
            TNSt01_comprobanteEmitido = new HashSet<TNSt01_comprobanteEmitido>();
        }

        [Key]
        public int idMoneda { get; set; }

        [StringLength(10)]
        public string codMoneda { get; set; }

        [StringLength(5)]
        public string codIsoMoneda { get; set; }

        [StringLength(5)]
        public string codIsoNumMoneda { get; set; }

        [Required]
        [StringLength(100)]
        public string descMoneda { get; set; }

        [StringLength(10)]
        public string abrvMoneda { get; set; }

        [Required]
        [StringLength(5)]
        public string simbolo { get; set; }

        public byte decimalCambio { get; set; }

        [StringLength(50)]
        public string fraccion { get; set; }

        public bool activo { get; set; }

        public DateTime fecRegistro { get; set; }

        public DateTime? fecModificacion { get; set; }

        public DateTime? fecEliminacion { get; set; }

        [Required]
        [StringLength(128)]
        public string idUsuario { get; set; }

        [StringLength(128)]
        public string idUsuarioModificar { get; set; }

        [StringLength(128)]
        public string idUsuarioEliminar { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PROt02_producto> PROt02_producto { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TNSt06_medioDePagoDtl> TNSt06_medioDePagoDtl { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TNSt01_comprobanteEmitido> TNSt01_comprobanteEmitido { get; set; }
    }
}
