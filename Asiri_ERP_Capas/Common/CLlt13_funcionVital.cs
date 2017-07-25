namespace Common
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class CLlt13_funcionVital
    {
        [Key]
        public long idFuncionVital { get; set; }

        [StringLength(50)]
        public string sistole { get; set; }

        [StringLength(50)]
        public string diastole { get; set; }

        [StringLength(50)]
        public string pulsacion { get; set; }

        [StringLength(50)]
        public string ritmoRespiratorio { get; set; }

        [StringLength(50)]
        public string temperatura { get; set; }

        [StringLength(50)]
        public string altura { get; set; }

        [StringLength(50)]
        public string peso { get; set; }

        [StringLength(50)]
        public string imc { get; set; }

        public long idAtencion { get; set; }

        public virtual CLlt03_atencion CLlt03_atencion { get; set; }
    }
}
