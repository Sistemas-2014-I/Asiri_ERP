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
    
    public partial class SNTt02_impuesto
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SNTt02_impuesto()
        {
            this.TNSt01_comprobanteEmitido = new HashSet<TNSt01_comprobanteEmitido>();
        }
    
        public short idImpto { get; set; }
        public string codImpto { get; set; }
        public string descImpto { get; set; }
        public string abrvImpto { get; set; }
        public decimal porcentajeImpto { get; set; }
        public bool activo { get; set; }
        public System.DateTime fecRegistro { get; set; }
        public Nullable<System.DateTime> fecModificacion { get; set; }
        public Nullable<System.DateTime> fecEliminacion { get; set; }
        public string idUsuario { get; set; }
        public string idUsuarioModificar { get; set; }
        public string idUsuarioEliminar { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TNSt01_comprobanteEmitido> TNSt01_comprobanteEmitido { get; set; }
    }
}
