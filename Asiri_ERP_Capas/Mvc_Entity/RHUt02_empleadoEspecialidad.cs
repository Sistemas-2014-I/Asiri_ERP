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
    
    public partial class RHUt02_empleadoEspecialidad
    {
        public long idEmpleado { get; set; }
        public int idEspecialidad { get; set; }
        public System.DateTime fecRegistro { get; set; }
        public Nullable<System.DateTime> fecModificacion { get; set; }
        public Nullable<System.DateTime> fecEliminacion { get; set; }
        public bool activo { get; set; }
        public string idUsuario { get; set; }
        public string idUsuarioModificar { get; set; }
        public string IdUsuarioEliminar { get; set; }
    
        public virtual RHUt01_empleado RHUt01_empleado { get; set; }
        public virtual RHUt04_especialidad RHUt04_especialidad { get; set; }
    }
}
