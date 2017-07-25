namespace Common
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class RHUt14_turno
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public RHUt14_turno()
        {
            RHUt03_empleadoTurno = new HashSet<RHUt03_empleadoTurno>();
        }

        [Key]
        public short idTurno { get; set; }

        [Required]
        [StringLength(50)]
        public string descTurno { get; set; }

        [StringLength(10)]
        public string abrvTurno { get; set; }

        [Column(TypeName = "date")]
        public DateTime horaInicio { get; set; }

        [Column(TypeName = "date")]
        public DateTime horaFin { get; set; }

        public bool activo { get; set; }

        public DateTime fecRegistro { get; set; }

        public DateTime? fecModficiacion { get; set; }

        public DateTime? fecEliminacion { get; set; }

        [Required]
        [StringLength(128)]
        public string idUsuario { get; set; }

        [StringLength(128)]
        public string idUsuarioModificar { get; set; }

        [StringLength(128)]
        public string idUsuarioEliminar { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RHUt03_empleadoTurno> RHUt03_empleadoTurno { get; set; }
    }
}
