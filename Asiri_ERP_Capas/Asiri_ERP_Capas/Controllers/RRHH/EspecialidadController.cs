using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Common;
using Service.RRHH;
using Microsoft.AspNet.Identity;

namespace Asiri_ERP_Capas.Controllers.RRHH
{
    public class EspecialidadController : Controller
    {
        private AsiriContext db = new AsiriContext();
        private EspecialidadBL especialidadBL = new EspecialidadBL();

        public ActionResult Index()
        {
            return View(especialidadBL.Listar());
        }

        public ActionResult Create()
        {
            ViewBag.idTipoDeEspeciliadad = new SelectList(db.RHUt13_tipoEspecialidad, "idTipoDeEspeciliadad", "descEspecialidad");
            return View();
        }

        [HttpPost]
        public ActionResult Create(RHUt04_especialidad especialidad)
        {
            especialidad.idUsuario = User.Identity.GetUserId();
            especialidad.fecRegistro = DateTime.Now;
            especialidad.activo = true;
            if (ModelState.IsValid)
            {
                especialidadBL.agregarEspecialidad(especialidad);
                return RedirectToAction("Index");
            }
            ViewBag.idTipoDeEspeciliadad = new SelectList(db.RHUt13_tipoEspecialidad, "idTipoDeEspeciliadad", "descEspecialidad", especialidad.idTipoDeEspeciliadad);
            return View(especialidad);
        }

        public ActionResult Edit(int id)
        {
            RHUt04_especialidad especialidad = especialidadBL.obtenerEspecialidad(id);
            ViewBag.idTipoDeEspeciliadad = new SelectList(db.RHUt13_tipoEspecialidad, "idTipoDeEspeciliadad", "descEspecialidad", especialidad.idTipoDeEspeciliadad);
            return View(especialidad);
        }

        [HttpPost]
        public ActionResult Edit(RHUt04_especialidad especialidad)
        {
            especialidad.idUsuarioModificar = User.Identity.GetUserId();
            especialidad.fecModificacion = DateTime.Now;
            if (ModelState.IsValid)
            {
                if (especialidadBL.actualizarEspecialidad(especialidad))
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.idTipoDeEspeciliadad = new SelectList(db.RHUt13_tipoEspecialidad, "idTipoDeEspeciliadad", "descEspecialidad", especialidad.idTipoDeEspeciliadad);
                    return View(especialidad);
                }
            }
            ViewBag.idTipoDeEspeciliadad = new SelectList(db.RHUt13_tipoEspecialidad, "idTipoDeEspeciliadad", "descEspecialidad", especialidad.idTipoDeEspeciliadad);
            return View(especialidad);
        }

        public ActionResult Delete(int id)
        {
            if (especialidadBL.eliminarEspecialidad(id))
                return RedirectToAction("Index");
            else
                return RedirectToAction("Index");
        }
    }
}