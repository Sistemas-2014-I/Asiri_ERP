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
    
    public partial class RHUt06_horario
    {
        public long idHorario { get; set; }
        public bool laboraLunes { get; set; }
        public bool laboraMartes { get; set; }
        public bool laboraMiercoles { get; set; }
        public bool laboraJueves { get; set; }
        public bool laboraViernes { get; set; }
        public bool laboraSabado { get; set; }
        public bool laboraDomingo { get; set; }
        public string horaInicioLunes { get; set; }
        public string horaFinLunes { get; set; }
        public string horaInicioMartes { get; set; }
        public string horaFinMartes { get; set; }
        public string horaInicioMiercoles { get; set; }
        public string horaFinMiercoles { get; set; }
        public string horaInicioJueves { get; set; }
        public string horaFinJueves { get; set; }
        public string horaInicioViernes { get; set; }
        public string horaFinViernes { get; set; }
        public string horaInicioSabado { get; set; }
        public string horaFinSabado { get; set; }
        public string horaInicioDomingo { get; set; }
        public string horaFinDomingo { get; set; }
        public System.DateTime fecRegistro { get; set; }
        public Nullable<System.DateTime> fecModificacion { get; set; }
        public string idUsuario { get; set; }
        public string idUsuarioModificar { get; set; }
        public long idEmpleado { get; set; }
    
        public virtual RHUt01_empleado RHUt01_empleado { get; set; }
    }
}
