namespace Common.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class RHUt06_horario
    {
        [Key]
        public long idHorario { get; set; }

        public bool laboraLunes { get; set; }

        public bool laboraMartes { get; set; }

        public bool laboraMiercoles { get; set; }

        public bool laboraJueves { get; set; }

        public bool laboraViernes { get; set; }

        public bool laboraSabado { get; set; }

        public bool laboraDomingo { get; set; }

        [StringLength(12)]
        public string horaInicioLunes { get; set; }

        [StringLength(12)]
        public string horaFinLunes { get; set; }

        [StringLength(12)]
        public string horaInicioMartes { get; set; }

        [StringLength(12)]
        public string horaFinMartes { get; set; }

        [StringLength(12)]
        public string horaInicioMiercoles { get; set; }

        [StringLength(12)]
        public string horaFinMiercoles { get; set; }

        [StringLength(12)]
        public string horaInicioJueves { get; set; }

        [StringLength(12)]
        public string horaFinJueves { get; set; }

        [StringLength(12)]
        public string horaInicioViernes { get; set; }

        [StringLength(12)]
        public string horaFinViernes { get; set; }

        [StringLength(12)]
        public string horaInicioSabado { get; set; }

        [StringLength(12)]
        public string horaFinSabado { get; set; }

        [StringLength(12)]
        public string horaInicioDomingo { get; set; }

        [StringLength(12)]
        public string horaFinDomingo { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime fecRegistro { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? fecModificacion { get; set; }

        [Required]
        [StringLength(128)]
        public string idUsuario { get; set; }

        [StringLength(128)]
        public string idUsuarioModificar { get; set; }

        public long idEmpleado { get; set; }

        public virtual RHUt01_empleado RHUt01_empleado { get; set; }
    }
}
