namespace Common
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class SISt01_parametro
    {
        [Key]
        public int idParametro { get; set; }

        [Required]
        [StringLength(100)]
        public string codParametro { get; set; }

        [Required]
        [StringLength(50)]
        public string nombreParametro { get; set; }

        [StringLength(250)]
        public string descParametro { get; set; }

        [StringLength(250)]
        public string valorDeTexto { get; set; }

        public decimal? valorNumerico { get; set; }

        [StringLength(250)]
        public string valorTextoDefault { get; set; }

        public decimal? valorNumericoDefault { get; set; }

        public DateTime? fecModificacion { get; set; }

        [StringLength(128)]
        public string idUsuarioModificar { get; set; }
    }
}
