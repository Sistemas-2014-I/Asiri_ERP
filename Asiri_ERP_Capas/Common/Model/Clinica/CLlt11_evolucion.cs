namespace Common.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class CLlt11_evolucion
    {
        [Key]
        public long idEvolucion { get; set; }

        [Required]
        [StringLength(2500)]
        public string descEvolucion { get; set; }

        public bool? pesimo { get; set; }

        public bool? malo { get; set; }

        public bool? regular { get; set; }

        public bool? bueno { get; set; }

        public bool? excelente { get; set; }

        public long idAtencion { get; set; }

        public virtual CLlt03_atencion CLlt03_atencion { get; set; }
    }
}
