using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Common.ViewModel
{
    public class PacienteBE
    {
        public RHUt09_persona rHUt09_persona { get; set; }
        public RHUt07_paciente rHUt07_paciente { get; set; }
        public UBIt03_region uBIt03_region { get; set; }
        public UBIt02_provincia uBIt02_provincia { get; set; }
    }
}