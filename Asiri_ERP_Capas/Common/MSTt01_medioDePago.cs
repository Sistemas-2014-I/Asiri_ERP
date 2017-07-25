namespace Common
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class MSTt01_medioDePago
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public MSTt01_medioDePago()
        {
            TNSt06_medioDePagoDtl = new HashSet<TNSt06_medioDePagoDtl>();
            RHUt01_empleado = new HashSet<RHUt01_empleado>();
        }

        [Key]
        public short idMedioDePago { get; set; }

        [Required]
        [StringLength(300)]
        public string descMedioDePago { get; set; }

        [StringLength(20)]
        public string abrvMedioDePago { get; set; }

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

        public short idTipoMedioDePago { get; set; }

        public bool medioDePagoRapido { get; set; }

        [StringLength(260)]
        public string photoPath { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TNSt06_medioDePagoDtl> TNSt06_medioDePagoDtl { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RHUt01_empleado> RHUt01_empleado { get; set; }

        public virtual SNTt05_tipoMedioDePago SNTt05_tipoMedioDePago { get; set; }
    }
}
