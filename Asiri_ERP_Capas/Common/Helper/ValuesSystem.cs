using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Helper
{
    public static class ValuesSystem
    {

        //IMPLEMEMTAR EL PLUGIN DE PAGINACIÓN
        //MEDIO DE PAGO
        //VALIDACIÓN DESCUENTO

        public const string PersonaNatural = "N";
        public const string PersonaJuridica = "J";

        public const decimal MaxAmount = 999999999999.999999m;
        public const decimal MaxPercent = 9999.999999m;
        public const long MaxIntAmount = 999999999999;
        public const int MaxIntPercent = 9999;

        public const int MaxSerie = 999;
        public const int MaxNumCorr = 9999999;


        public const string Efectivo = "01";

        public const int NoResponse = 0;
        public const int RegistroCorrecto = 1;
        public const int RegistroError = 2;
        public const int EdicionCorrecto = 3;
        public const int EdicionError = 4;
        public const int EliminacionCorrecto = 5;
        public const int EliminacionError = 6;
    }
}
