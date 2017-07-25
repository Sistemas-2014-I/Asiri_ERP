namespace Common
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class TNSt02_comprobanteEmitidoDtl
    {
        [Key]
        public long idComprobanteEmitidoDtl { get; set; }

        public decimal precio { get; set; }

        public decimal cantidad { get; set; }

        public long idComprobanteEmitido { get; set; }

        public long idCitaDtl { get; set; }

        public virtual CLlt06_citaDtl CLlt06_citaDtl { get; set; }

        public virtual TNSt01_comprobanteEmitido TNSt01_comprobanteEmitido { get; set; }
    }
}
