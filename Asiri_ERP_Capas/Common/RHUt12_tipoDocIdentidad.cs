namespace Common
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class RHUt12_tipoDocIdentidad
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public RHUt12_tipoDocIdentidad()
        {
            RHUt09_persona = new HashSet<RHUt09_persona>();
        }

        [Key]
        public int idTipoDocIdentidad { get; set; }

        [Required]
        [StringLength(10)]
        public string codTipoDocIdentidad { get; set; }

        [Required]
        [StringLength(100)]
        public string descTipoDocIdentidad { get; set; }

        [Required]
        [StringLength(20)]
        public string abrvTipoDocIdentidad { get; set; }

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
        public virtual ICollection<RHUt09_persona> RHUt09_persona { get; set; }
    }
}
