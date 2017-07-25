namespace Common.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class RHUt09_persona
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public RHUt09_persona()
        {
            RHUt01_empleado = new HashSet<RHUt01_empleado>();
            RHUt07_paciente = new HashSet<RHUt07_paciente>();
            RHUt10_personaRedSocial = new HashSet<RHUt10_personaRedSocial>();
        }

        [Key]
        public long idPersona { get; set; }

        [Required]
        [StringLength(1)]
        public string tipoPersoneria { get; set; }

        [StringLength(300)]
        public string nombreRepresentante { get; set; }

        [StringLength(100)]
        public string nombrePersona { get; set; }

        [StringLength(70)]
        public string apellidoPaterno { get; set; }

        [StringLength(70)]
        public string apellidoMaterno { get; set; }

        [Required]
        [StringLength(30)]
        public string numDocIdentidad { get; set; }

        [StringLength(200)]
        public string razonSocial { get; set; }

        [Column(TypeName = "date")]
        public DateTime? fecNacimiento { get; set; }

        [StringLength(100)]
        public string nombreVia { get; set; }

        [StringLength(10)]
        public string numVia { get; set; }

        [StringLength(100)]
        public string nombreZona { get; set; }

        [StringLength(120)]
        public string direccion01 { get; set; }

        [StringLength(120)]
        public string direccion02 { get; set; }

        [StringLength(15)]
        public string numTelefonico01 { get; set; }

        [StringLength(15)]
        public string numTelefonico02 { get; set; }

        [StringLength(120)]
        public string email01 { get; set; }

        [StringLength(120)]
        public string email02 { get; set; }

        [StringLength(1)]
        public string sexo { get; set; }

        public bool difunto { get; set; }

        [Column(TypeName = "date")]
        public DateTime? fecDefuncion { get; set; }

        public byte[] pathFoto { get; set; }

        public bool activo { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime fecRegistro { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? fecModificacion { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? fecEliminacion { get; set; }

        [Required]
        [StringLength(128)]
        public string idUsuario { get; set; }

        [StringLength(128)]
        public string idUsuarioModificar { get; set; }

        [StringLength(128)]
        public string idUsuarioEliminar { get; set; }

        public int? idVia { get; set; }

        public int? idZona { get; set; }

        public int idTipoDocIdentidad { get; set; }

        public int? idDistrito { get; set; }

        public short? idEstadoCivil { get; set; }

        [StringLength(250)]
        public string obsvPersona { get; set; }

        public bool esOnline { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RHUt01_empleado> RHUt01_empleado { get; set; }

        public virtual RHUt05_estadoCivil RHUt05_estadoCivil { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RHUt07_paciente> RHUt07_paciente { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RHUt10_personaRedSocial> RHUt10_personaRedSocial { get; set; }

        public virtual RHUt12_tipoDocIdentidad RHUt12_tipoDocIdentidad { get; set; }

        public virtual UBIt01_distrito UBIt01_distrito { get; set; }

        public virtual UBIt04_via UBIt04_via { get; set; }

        public virtual UBIt05_zona UBIt05_zona { get; set; }
    }
}
