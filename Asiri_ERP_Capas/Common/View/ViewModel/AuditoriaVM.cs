using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.View.ViewModel
{
    public class AuditoriaVM
    {

        public long usuarioRegistro { get; set; }
        public string fecRegistro { get; set; }
        public long usuarioModificacion { get; set; }
        public string fecModificacion { get; set; }
        public long usuarioEliminacion { get; set; }
        public string fecEliminacion { get; set; }

        public AuditoriaVM() { }
    }
}
