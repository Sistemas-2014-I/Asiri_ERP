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
    
    public partial class RHUt10_personaRedSocial
    {
        public int idRedSocial { get; set; }
        public long idPersona { get; set; }
        public string nombrePerfil { get; set; }
        public string obsvPerfil { get; set; }
        public bool activo { get; set; }
        public System.DateTime fecRegistro { get; set; }
        public Nullable<System.DateTime> fecModificacion { get; set; }
        public Nullable<System.DateTime> fecEliminacion { get; set; }
        public string idUsuario { get; set; }
        public string idUsuarioModificar { get; set; }
        public string idUsuarioEliminar { get; set; }
    
        public virtual MSTt03_redSocial MSTt03_redSocial { get; set; }
        public virtual RHUt09_persona RHUt09_persona { get; set; }
    }
}
