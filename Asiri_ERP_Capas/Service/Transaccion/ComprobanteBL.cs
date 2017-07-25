using Common.Helper;
using Common.Model;
using Common.View.View_Report;
using Common.View.ViewModel;
using NLog;
using Persistence.Transaccion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Transaccion
{
    public class ComprobanteBL
    {
        public ComprobanteVM GetCompById(long idComp)
        {
            return new ComprobanteDA().GetCompById(idComp);
        }
        public ComprobanteVM GetCompBasicById(long idComp)
        {
            return new ComprobanteDA().GetCompBasicById(idComp);
        }
        public ComprobanteVM GetPreCompByCita(long idCita)
        {
            return new ComprobanteDA().GetPreCompByCita(idCita);
        }

        public IEnumerable<CitasPorCobrarVM> GetCitasPorCobrar()
        {
            return new ComprobanteDA().GetCitasPorCobrar();
        }

        public IEnumerable<ComprobanteVM> List()
        {
            return new ComprobanteDA().List();
        }

        public long Add(TNSt01_comprobanteEmitido obj)
        {
            obj.fecRegistro = DateTime.Now;
            obj.fecEmision = DateTime.Now;
            obj.fecCancelacion = obj.fecCancelacion ?? DateTime.Now;
            obj.fecVencimiento = obj.fecVencimiento ?? DateTime.Now;
            int? currentNumCorr = new ComprobanteDA().GetMaxNumCorr(obj.idTipoComprobante);
            int? currentSerie = new ComprobanteDA().GetMaxSerie(obj.idTipoComprobante);
            //Validar
            if (currentSerie != -1 && currentNumCorr != -1)
            {
                //lógica NUM NEGATIVOS VERR...
                //if(currentNumCorr!= null)
                currentNumCorr = currentNumCorr ?? 1;
                currentSerie = currentSerie ?? 1;
                //cuando es la primera serie y primer número
                if (!(currentSerie == 1 && currentNumCorr == 1))
                {
                    currentNumCorr = new ComprobanteDA().GetMaxNumCorrByCompSerie(obj.idTipoComprobante, ((int)currentSerie).ToString("D3"));

                    if (currentNumCorr != -1 && currentNumCorr != null)
                    {
                        if (currentNumCorr == ValuesSystem.MaxNumCorr)
                        {
                            if (currentSerie == ValuesSystem.MaxSerie)
                            {
                                var log = LogManager.GetLogger("fileLogger");
                                log.Error($"EL TIPO DE COMPROBANTE CON ID:{obj.idTipoComprobante} YA LLEGÓ AL LÍMITE {ValuesSystem.MaxSerie} - {ValuesSystem.MaxNumCorr}");
                            }
                            else if (currentSerie < ValuesSystem.MaxSerie)
                            {
                                //aumentar en uno la serie y resetear el num correlativo
                                obj.serie = ((int)++currentSerie).ToString("D3");
                                obj.numComprobanteEmitido = 1.ToString("D7");
                                return new ComprobanteDA().Add(obj);
                            }
                        }
                        //LLEGA AL ELSE SI NO SE REQUIERE AUMENTAR EL NUM SERIE Y EL MAC NU
                        else if (currentNumCorr < ValuesSystem.MaxNumCorr)
                        {
                            if (currentSerie <= ValuesSystem.MaxSerie)
                            {
                                obj.numComprobanteEmitido = ((int)++currentNumCorr).ToString("D7");
                                obj.serie = ((int)currentSerie).ToString("D3");
                                return new ComprobanteDA().Add(obj);
                            }
                        }
                    }
                }
                else
                {
                    obj.serie = ((int)currentSerie).ToString("D3");
                    obj.numComprobanteEmitido = ((int)currentNumCorr).ToString("D7");
                    return new ComprobanteDA().Add(obj);
                }
            }

            return 0;
        }

        public bool Anular(long idComp, string razonAnulacion, string idUser)
        {
            return new ComprobanteDA().Anular(idComp, razonAnulacion,idUser );
        }

        public IEnumerable<ComprobanteVR> GetComprobanteVR(long id)
        {
            return new ComprobanteDA().GetComprobanteVR(id);
        }

        public string GetAmountInText(long id)
        {
            return new ComprobanteDA().GetAmountInText(id);
        }
    }
}
