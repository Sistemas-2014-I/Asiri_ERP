namespace Common.Model
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
        public long idProducto { get; set; }

        [Required]
        [StringLength(20)]
        public string codProducto { get; set; }

        [StringLength(30)]
        public string codBarra { get; set; }

        [StringLength(200)]
        public string nombreProductoGenerico { get; set; }

        [Required]
        [StringLength(200)]
        public string nombreProductoComercial { get; set; }

        [StringLength(300)]
        public string descProducto { get; set; }

        [StringLength(1000)]
        public string obsvProducto { get; set; }

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
