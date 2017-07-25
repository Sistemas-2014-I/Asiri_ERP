namespace Common.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class RHUt05_estadoCivil
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public RHUt05_estadoCivil()
        {
            RHUt09_persona = new HashSet<RHUt09_persona>();
        }

        [Key]
        public short idEstadoCivil { get; set; }

        [Required]
        [StringLength(50)]
        public string descEstadoCivil { get; set; }

        [StringLength(20)]
        public string abrvEstadoCivil { get; set; }

        public bool activo { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime fecRegistro { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? fecModificacion { get; set; }

        [Required]
        [StringLength(128)]
        public string idUsuario { get; set; }

        [StringLength(128)]
        public string idUsuarioModificar { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RHUt09_persona> RHUt09_persona { get; set; }
    }
}
