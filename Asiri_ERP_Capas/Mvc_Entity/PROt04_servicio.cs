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
    
    public partial class PROt04_servicio
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PROt04_servicio()
        {
            this.CLlt06_citaDtl = new HashSet<CLlt06_citaDtl>();
            this.CLlt17_tratamientoDtl = new HashSet<CLlt17_tratamientoDtl>();
        }
    
        public long idServicio { get; set; }
        public string codServicio { get; set; }
        public string nombreServicio { get; set; }
        public string descServicio { get; set; }
        public decimal precio { get; set; }
        public Nullable<decimal> precioMinimo { get; set; }
        public Nullable<decimal> precioMaximo { get; set; }
        public bool esGratis { get; set; }
        public bool activo { get; set; }
        public System.DateTime fecRegistro { get; set; }
        public Nullable<System.DateTime> fecModificacion { get; set; }
        public Nullable<System.DateTime> fecEliminacion { get; set; }
        public string idUsuario { get; set; }
        public string idUsuarioModificar { get; set; }
        public string idUsuarioEliminar { get; set; }
        public int idTipoDeServicio { get; set; }
        public int idEspecialidad { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CLlt06_citaDtl> CLlt06_citaDtl { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CLlt17_tratamientoDtl> CLlt17_tratamientoDtl { get; set; }
        public virtual PROt05_tipoDeServicio PROt05_tipoDeServicio { get; set; }
        public virtual RHUt04_especialidad RHUt04_especialidad { get; set; }
    }
}
