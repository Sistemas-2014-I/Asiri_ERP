using Common.Helper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Common.Helper.ValuesSystem;

namespace Common.View.ViewModel
{
    public class ComprobanteVM
    {
        public long idComprobanteEmitido { get; set; }
        public string serie { get; set; }
        public string numComprobanteEmitido { get; set; }

        public string tipoPersoneria { get; set; }
        public string nombrePaciente { get; set; }
        public string apellidoPaternoPaciente { get; set; }
        public string apellidoMaternoPaciente { get; set; }
        public string razonSocial { get; set; }
        public string nombreRepresentante { get; set; }
        public string tipoDoc { get; set; }
        public string numDoc { get; set; }

        [StringLength(250)]
        public string razonAnulacion { get; set; }
        public bool esAnulado { get; set; }

        public List<ComprobanteDtlVM> ComprobanteDtl { get; set; }
        public List<MedioDePagoDtlVM> MedioDePagoDtl { get; set; }


        [StringLength(250)]
        public string obsvComprobanteEmitido { get; set; }
        [StringLength(250)]
        public string info01 { get; set; }
        [StringLength(250)]
        public string info02 { get; set; }
        [StringLength(250)]
        public string info03 { get; set; }
        public DateTime? fecha01 { get; set; }
        public DateTime? fecha02 { get; set; }
        public DateTime? fecha03 { get; set; }

        [Required]
        public long idCita { get; set; }
        public string codCita { get; set; }
        public string nombreDoctor { get; set; }
        public string apellidoPaternoDoctor { get; set; }
        public string apellidoMaternoDoctor { get; set; }
        public string fecHoraInicioCita { get; set; }
        public string consultorio { get; set; }
        public string piso { get; set; }

        [Required(ErrorMessage = "El campo es obligatorio.")]
        public int idTipoComprobante { get; set; }
        public string nombreTipoComp { get; set; }
        [Required]
        public int idMoneda { get; set; }
        public string nombreMoneda { get; set; }
        public string simboloMoneda { get; set; }
        [Required]
        public short idImpto { get; set; }
        public string abrvImpto { get; set; }

        //data anotation
        [Range(0, MaxIntAmount)]
        public decimal mtoTotal { get; set; }
        [Range(0, MaxIntAmount)]
        public decimal mtoSubTotal { get; set; }
        [Range(0, MaxIntAmount)]
        public decimal mtoImpto { get; set; }
        [Range(0, MaxIntAmount)]
        public decimal porcentajeImpto { get; set; }
        [Range(0, MaxIntAmount)]
        public decimal? mtoDescuento { get; set; }
        [Range(0, 100)]
        public decimal? porcentajeDescuento { get; set; }

        public DateTime? fecEmision { get; set; }
        public DateTime? fecVencimiento { get; set; }
        public DateTime? fecCancelacion { get; set; }

        public string idUsuario { get; set; }
        public DateTime fecRegistro { get; set; }

        public string idUsuarioAnular { get; set; }
        public DateTime? fecAnulacion { get; set; }

        [Required(ErrorMessage ="El campo es obligatorio.")]
        public int idSucursal { get; set; }
        //public AuditoriaVM aud { get; set; }

        public ComprobanteVM()
        {
            ComprobanteDtl = new List<ComprobanteDtlVM>();
            MedioDePagoDtl = new List<MedioDePagoDtlVM>();
            //aud = new AuditoriaVM();

        }

        public string GetNombrePaciente()
        {
            return string.Join(" ", new string[] { apellidoPaternoPaciente, apellidoMaternoPaciente, nombrePaciente }).ToUpper();
        }

        public string GetNombreDoctor()
        {
            return string.Join(" ", new string[] { apellidoPaternoDoctor, apellidoMaternoDoctor, nombreDoctor }).ToUpper();
        }

        public string GetPersoneria()
        {
            return tipoPersoneria == PersonaNatural ? "NATURAL" : tipoPersoneria == PersonaJuridica ? "JURÍDICO" : ErrorMsj.TipoPersoneriaIncorrecto;
        }

        public string GetNombreXPersoneria()
        {
            return tipoPersoneria == PersonaNatural ? GetNombrePaciente() : tipoPersoneria == PersonaJuridica ? nombreRepresentante : ErrorMsj.TipoPersoneriaIncorrecto;
        }

        public string GetEstado()
        {
            return esAnulado? "ANULADO": "CANCELADO";
        }
    }

    public class ComprobanteDtlVM
    {
        public long idCitaDtl { get; set; }
        public decimal cantidad { get; set; }
        public decimal precio { get; set; }
        public string nombreServicio { get; set; }

        public ComprobanteDtlVM() { }
    }

    public class MedioDePagoDtlVM
    {
        public int idMoneda { get; set; }
        public short idMedioDePago { get; set; }
        public string nombreMoneda { get; set; }
        public string nombreMedioDePago { get; set; }
        [Range(0, MaxIntAmount)]
        public decimal mtoMedioDePago { get; set; }
        [Range(0, MaxIntAmount)]
        public decimal tipoDeCambio { get; set; }
        public bool activo { get; set; }


        public MedioDePagoDtlVM() { }
    }
}
