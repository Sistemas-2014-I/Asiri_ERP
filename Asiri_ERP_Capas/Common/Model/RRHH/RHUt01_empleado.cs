namespace Common.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class RHUt01_empleado
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public RHUt01_empleado()
        {
            CLlt05_cita = new HashSet<CLlt05_cita>();
            RHUt02_empleadoEspecialidad = new HashSet<RHUt02_empleadoEspecialidad>();
            RHUt03_empleadoTurno = new HashSet<RHUt03_empleadoTurno>();
            RHUt06_horario = new HashSet<RHUt06_horario>();
        }

        [Key]
        public long idEmpleado { get; set; }

        [StringLength(20)]
        public string codEmpleado { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime fecRegistro { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? fecModificacion { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? fecEliminacion { get; set; }

        [Column(TypeName = "date")]
        public DateTime? fecIngreso { get; set; }

        [Column(TypeName = "date")]
        public DateTime? fecCese { get; set; }

        [StringLength(10)]
        public string horasPorPeriodo { get; set; }

        public decimal? mtoRemuneracion { get; set; }

        public bool esHorarioTurno { get; set; }

        public bool activo { get; set; }

        [Required]
        [StringLength(128)]
        public string idUsuario { get; set; }

        [StringLength(128)]
        public string idUsuarioModificar { get; set; }

        [StringLength(128)]
        public string idUsuarioEliminar { get; set; }

        public int idEntidadFinanciera { get; set; }

        public int idTipoDeEmpleado { get; set; }

        public long? idPersona { get; set; }

        public long idPeriodoRemuneracion { get; set; }

        public short idMedioDePago { get; set; }

        public short idConsultorio { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CLlt05_cita> CLlt05_cita { get; set; }

        public virtual CLlt07_consultorio CLlt07_consultorio { get; set; }

        public virtual MSTt01_medioDePago MSTt01_medioDePago { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RHUt02_empleadoEspecialidad> RHUt02_empleadoEspecialidad { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RHUt03_empleadoTurno> RHUt03_empleadoTurno { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RHUt06_horario> RHUt06_horario { get; set; }

        public virtual RHUt08_periodoRemuneracion RHUt08_periodoRemuneracion { get; set; }

        public virtual RHUt09_persona RHUt09_persona { get; set; }

        public virtual RHUt11_tipoDeEmpleado RHUt11_tipoDeEmpleado { get; set; }

        public virtual SNTt01_entidadFinanciera SNTt01_entidadFinanciera { get; set; }
    }
}
