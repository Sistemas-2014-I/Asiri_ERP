using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Common;
using Service.Sunat;
using Common.Helper;
using Asiri_ERP_Capas.DA;
using Microsoft.AspNet.Identity;

namespace Asiri_ERP.Controllers.Sunat
{
    public class ImpuestoController : Controller
    {
        private AsiriContext db = new AsiriContext();

        // GET: Impuesto
        public ActionResult Index()
        {
            #region
            var idusuario = User.Identity.GetUserId();

            if (idusuario == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var custom = db.CustomPermiso.Where(x => x.UserID == idusuario).ToList();
            var MenuUrl = db.Menu.SingleOrDefault(x => x.MenuURL == UrlSystem.UrlImpuesto);
            var validar1 = false;

            for (int i = 0; i < custom.Count; i++)
            {
                if (custom[i].MenuID == MenuUrl.MenuID)
                {
                    validar1 = true;
                    break;
                }
            }


            AspNetUserRolesDA oAspNetUserRolesDA = new AspNetUserRolesDA();
            var userRol = oAspNetUserRolesDA.obtenerPermisos(idusuario);
            var idRol = userRol[0].RoleId;
            var permiso = db.Permiso.Where(x => x.RoleID == idRol).ToList();
            var validar2 = false;
            for (int i = 0; i < permiso.Count; i++)
            {
                if (permiso[i].MenuID == MenuUrl.MenuID)
                {
                    validar2 = true;
                    break;
                }
            }
            if (validar1 == false && validar2 == false)
            {
                return RedirectToAction("ErrorPermiso", "Account");
            }

            #endregion

            var model = new ImpuestoBL().List();
            return View(model);
        }
        // GET: Impuesto/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Impuesto/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idImpto,codImpto,descImpto,abrvImpto,porcentajeImpto,activo,fecRegistro,fecModificacion,fecEliminacion,idUsuario,idUsuarioModificar,idUsuarioEliminar")] SNTt02_impuesto model)
        {
            model.idUsuario = "23";
            if (db.SNTt02_impuesto.Any(x => x.abrvImpto== model.abrvImpto))
                ModelState.AddModelError("abrvImpto", "El impuesto ingresado ya existe");
            if (db.SNTt02_impuesto.Any(x => x.codImpto == model.codImpto))
                ModelState.AddModelError("codImpto", "El código ingresado ya existe");
            if (ModelState.IsValid)
            {
                new ImpuestoBL().Add(model);
                return RedirectToAction("Index");
            }

            return View(model);
        }

        // GET: Impuesto/Edit/5
        public ActionResult Edit(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SNTt02_impuesto model = new ImpuestoBL().GetImpuesto(id);
            if (model == null)
            {
                return HttpNotFound();
            }
            return View(model);
        }

        // POST: Impuesto/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idImpto,codImpto,descImpto,abrvImpto,porcentajeImpto,activo,fecRegistro,fecModificacion,fecEliminacion,idUsuario,idUsuarioModificar,idUsuarioEliminar")] SNTt02_impuesto model)
        {
            if (ModelState.IsValid)
            {
                new ImpuestoBL().Edit(model);
                return RedirectToAction("Index");
            }
            return View(model);
        }


        public ActionResult GetImpuesto(string id)
        {
            var model = new ImpuestoBL().GetImpuesto(short.Parse(id));
            return PartialView("Modal/_DetailImpuesto", model);
        }
        public ActionResult ChangeStatus(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var model = new ImpuestoBL().GetImpuesto(id);
            if (model == null)
            {
                return HttpNotFound();
            }
            new ImpuestoBL().ChangeStatus(model);
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
