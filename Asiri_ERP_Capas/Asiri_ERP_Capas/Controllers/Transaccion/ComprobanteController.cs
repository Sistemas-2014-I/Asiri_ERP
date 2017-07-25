using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Common;
using Common.View.ViewModel;
using Common.Helper;
using Service.Transaccion;
using Common.Model;
using AutoMapper;
using Service.Sistema;
using Service.Maestro;
using System.IO;
using Microsoft.AspNet.Identity;
using Newtonsoft.Json.Converters;
using System.Net.Http;
using Newtonsoft.Json;
using Common.View.API;

namespace Asiri_ERP_Capas.Controllers.Transaccion
{
    public class ComprobanteController : Controller
    {
        private Common.Model.AsiriContext db = new Common.Model.AsiriContext();

        public ActionResult Index(bool? success)
        {

            var comprobantes = new ComprobanteBL().List();
            comprobantes = comprobantes ?? new List<ComprobanteVM>();
            return View(comprobantes);
        }

        public ActionResult Create(long? idCita)
        {
            if (idCita == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var medioDePagoRapido = new MedioDePagoBL().GetMedioDePagoRapido();
            if (medioDePagoRapido != null && medioDePagoRapido.idMedioDePago > 0)
            {
                ViewBag.descMedioDePagoRapido = medioDePagoRapido.descMedioDePago;
                ViewBag.idMedioDePagoRapido = medioDePagoRapido.idMedioDePago;
            }
            ViewBag.idTipoComprobante = new SelectList(db.SNTt04_tipoComprobante.ToList() ?? new List<Common.Model.SNTt04_tipoComprobante>() { }, "idTipoComprobante", "descTipoComprobante");
            ViewBag.idSucursal = new SelectList(db.MSTt04_sucursal.ToList() ?? new List<Common.Model.MSTt04_sucursal>() { }, "idSucursal", "descSucursal");

            var preComp = new ComprobanteBL().GetPreCompByCita((long)idCita);
            if (preComp != null && preComp.idCita > 0)
                return View(preComp);

            return RedirectToAction("Index");
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Create(ComprobanteVM comp)
        {
            var success = false;
            long id = 0;
            //  traer usuario para medio de pago 
            comp.idUsuario = "-";
            comp.idUsuario = User.Identity.GetUserId() ?? "-";
            if (ModelState.IsValid)
            {
                try
                {
                    var config = new MapperConfiguration(cfg =>
               {
                   cfg.CreateMap<ComprobanteVM, Common.Model.TNSt01_comprobanteEmitido>();
                   cfg.CreateMap<ComprobanteDtlVM, Common.Model.TNSt02_comprobanteEmitidoDtl>();
                   cfg.CreateMap<MedioDePagoDtlVM, Common.Model.TNSt06_medioDePagoDtl>();
               });

                    IMapper mapper = config.CreateMapper();
                    var comprobante = mapper.Map<Common.Model.TNSt01_comprobanteEmitido>(comp);
                    foreach (var cd in comp.ComprobanteDtl)
                        comprobante.TNSt02_comprobanteEmitidoDtl.Add(mapper.Map<Common.Model.TNSt02_comprobanteEmitidoDtl>(cd));
                    foreach (var md in comp.MedioDePagoDtl)
                        comprobante.TNSt06_medioDePagoDtl.Add(mapper.Map<Common.Model.TNSt06_medioDePagoDtl>(md));

                    foreach (var mpdt in comprobante.TNSt06_medioDePagoDtl)
                    {
                        mpdt.fecRegistro = DateTime.Now;
                        mpdt.idUsuario = User.Identity.GetUserId() ?? "-"; }


                    id = new ComprobanteBL().Add(comprobante);
                    success = id > 0;
                }
                catch (Exception e)
                {

                }
            }
            return Json(new { success, id }, JsonRequestBehavior.AllowGet);
        }

        public PartialViewResult _CitasPorCobrar()
        {
            var citas = new ComprobanteBL().GetCitasPorCobrar();
            if (citas != null)
                return PartialView(citas);
            ViewBag.error = ErrorMsj.CitasPorCobrarNull;
            return PartialView("_ErrorResponse");
        }

        public PartialViewResult _Detalle(long? idComp)
        {

            if (idComp != null)
            {
                var comp = new ComprobanteBL().GetCompById((long)idComp);
                if (comp != null && comp.idComprobanteEmitido > 0)
                    return PartialView(comp);
            }
            ViewBag.error = ErrorMsj.ComprobanteNotFound;
            return PartialView("_ErrorResponse");
        }

        public PartialViewResult _Anular(long? idComp)
        {
            if (idComp != null)
            {
                var comp = new ComprobanteBL().GetCompBasicById((long)idComp);
                return PartialView(comp);
            }
            ViewBag.error = ErrorMsj.ComprobanteNotFound;
            return PartialView("_ErrorResponse");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Anular(ComprobanteVM comp)
        {
            //comp.idUsuarioAnular = User.Identity.GetUserId() ?? "-";
            if (ModelState.IsValid)
            {         
                var success = new ComprobanteBL().Anular(comp.idComprobanteEmitido, comp.razonAnulacion,User.Identity.GetUserId() ?? "-");
            }
            return RedirectToAction("Index");
        }

        public PartialViewResult _MedioDePagoDtl()
        {
            ViewBag.idMoneda = new SelectList(db.SNTt03_moneda.Where(x => x.activo == true).ToList(), "idMoneda", "descMoneda");
            ViewBag.idMedioDePago = new SelectList(db.MSTt01_medioDePago, "idMedioDePago", "descMedioDePago");
            return PartialView();
        }

        public ActionResult ReporteComprobante(long? id)
        {
            if (id != null && id > 0)
            {
                var model = new ComprobanteBL().GetComprobanteVR((long)id);
                var amountInText = "";
                if (model != null && model.Count() > 0)
                {
                    amountInText = new ComprobanteBL().GetAmountInText((long)id);
                }

                if (model != null && model.Count() > 0)
                {
                    string reportPath = Path.Combine(Server.MapPath("~/Reportes"), "ComprobanteRpt.rpt");
                    return new CrystalReportPdfResult(reportPath, model, "GroupFooterSection1", "TxtMtoEnTexto", amountInText);
                }
            }
            return Content("NO SE PUDO GENERAR ESTE COMPROBANTE.");
        }

        public JsonResult GetTipoCambio(decimal mto, string monedaInicial, string monedaFinal)
        {
        
            var HttpClient = new HttpClient();
            #region Parámetros
            byte decimales = 3;
            string key = "ej07skuqi40r15hh57nliastqu4odsbtn7fasrbf";
            //var fecha = Da.ToString("dd-MM-yyyy");
            #endregion
            string UrlService = $"https://sunapiperu.com/api/calculadora?valor={mto}&de={monedaInicial}&a={monedaFinal}&decimal={decimales}&apikey={key}";
            string JsonString = "";
            using (WebClient mc = new WebClient())
            {
                JsonString = mc.DownloadString(UrlService);
            }
            //var IsoDateFormat = new IsoDateTimeConverter { DateTimeFormat = "dd-MM-yyyy" };
            //var TipoCambio = JsonConvert.se<TipoCambio>(JsonString, IsoDateFormat);

            return Json(JsonString, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetMonedaISO()
        {
            var iso = new List<MonedaISO>();
            try
            {
               iso = db.SNTt03_moneda.Where(x => x.activo == true).Select(m =>new MonedaISO() { id = m.idMoneda, iso = m.codIsoMoneda}).ToList();
            }
            catch(Exception e)
            {
                iso = null;
            }
            return Json(JsonConvert.SerializeObject(iso), JsonRequestBehavior.AllowGet);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

    }
}
