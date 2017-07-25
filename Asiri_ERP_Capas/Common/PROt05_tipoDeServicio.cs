namespace Common
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class PROt05_tipoDeServicio
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PROt05_tipoDeServicio()
        {
            PROt04_servicio = new HashSet<PROt04_servicio>();
        }

        [Key]
        public int idTipoDeServicio { get; set; }

        [Required(ErrorMessage = "Debe agregar una descripción al tipo de servicio")]
        [StringLength(100, ErrorMessage = "El campo {0} no puede tener más de {1} caracteres")]
        [Display(Name = "Descripción")]
        public string descTipoDeServicio { get; set; }
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

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PROt04_servicio> PROt04_servicio { get; set; }
    }
}
