namespace Common
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class CLlt10_estudioCompl
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CLlt10_estudioCompl()
        {
            CLIt02_archivoEstudioCompl = new HashSet<CLIt02_archivoEstudioCompl>();
        }

        [Key]
        public long idEstudioCompl { get; set; }

        [Required]
        [StringLength(2500)]
        public string descEstudioCompl { get; set; }

        public long idAtencion { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CLIt02_archivoEstudioCompl> CLIt02_archivoEstudioCompl { get; set; }

        public virtual CLlt03_atencion CLlt03_atencion { get; set; }
    }
}
