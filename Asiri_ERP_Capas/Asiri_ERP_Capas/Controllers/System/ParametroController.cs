using Common.Helper;
using Common;
using Service.Sunat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;

namespace Asiri.Controllers.System
{
    public class ParametroController : Controller
    {
        private AsiriContext ctx = new AsiriContext();
        // GET: Parametro
        public ActionResult Index()
        {
            // Debe estar configurado el sistema con la moneda para que se pueda obtener
            ViewBag.idMoneda = new SelectList(ctx.SNTt03_moneda, "idMoneda", "descMoneda", new MonedaBL().GetMonedaSist().idMoneda);
            ViewBag.idImpuesto = new SelectList(ctx.SNTt02_impuesto, "idImpto", "descImpto");
            ViewBag.idMedioDePago = new SelectList(ctx.MSTt01_medioDePago, "idMedioDePago", "descMedioDePago");
            return View();
        }
        [HttpPost]
        public ActionResult Moneda(int? id)
        {
            if (id != null)
            {
                ctx.Database.ExecuteSqlCommand(
                    $"UPDATE SISt01_parametro SET valorNumerico = {id} WHERE codParametro={CodParam.Moneda}");
            }
            else
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            return Json("Index");
        }
        [HttpPost]
        public ActionResult Impuesto(string id, string porcImp)
        {
            var x = new SNTt02_impuesto();

            ctx.Database.ExecuteSqlCommand($"UPDATE SISt01_parametro SET valorNumerico = {id} WHERE codParametro={CodParam.ImpuestoComprobante}");
            ViewBag.idMoneda = new SelectList(ctx.SNTt03_moneda, "idMoneda", "descMoneda");
            ViewBag.idImpuesto = new SelectList(ctx.SNTt02_impuesto, "idImpto", "descImpto");
            ViewBag.idMedioDePago = new SelectList(ctx.MSTt01_medioDePago, "idMedioDePago", "descMedioDePago");
            return View("Index");
        }
        [HttpPost]
        public ActionResult MedioPago(int idMedio)
        {
            ctx.Database.ExecuteSqlCommand($"UPDATE SISt01_parametro SET valorNumerico = {idMedio} WHERE codParametro={CodParam.MedioDePagoRapido}");
            ViewBag.idMoneda = new SelectList(ctx.SNTt03_moneda, "idMoneda", "descMoneda");
            ViewBag.idImpuesto = new SelectList(ctx.SNTt02_impuesto, "idImpto", "descImpto");
            ViewBag.idMedioDePago = new SelectList(ctx.MSTt01_medioDePago, "idMedioDePago", "descMedioDePago");
            return View("Index");
        }



        public ActionResult ParametrosCita()
        {
            var parametro = new SISt01_parametro();

            var citaXDia = ctx.SISt01_parametro.SingleOrDefault(x => x.codParametro == CodParam.CitaxDia);
            if (citaXDia.valorNumerico == null)
            {
                citaXDia.valorNumerico = citaXDia.valorNumericoDefault;
            }
            ViewBag.valorActualCita = (int)citaXDia.valorNumerico;

            var reprogramaciaXCita = ctx.SISt01_parametro.SingleOrDefault(x => x.codParametro == CodParam.NReprogramacionMax);
            if (reprogramaciaXCita.valorNumerico == null)
            {
                reprogramaciaXCita.valorNumerico = reprogramaciaXCita.valorNumericoDefault;
            }
            ViewBag.valorActualReprogramacion = (int)reprogramaciaXCita.valorNumerico;

            var horaFinXDia = ctx.SISt01_parametro.SingleOrDefault(x => x.codParametro == CodParam.HoraFinDiaLaboral);
            if (horaFinXDia.valorDeTexto == null)
            {
                horaFinXDia.valorDeTexto = horaFinXDia.valorTextoDefault;
            }
            ViewBag.horaActualxDia = DateTime.Parse(horaFinXDia.valorDeTexto).ToString("hh:mm tt");


            return View();
        }
        [HttpPost]
        public ActionResult CitaxDia(SISt01_parametro sISt01_parametro)
        {
            var citaXDia = ctx.SISt01_parametro.SingleOrDefault(x => x.codParametro == CodParam.CitaxDia);

            if (sISt01_parametro.valorNumerico != null)
            {
                citaXDia.valorNumerico = sISt01_parametro.valorNumerico;
                citaXDia.fecModificacion = DateTime.Now;
                var idUsu = User.Identity.GetUserId();
                citaXDia.idUsuarioModificar = idUsu;
                ctx.SaveChanges();
                return RedirectToAction("ParametrosCita");
            }
            ModelState.AddModelError("valorNumerico", "Debe insertar un valor");

            return RedirectToAction("ParametrosCita");

        }
        [HttpPost]
        public ActionResult ReprogramacionxCita(SISt01_parametro sISt01_parametro)
        {
            var reprogramacionxCita = ctx.SISt01_parametro.SingleOrDefault(x => x.codParametro == CodParam.NReprogramacionMax);

            if (sISt01_parametro.valorNumerico != null)
            {
                reprogramacionxCita.valorNumerico = sISt01_parametro.valorNumerico;
                reprogramacionxCita.fecModificacion = DateTime.Now;
                var idUsu = User.Identity.GetUserId();
                reprogramacionxCita.idUsuarioModificar = idUsu;
                ctx.SaveChanges();
                return RedirectToAction("ParametrosCita");
            }
            ModelState.AddModelError("valorNumerico", "Debe insertar un valor");

            return RedirectToAction("ParametrosCita");

        }
        [HttpPost]
        public ActionResult HorafinLaboral(SISt01_parametro sISt01_parametro)
        {
            var horaFin = ctx.SISt01_parametro.SingleOrDefault(x => x.codParametro == CodParam.HoraFinDiaLaboral);
            TimeSpan d;
            var date = TimeSpan.TryParse(sISt01_parametro.valorDeTexto, out d);
            var valid = ":";
            if (sISt01_parametro.valorDeTexto.Contains(valid))
            {
                date = true;
            }
            else
            {
                date = false;
            }
            if (date == true)
            {
                horaFin.valorDeTexto = sISt01_parametro.valorDeTexto;
                horaFin.fecModificacion = DateTime.Now;
                var idUsu = User.Identity.GetUserId();
                horaFin.idUsuarioModificar = idUsu;
                ctx.SaveChanges();
                return RedirectToAction("ParametrosCita");
            }
            ModelState.AddModelError("valorDeTexto", "Debe insertar un dato en formato hora");

            var citaXDia = ctx.SISt01_parametro.SingleOrDefault(x => x.codParametro == CodParam.CitaxDia);
            if (citaXDia.valorNumerico == null)
            {
                citaXDia.valorNumerico = citaXDia.valorNumericoDefault;
            }
            ViewBag.valorActualCita = (int)citaXDia.valorNumerico;

            var reprogramaciaXCita = ctx.SISt01_parametro.SingleOrDefault(x => x.codParametro == CodParam.NReprogramacionMax);
            if (reprogramaciaXCita.valorNumerico == null)
            {
                reprogramaciaXCita.valorNumerico = reprogramaciaXCita.valorNumericoDefault;
            }
            ViewBag.valorActualReprogramacion = (int)reprogramaciaXCita.valorNumerico;

            var horaFinXDia = ctx.SISt01_parametro.SingleOrDefault(x => x.codParametro == CodParam.HoraFinDiaLaboral);
            if (horaFinXDia.valorDeTexto == null)
            {
                horaFinXDia.valorDeTexto = horaFinXDia.valorTextoDefault;
            }
            ViewBag.horaActualxDia = DateTime.Parse(horaFinXDia.valorDeTexto).ToString("hh:mm tt");


            return View("ParametrosCita");
        }
    }
}