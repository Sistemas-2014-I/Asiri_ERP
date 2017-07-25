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
    public class TipoDeEspecialidadController : Controller
    {
        private AsiriContext db = new AsiriContext();
        private TipoDeEspecialidadBL tipoDeEspecialidadBL = new TipoDeEspecialidadBL();

        public ActionResult Index()
        {
            return View(tipoDeEspecialidadBL.Listar());
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(RHUt13_tipoEspecialidad tipoDeEspecialidad)
        {
            tipoDeEspecialidad.idUsuario = User.Identity.GetUserId();
            tipoDeEspecialidad.fecRegistro = DateTime.Now;
            tipoDeEspecialidad.activo = true;
            if (ModelState.IsValid)
            {
                tipoDeEspecialidadBL.agregarTipoDeEspecialidad(tipoDeEspecialidad);
                return RedirectToAction("Index");
            }
            return View(tipoDeEspecialidad);
        }

        public ActionResult Edit(int id)
        {
            RHUt13_tipoEspecialidad tipoDeEspecialidad = tipoDeEspecialidadBL.obtenerTipoDeEspecialidad(id);
            return View(tipoDeEspecialidad);
        }

        [HttpPost]
        public ActionResult Edit(RHUt13_tipoEspecialidad tipoDeEspecialidad)
        {
            tipoDeEspecialidad.idUsuarioModificar = User.Identity.GetUserId();
            tipoDeEspecialidad.fecModificacion = DateTime.Now;
            if (ModelState.IsValid)
            {
                if (tipoDeEspecialidadBL.actualizarTipoDeEspecialidad(tipoDeEspecialidad))
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return View(tipoDeEspecialidad);
                }
            }
            return View(tipoDeEspecialidad);
        }

        public ActionResult Delete(int id)
        {
            if (tipoDeEspecialidadBL.eliminarTipoDeEspecialidad(id))
                return RedirectToAction("Index");
            else
                return RedirectToAction("Index");
        }
    }
}