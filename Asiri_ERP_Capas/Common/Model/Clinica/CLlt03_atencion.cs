namespace Common.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class CLlt03_atencion
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CLlt03_atencion()
        {
            CLIt01_anamnesis = new HashSet<CLIt01_anamnesis>();
            CLlt12_examenFisico = new HashSet<CLlt12_examenFisico>();
            CLlt13_funcionVital = new HashSet<CLlt13_funcionVital>();
            CLlt10_estudioCompl = new HashSet<CLlt10_estudioCompl>();
            CLlt08_diagnostico = new HashSet<CLlt08_diagnostico>();
            CLlt11_evolucion = new HashSet<CLlt11_evolucion>();
        }

        [Key]
        public long idAtencion { get; set; }

        [Required]
        [StringLength(10)]
        public string codAtencion { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime fecRegistro { get; set; }

        [Required]
        [StringLength(128)]
        public string idUsuario { get; set; }

        public long? idCita { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CLIt01_anamnesis> CLIt01_anamnesis { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CLlt12_examenFisico> CLlt12_examenFisico { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CLlt13_funcionVital> CLlt13_funcionVital { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CLlt10_estudioCompl> CLlt10_estudioCompl { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CLlt08_diagnostico> CLlt08_diagnostico { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CLlt11_evolucion> CLlt11_evolucion { get; set; }

        public virtual CLlt05_cita CLlt05_cita { get; set; }
    }
}
