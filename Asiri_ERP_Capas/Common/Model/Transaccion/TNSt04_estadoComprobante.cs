namespace Common.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class TNSt04_estadoComprobante
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TNSt04_estadoComprobante()
        {
            TNSt01_comprobanteEmitido = new HashSet<TNSt01_comprobanteEmitido>();
        }

        [Key]
        public short idEstadoComprobante { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime fecRegistro { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? fecModificacion { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? fecEliminacion { get; set; }

        [Required]
        [StringLength(20)]
        public string nombreEstadoComprobante { get; set; }

        [StringLength(300)]
        public string descEstadoComprobante { get; set; }

        public bool activo { get; set; }

        [Required]
        [StringLength(128)]
        public string idUsuario { get; set; }

        [StringLength(128)]
        public string idUsuarioModificar { get; set; }

        [StringLength(128)]
        public string idUsuarioEliminar { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TNSt01_comprobanteEmitido> TNSt01_comprobanteEmitido { get; set; }
    }
}
