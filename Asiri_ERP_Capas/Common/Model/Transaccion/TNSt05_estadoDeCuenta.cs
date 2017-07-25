namespace Common.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class TNSt05_estadoDeCuenta
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TNSt05_estadoDeCuenta()
        {
            TNSt03_cuota = new HashSet<TNSt03_cuota>();
        }

        [Key]
        public long idEstadoDeCuenta { get; set; }

        public decimal porcentajeDescuento { get; set; }

        public decimal mtoDescuento { get; set; }

        public decimal mtoSubtotal { get; set; }

        public decimal debe { get; set; }

        public decimal haber { get; set; }

        public decimal saldo { get; set; }

        public decimal inicial { get; set; }

        public decimal porcentajeInteres { get; set; }

        public decimal mtoInteres { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime fecRegistro { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? fecModificacion { get; set; }

        [Required]
        [StringLength(128)]
        public string idUsuario { get; set; }

        [StringLength(128)]
        public string idUsuarioModificar { get; set; }

        public long idCita { get; set; }

        public virtual CLlt05_cita CLlt05_cita { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TNSt03_cuota> TNSt03_cuota { get; set; }
    }
}
