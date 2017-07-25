namespace Common.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class CLlt05_cita
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CLlt05_cita()
        {
            CLlt03_atencion = new HashSet<CLlt03_atencion>();
            CLlt06_citaDtl = new HashSet<CLlt06_citaDtl>();
            TNSt05_estadoDeCuenta = new HashSet<TNSt05_estadoDeCuenta>();
            TNSt01_comprobanteEmitido = new HashSet<TNSt01_comprobanteEmitido>();
        }

        [Key]
        public long idCita { get; set; }

        [Required]
        [StringLength(10)]
        public string codCita { get; set; }

        [Column(TypeName = "date")]
        public DateTime fecCita { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime fecRegistro { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? fecModificacion { get; set; }

        [Required]
        [StringLength(12)]
        public string horaInicio { get; set; }

        [Required]
        [StringLength(10)]
        public string duracionEstimada { get; set; }

        public bool esReprogramado { get; set; }

        public byte numReprogramacion { get; set; }

        public bool esOnline { get; set; }

        public bool esCerrado { get; set; }

        public long? idCitaPadre { get; set; }

        [StringLength(250)]
        public string obsvCita { get; set; }

        public decimal mtoTotal { get; set; }

        public short idEstadoCita { get; set; }

        public long idPaciente { get; set; }

        public long idEmpleado { get; set; }

        [Required]
        [StringLength(128)]
        public string idUsuario { get; set; }

        [StringLength(128)]
        public string idUsuarioModificar { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CLlt03_atencion> CLlt03_atencion { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CLlt06_citaDtl> CLlt06_citaDtl { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TNSt05_estadoDeCuenta> TNSt05_estadoDeCuenta { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TNSt01_comprobanteEmitido> TNSt01_comprobanteEmitido { get; set; }

        public virtual CLlt09_estadoCita CLlt09_estadoCita { get; set; }

        public virtual RHUt01_empleado RHUt01_empleado { get; set; }

        public virtual RHUt07_paciente RHUt07_paciente { get; set; }
    }
}
