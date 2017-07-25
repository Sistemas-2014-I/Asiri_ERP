namespace Common
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class MSTt05_tipoSucursal
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public MSTt05_tipoSucursal()
        {
            MSTt04_sucursal = new HashSet<MSTt04_sucursal>();
        }

        [Key]
        public short idTipoSucursal { get; set; }

        [Required]
        [StringLength(50)]
        public string descTipoSucursal { get; set; }

        [StringLength(20)]
        public string abrvSurcursal { get; set; }

        public bool activo { get; set; }

        public DateTime fecRegistro { get; set; }

        public DateTime? fecModificacion { get; set; }

        public DateTime? fecEliminacion { get; set; }

        [Required]
        [StringLength(128)]
        public string idUsuario { get; set; }

        [StringLength(128)]
        public string IdUsuarioModificar { get; set; }

        [StringLength(128)]
        public string idUsuarioEliminar { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MSTt04_sucursal> MSTt04_sucursal { get; set; }
    }
}
