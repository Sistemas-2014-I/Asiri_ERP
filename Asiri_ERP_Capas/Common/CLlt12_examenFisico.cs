namespace Common
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class CLlt12_examenFisico
    {
        [Key]
        public long idExamenFisico { get; set; }

        [Required]
        [StringLength(2500)]
        public string descExamenFisico { get; set; }

        public long idAtencion { get; set; }

        public virtual CLlt03_atencion CLlt03_atencion { get; set; }
    }
}
