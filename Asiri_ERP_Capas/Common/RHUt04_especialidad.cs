namespace Common
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class RHUt04_especialidad
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public RHUt04_especialidad()
        {
            PROt04_servicio = new HashSet<PROt04_servicio>();
            RHUt02_empleadoEspecialidad = new HashSet<RHUt02_empleadoEspecialidad>();
        }

        [Key]
        public int idEspecialidad { get; set; }

        [Required]
        [StringLength(70)]
        public string nombreEspecialidad { get; set; }

        [StringLength(40)]
        public string abrvEspecialidad { get; set; }

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

        public int idTipoDeEspeciliadad { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PROt04_servicio> PROt04_servicio { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RHUt02_empleadoEspecialidad> RHUt02_empleadoEspecialidad { get; set; }

        public virtual RHUt13_tipoEspecialidad RHUt13_tipoEspecialidad { get; set; }
    }
}
