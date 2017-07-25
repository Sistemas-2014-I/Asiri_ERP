namespace Common
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class PROt01_categoria
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PROt01_categoria()
        {
            PROt02_producto = new HashSet<PROt02_producto>();
        }

        [Key]
        public int idCategoria { get; set; }

        [Column(TypeName = "datetime2")]
        [Display(Name = "Fecha de registro")]
        public DateTime fecRegistro { get; set; }

        [Column(TypeName = "datetime2")]
        [Display(Name = "Fecha de modificación")]
        public DateTime? fecModificacion { get; set; }

        [Column(TypeName = "datetime2")]
        [Display(Name = "Fecha de eliminación")]
        public DateTime? fecEliminacion { get; set; }

 
        [StringLength(128)]
        public string idUsuario { get; set; }

        [StringLength(128)]
        public string idUsuarioModificar { get; set; }

        [StringLength(128)]
        public string idUsuarioEliminar { get; set; }

        [Required(ErrorMessage = "Debe ingresar un nombre")]
        [StringLength(70, ErrorMessage = "El campo {0} no puede tener más de {1} caracteres")]
        [Display(Name = "Nombre")]
        public string nombreCategoria { get; set; }
        [StringLength(500, ErrorMessage = "El campo {0} no puede tener más de {1} caracteres")]
        [Display(Name = "Descripción")]
        public string descCategoria { get; set; }
        [Display(Name = "Estado")]
        public bool activo { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PROt02_producto> PROt02_producto { get; set; }
    }
}
