namespace Common
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class RHUt13_tipoEspecialidad
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public RHUt13_tipoEspecialidad()
        {
            RHUt04_especialidad = new HashSet<RHUt04_especialidad>();
        }

        [Key]
        public int idTipoDeEspeciliadad { get; set; }

        [Required]
        [StringLength(200)]
        public string nombreEspecialidad { get; set; }

        [StringLength(300)]
        public string descEspecialidad { get; set; }

        [StringLength(40)]
        public string abrvTipoEspecialidad { get; set; }

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
        public virtual ICollection<RHUt04_especialidad> RHUt04_especialidad { get; set; }
    }
}
