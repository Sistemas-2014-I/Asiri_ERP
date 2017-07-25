namespace Common.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class MSTt04_sucursal
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public MSTt04_sucursal()
        {
            TNSt01_comprobanteEmitido = new HashSet<TNSt01_comprobanteEmitido>();
        }

        [Key]
        public int idSucursal { get; set; }

        [Required]
        [StringLength(100)]
        public string descSucursal { get; set; }

        [Required]
        [StringLength(200)]
        public string direccion { get; set; }

        [Required]
        [StringLength(15)]
        public string numTelefonico01 { get; set; }

        [StringLength(15)]
        public string numTelefonico02 { get; set; }

        [StringLength(250)]
        public string obsvSucursal { get; set; }

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

        public short idTipoSucursal { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TNSt01_comprobanteEmitido> TNSt01_comprobanteEmitido { get; set; }

        public virtual MSTt05_tipoSucursal MSTt05_tipoSucursal { get; set; }
    }
}
