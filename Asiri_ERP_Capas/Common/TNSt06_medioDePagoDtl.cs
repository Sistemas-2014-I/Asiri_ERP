namespace Common
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class TNSt06_medioDePagoDtl
    {
        [Key]
        public long idMedioDePagoDtl { get; set; }

        public DateTime fecRegistro { get; set; }

        public DateTime? fecModificacion { get; set; }

        public DateTime? fecEliminacion { get; set; }

        public decimal mtoMedioDePago { get; set; }

        public decimal tipoDeCambio { get; set; }

        public bool activo { get; set; }

        [Required]
        [StringLength(128)]
        public string idUsuario { get; set; }

        [StringLength(128)]
        public string idUsuarioModificar { get; set; }

        [StringLength(128)]
        public string idUsuarioEliminar { get; set; }

        public long idComprobanteEmitido { get; set; }

        public short idMedioDePago { get; set; }

        public int idMoneda { get; set; }

        public virtual MSTt01_medioDePago MSTt01_medioDePago { get; set; }

        public virtual SNTt03_moneda SNTt03_moneda { get; set; }

        public virtual TNSt01_comprobanteEmitido TNSt01_comprobanteEmitido { get; set; }
    }
}
