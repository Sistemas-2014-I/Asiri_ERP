namespace Common.Model
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

        [Required]
        [StringLength(20)]
        public string codServicio { get; set; }

        [Required]
        [StringLength(200)]
        public string nombreServicio { get; set; }

        [StringLength(200)]
        public string descServicio { get; set; }

        public decimal precio { get; set; }

        public decimal? precioMinimo { get; set; }

        public decimal? precioMaximo { get; set; }

        public bool esGratis { get; set; }

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

        public int idTipoDeServicio { get; set; }

        public int idEspecialidad { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CLlt06_citaDtl> CLlt06_citaDtl { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CLlt17_tratamientoDtl> CLlt17_tratamientoDtl { get; set; }

        public virtual PROt05_tipoDeServicio PROt05_tipoDeServicio { get; set; }

        public virtual RHUt04_especialidad RHUt04_especialidad { get; set; }
    }
}
