namespace Common
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class CLlt08_diagnostico
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CLlt08_diagnostico()
        {
            CLlt16_tratamiento = new HashSet<CLlt16_tratamiento>();
        }

        [Key]
        public long idDiagnostico { get; set; }

        [Required]
        [StringLength(2500)]
        public string descDiagnostico { get; set; }

        public long idAtencion { get; set; }

        public int? idCie10 { get; set; }

        public virtual CLlt03_atencion CLlt03_atencion { get; set; }

        public virtual CLlt04_cie10 CLlt04_cie10 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CLlt16_tratamiento> CLlt16_tratamiento { get; set; }
    }
}
