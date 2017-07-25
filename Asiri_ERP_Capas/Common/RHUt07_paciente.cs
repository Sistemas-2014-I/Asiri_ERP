namespace Common
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class RHUt07_paciente
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public RHUt07_paciente()
        {
            CLlt05_cita = new HashSet<CLlt05_cita>();
            CLlt15_seguroPaciente = new HashSet<CLlt15_seguroPaciente>();
        }

        [Key]
        public long idPaciente { get; set; }

        [StringLength(20)]
        public string codPaciente { get; set; }

        [Required]
        [StringLength(15)]
        public string numHistoriaClinica { get; set; }

        [StringLength(10)]
        public string grupoSanguineo { get; set; }

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

        public long? idPersona { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CLlt05_cita> CLlt05_cita { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CLlt15_seguroPaciente> CLlt15_seguroPaciente { get; set; }

        public virtual RHUt09_persona RHUt09_persona { get; set; }
    }
}
