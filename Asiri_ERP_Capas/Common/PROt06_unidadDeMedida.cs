namespace Common
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class PROt06_unidadDeMedida
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PROt06_unidadDeMedida()
        {
            PROt03_productoUnidadDeMedida = new HashSet<PROt03_productoUnidadDeMedida>();
        }

        [Key]
        public int idUnidadDeMedida { get; set; }

        [Required]
        [StringLength(10)]
        public string codUnidadDeMedida { get; set; }

        [Required]
        [StringLength(50)]
        public string descUnidadDeMedida { get; set; }

        public bool activo { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? fecRegistro { get; set; }

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

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PROt03_productoUnidadDeMedida> PROt03_productoUnidadDeMedida { get; set; }
    }
}
