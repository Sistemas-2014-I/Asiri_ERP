//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Mvc_Entity
{
    using System;
    using System.Collections.Generic;
    
    public partial class RHUt08_periodoRemuneracion
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public RHUt08_periodoRemuneracion()
        {
            this.RHUt01_empleado = new HashSet<RHUt01_empleado>();
        }
    
        public long idPeriodoRemuneracion { get; set; }
        public string codPeriodoRemuneracion { get; set; }
        public string descPeriodoRemuneracion { get; set; }
        public string abrvPeriodoRemuneracion { get; set; }
        public bool activo { get; set; }
        public System.DateTime fecRegistro { get; set; }
        public Nullable<System.DateTime> fecModificacion { get; set; }
        public Nullable<System.DateTime> fecEliminacion { get; set; }
        public string idUsuario { get; set; }
        public string idUsuarioModificar { get; set; }
        public string idUsuarioEliminar { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RHUt01_empleado> RHUt01_empleado { get; set; }
    }
}
