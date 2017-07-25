namespace Common
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using Validation;

    [MetadataType(typeof(PersonaMetadata))]

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

    #region PERSONA METADA

    public class PersonaMetadata
    {
        [Key]
        public long idPersona { get; set; }

        [Required]
        [StringLength(1)]
        public string tipoPersoneria { get; set; }

        [StringLength(300)]
        [Display(Name = "Nombre representante:")]
        public string nombreRepresentante { get; set; }

        [StringLength(100)]
        [Display(Name = "Nombre:")]
        public string nombrePersona { get; set; }

        [StringLength(70)]
        [Display(Name = "Ape. Paterno:")]
        public string apellidoPaterno { get; set; }

        [StringLength(70)]
        [Display(Name = "Ape. Materno:")]
        public string apellidoMaterno { get; set; }

        [Required]
        [StringLength(30)]
        //[IsNumDoc(ErrorMessage = "El número  no es válido. x2")]
        [IsNumDoc]
        [Display(Name = "N° de Documento:")]
        //[AgeValidator(12)]
        //[ExcludeChar("!@.'¿¡!12378}{")]
        //[AgeValidator(10,ErrorMessage ="El campo {0} no es válido")]        
        public string numDocIdentidad { get; set; }

        [StringLength(200)]
        [Display(Name = "Razón Social")]
        public string razonSocial { get; set; }

        [Column(TypeName = "date")]
        [Display(Name = "Fec. de Nacimiento:")]
        //FECHA DE NACIMIENTO:
        //[DataType(DataType.Date, ErrorMessage = "Please enter a valid date (ex: 2/14/2011)")]
        //[MyDate(6)]
        //[Age2Validation(ErrorMessage = "Age must be great then or equal to 18",Minimunage =18)]
        //falata el formato oo
        //[DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:dd/MM/yy H:mm:ss zzz}", ApplyFormatInEditMode = true)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy hh:mm:ss}", ApplyFormatInEditMode = true)]
        [DataType(DataType.DateTime)]
        [AgeTrueValidation("El campo Fecha de Nacimiento debe ser menor a la fecha actual: {0} ")]
        public DateTime? fecNacimiento { get; set; }

        [StringLength(100)]
        [Display(Name = "Nombre de la Vía:")]
        public string nombreVia { get; set; }

        [StringLength(10)]
        [Display(Name = "Número de la Vía:")]
        public string numVia { get; set; }

        [StringLength(100)]
        [Display(Name = "Nombre de la Zona:")]
        public string nombreZona { get; set; }

        [StringLength(120)]
        [Display(Name = "Dirección:")]
        public string direccion01 { get; set; }

        [StringLength(120)]
        [Display(Name = "Dirección opcional:")]
        public string direccion02 { get; set; }

        [StringLength(15)]//,ErrorMessageResourceType = (typeof(Messages)), ErrorMessageResourceName = "FieldMustBeNumeric")]]
        [Display(Name = "N° de Teléfono:")]
        [DataType(DataType.PhoneNumber, ErrorMessage = "No es un número")]
        public string numTelefonico01 { get; set; }

        [StringLength(15)]
        [Display(Name = "N° de Teléfono opcional:")]
        public string numTelefonico02 { get; set; }

        [StringLength(120)]
        [Display(Name = "Email::")]
        [DataType(DataType.EmailAddress)]
        //valida desde el cliente
        //[IsEmailInterno(ErrorMessage = "El dominio no es válido.")]
        //[EsEmailInternoAttribute("sergio@sergio.com", "antonio@antonio.com", ErrorMessage = "El dominio no es válido. x2")]
        public string email01 { get; set; }

        [StringLength(120)]
        [Display(Name = "Email opcional:")]
        [DataType(DataType.EmailAddress)]
        public string email02 { get; set; }

        [StringLength(1)]
        [Display(Name = "Sexo:")]
        public string sexo { get; set; }

        [Display(Name = "Difunto:")]
        public bool difunto { get; set; }

        [Column(TypeName = "date")]
        [Display(Name = "Fec. de Defunción:")]
        public DateTime? fecDefuncion { get; set; }

        [Display(Name = "Foto:")]
        public byte[] pathFoto { get; set; }

        [Display(Name = "Estado:")]
        public bool activo { get; set; }

        [Display(Name = "Fec. de Registro:")]
        public DateTime fecRegistro { get; set; }

        [Display(Name = "Fec. de Modificación:")]
        public DateTime? fecModificacion { get; set; }

        [Display(Name = "Fec. de Eliminación:")]
        public DateTime? fecEliminacion { get; set; }

        [Required]
        [StringLength(128)]
        [Display(Name = "Usuario:")]
        public string idUsuario { get; set; }

        [StringLength(128)]
        [Display(Name = "Usuario Modificar:")]
        public string idUsuarioModificar { get; set; }

        [StringLength(128)]
        [Display(Name = "Usuario Eliminar:")]
        public string idUsuarioEliminar { get; set; }

        [Display(Name = "Vía:")]
        public int? idVia { get; set; }

        [Display(Name = "Zona:")]
        public int? idZona { get; set; }

        [Display(Name = "Tipo de Documento:")]
        public int idTipoDocIdentidad { get; set; }

        [Display(Name = "Distrito:")]
        public int? idDistrito { get; set; }

        [Display(Name = "Estado civil:")]
        public short? idEstadoCivil { get; set; }

        [StringLength(250)]
        [Display(Name = "Observación:")]
        public string obsvPersona { get; set; }
    }
    #endregion






}
