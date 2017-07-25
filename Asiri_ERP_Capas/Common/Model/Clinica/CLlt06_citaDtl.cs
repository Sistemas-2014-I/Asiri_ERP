namespace Common.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class CLlt06_citaDtl
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CLlt06_citaDtl()
        {
            TNSt02_comprobanteEmitidoDtl = new HashSet<TNSt02_comprobanteEmitidoDtl>();
        }

        [Key]
        public long idCitaDtl { get; set; }

        public decimal precio { get; set; }

        public decimal cantidad { get; set; }

        public bool esGratis { get; set; }

        public bool activo { get; set; }

        public long idCita { get; set; }

        public long idServicio { get; set; }

        public virtual CLlt05_cita CLlt05_cita { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TNSt02_comprobanteEmitidoDtl> TNSt02_comprobanteEmitidoDtl { get; set; }

        public virtual PROt04_servicio PROt04_servicio { get; set; }
    }
}
