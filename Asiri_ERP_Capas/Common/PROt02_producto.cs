namespace Common
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class PROt02_producto
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PROt02_producto()
        {
            CLlt17_tratamientoDtl = new HashSet<CLlt17_tratamientoDtl>();
            PROt03_productoUnidadDeMedida = new HashSet<PROt03_productoUnidadDeMedida>();
        }

        [Key]
        [Display(Name = "ID")]
        public long idProducto { get; set; }

        //[StringLength(20, ErrorMessage = "El campo {0} no puede tener más de {1} caracteres")]
        //[RegularExpression("^[a-zA-Z0-9_.-]*$", ErrorMessage = "El formato es incorrecto (evite el uso de carateres especiales)")]
        [Display(Name = "Código")]
        public string codProducto { get; set; }
        
        [Display(Name = "Código de barra")]
        public string codBarra { get; set; }

        [StringLength(200, ErrorMessage = "El campo {0} no puede tener más de {1} caracteres")]
        [Display(Name = "Nombre Genérico")]
        public string nombreProductoGenerico { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [StringLength(200, ErrorMessage = "El campo {0} no puede tener más de {1} caracteres")]
        [Display(Name = "Nombre Comercial")]
        public string nombreProductoComercial { get; set; }

        [StringLength(300, ErrorMessage = "El campo {0} no puede tener más de {1} caracteres")]
        [Display(Name = "Descripción")]
        public string descProducto { get; set; }

        [StringLength(1000, ErrorMessage = "El campo {0} no puede tener más de {1} caracteres")]
        [Display(Name = "Observación")]
        public string obsvProducto { get; set; }

        [Display(Name = "Estado")]
        public bool activo { get; set; }

        [Display(Name = "Fecha de registro")]
        [DataType(DataType.Date)]
        [Column(TypeName = "datetime2")]
        public DateTime fecRegistro { get; set; }

        [Display(Name = "Fecha de modificación")]
        [DataType(DataType.Date)]
        [Column(TypeName = "datetime2")]
        public DateTime? fecModificacion { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Fecha de eliminación")]
        [Column(TypeName = "datetime2")]
        public DateTime? fecEliminacion { get; set; }

        [StringLength(128)]
        public string idUsuario { get; set; }

        [StringLength(128)]
        public string idUsuarioModificar { get; set; }

        [StringLength(128)]
        public string idUsuarioEliminar { get; set; }
        [Display(Name = "Categoría")]
        [Required(ErrorMessage = "Debe seleccionar una categoría")]
        public int idCategoria { get; set; }

        public int idMoneda { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CLlt17_tratamientoDtl> CLlt17_tratamientoDtl { get; set; }

        public virtual PROt01_categoria PROt01_categoria { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PROt03_productoUnidadDeMedida> PROt03_productoUnidadDeMedida { get; set; }

        public virtual SNTt03_moneda SNTt03_moneda { get; set; }
    }
}
