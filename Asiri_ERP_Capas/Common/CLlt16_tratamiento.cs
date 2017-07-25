namespace Common
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class CLlt16_tratamiento
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CLlt16_tratamiento()
        {
            CLlt17_tratamientoDtl = new HashSet<CLlt17_tratamientoDtl>();
        }

        [Key]
        public long idTratamiento { get; set; }

        [Required]
        [StringLength(2500)]
        public string descTratamiento { get; set; }

        public long? idDiagnostico { get; set; }

        public virtual CLlt08_diagnostico CLlt08_diagnostico { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CLlt17_tratamientoDtl> CLlt17_tratamientoDtl { get; set; }
    }
}
