namespace Common.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class CLIt01_anamnesis
    {
        [Key]
        public long idAnamnesis { get; set; }

        [Required]
        [StringLength(2500)]
        public string descAnamnesis { get; set; }

        public long idAtencion { get; set; }

        public virtual CLlt03_atencion CLlt03_atencion { get; set; }
    }
}
