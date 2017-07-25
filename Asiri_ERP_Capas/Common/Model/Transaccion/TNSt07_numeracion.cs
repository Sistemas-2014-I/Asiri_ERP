namespace Common.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class TNSt07_numeracion
    {
        [Key]
        public int idNumeracion { get; set; }

        [Required]
        [StringLength(10)]
        public string serie { get; set; }

        [Required]
        [StringLength(10)]
        public string num_corr_inicial { get; set; }

        [Required]
        [StringLength(10)]
        public string num_corr_final { get; set; }

        [Required]
        [StringLength(10)]
        public string num_corr_actual { get; set; }

        public int idTipoComprobante { get; set; }

        public int idSucursal { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime fecRegistro { get; set; }

        [Required]
        [StringLength(128)]
        public string idUsuario { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? fecModificacion { get; set; }

        [StringLength(128)]
        public string idUsuarioModificar { get; set; }
    }
}
