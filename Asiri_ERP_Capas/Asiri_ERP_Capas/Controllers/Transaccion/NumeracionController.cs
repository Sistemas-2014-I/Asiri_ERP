using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Common.Model;
using Service.Transaccion;

namespace Asiri_ERP_Capas.Controllers.Transaccion
{
    public class NumeracionController : Controller
    {
        private AsiriContext db = new AsiriContext();

        public ActionResult Index()
        {
            var list = new NumeracionBL().List();
            return View(list);
        }

        // GET: Numeracion/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TNSt07_numeracion tNSt07_numeracion = db.TNSt07_numeracion.Find(id);
            if (tNSt07_numeracion == null)
            {
                return HttpNotFound();
            }
            return View(tNSt07_numeracion);
        }

        // GET: Numeracion/Create
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TNSt07_numeracion tNSt07_numeracion)
        {
            if (ModelState.IsValid)
            {
                db.TNSt07_numeracion.Add(tNSt07_numeracion);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tNSt07_numeracion);
        }

        // GET: Numeracion/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TNSt07_numeracion tNSt07_numeracion = db.TNSt07_numeracion.Find(id);
            if (tNSt07_numeracion == null)
            {
                return HttpNotFound();
            }
            return View(tNSt07_numeracion);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(TNSt07_numeracion tNSt07_numeracion)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tNSt07_numeracion).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tNSt07_numeracion);
        }

        // GET: Numeracion/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TNSt07_numeracion tNSt07_numeracion = db.TNSt07_numeracion.Find(id);
            if (tNSt07_numeracion == null)
            {
                return HttpNotFound();
            }
            return View(tNSt07_numeracion);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TNSt07_numeracion tNSt07_numeracion = db.TNSt07_numeracion.Find(id);
            db.TNSt07_numeracion.Remove(tNSt07_numeracion);
            db.SaveChanges();
            return RedirectToAction("Index");
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
