namespace Common
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class PROt04_servicio
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PROt04_servicio()
        {
            CLlt06_citaDtl = new HashSet<CLlt06_citaDtl>();
            CLlt17_tratamientoDtl = new HashSet<CLlt17_tratamientoDtl>();
        }

        [Key]
        public long idServicio { get; set; }
        
        [Display(Name = "Código")]
        public string codServicio { get; set; }

        [Required(ErrorMessage = "El nombre es requerido")]
        [Display(Name = "Nombre")]
        [StringLength(200, ErrorMessage = "El campo {0} no puede tener más de {1} caracteres")]
        public string nombreServicio { get; set; }

        [StringLength(200, ErrorMessage = "El campo {0} no puede tener más de {1} caracteres")]
        [Display(Name = "Descripción")]
        public string descServicio { get; set; }
        [Display(Name = "Precio")]
        [Range(0.0, 99999999999, ErrorMessage = "Por favor ingresar un número válido")]
        [Required(ErrorMessage = "El precio el requerido")]
        //[GreaterThanOrEqualTo("precioMinimo", ErrorMessage = "El precio no puede ser menor al precio mínimo")]
        public decimal precio { get; set; }
        [Display(Name = "Precio Mínimo")]
        [Range(0.0, 99999999999, ErrorMessage = "Por favor ingresar un número válido")]
        public decimal? precioMinimo { get; set; }
        //[RegularExpression("^[0-9]+([.][0-9]+)?$", ErrorMessage = "Formato numérico incorrecto (use . como separador decimal)")]
        [Display(Name = "Precio Máximo")]
        [Range(0.0, 99999999999, ErrorMessage = "Por favor ingresar un número válido")]
        public decimal? precioMaximo { get; set; }
        [Display(Name = "Gratis")]
        public bool esGratis { get; set; }
        [Display(Name = "Estado")]
        public bool activo { get; set; }
        [Display(Name = "Fecha de registro")]
        public DateTime fecRegistro { get; set; }
        [Display(Name = "Fecha de modificación")]
        public DateTime? fecModificacion { get; set; }
        [Display(Name = "Fecha de eliminación")]
        public DateTime? fecEliminacion { get; set; }
        [StringLength(128)]
        public string idUsuario { get; set; }

        [StringLength(128)]
        public string idUsuarioModificar { get; set; }

        [StringLength(128)]
        public string idUsuarioEliminar { get; set; }
        [Required(ErrorMessage = "Debe seleccionar un tipo de servicio")]
        public int idTipoDeServicio { get; set; }

        [Required(ErrorMessage = "Debe seleccionar una especialidad")]
        public int idEspecialidad { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CLlt06_citaDtl> CLlt06_citaDtl { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CLlt17_tratamientoDtl> CLlt17_tratamientoDtl { get; set; }

        public virtual PROt05_tipoDeServicio PROt05_tipoDeServicio { get; set; }

        public virtual RHUt04_especialidad RHUt04_especialidad { get; set; }
    }
}
