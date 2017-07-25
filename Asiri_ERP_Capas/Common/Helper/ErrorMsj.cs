using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Helper
{
    public class ErrorMsj
    {
        //Manejar los errores con códigos. 
        public const string TipoPersoneriaIncorrecto = "Tipo de personería incorrecto - ERRORx001";
        public const string ComprobanteNotFound = @"Comprobante no encontrado. Ocurrió un error cuando se
                                                    intento obtener el comprobante.";
        public const string CitasPorCobrarNull = @"No se pudo mostrar las citas por cobrar.";
    }
}
