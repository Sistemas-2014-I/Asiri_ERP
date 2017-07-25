using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Service.Maestro;
using Common;
using Microsoft.AspNet.Identity;
using Common.Helper;
using Asiri_ERP_Capas.DA;

namespace Asiri_ERP_Capas.Controllers.Maestro
{
    //[Authorize(Roles = "Administrador")]
    public class PisoController : Controller
    {
        private AsiriContext db = new AsiriContext();
        private PisoBL pisoBL = new PisoBL();

        public ActionResult Index()
        {
            #region
            var idusuario = User.Identity.GetUserId();

            if (idusuario == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var custom = db.CustomPermiso.Where(x => x.UserID == idusuario).ToList();
            var MenuUrl = db.Menu.SingleOrDefault(x => x.MenuURL == UrlSystem.UrlPiso);
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
            return View(pisoBL.Listar());
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(MSTt02_piso piso)
        {
            piso.idUsuario = User.Identity.GetUserId();
            piso.fecRegistro = DateTime.Now;
            piso.activo = true;
            if (db.MSTt02_piso.Any(x => x.numPiso == piso.numPiso))
            {
                ModelState.AddModelError("numPiso", "El número de piso indicado ya existe");
            }
            if (ModelState.IsValid)
            {
                pisoBL.agregarPiso(piso);
                return RedirectToAction("Index");
            }
            return View(piso);
        }

        public ActionResult Edit(short id)
        {
            MSTt02_piso piso = pisoBL.obtenerPiso(id);
            return View(piso);
        }

        [HttpPost]
        public ActionResult Edit(MSTt02_piso piso)
        {
            piso.idUsuarioModificar = User.Identity.GetUserId();
            if (db.MSTt02_piso.Any(x => x.numPiso == piso.numPiso))
            {
                ModelState.AddModelError("numPiso", "El número de piso indicado ya existe");
            }
            piso.fecModificacion = DateTime.Now;
            if (ModelState.IsValid)
            {
                if (pisoBL.actualizarPiso(piso))
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return View(piso);
                }
            }
            return View(piso);
        }

        public ActionResult Delete(int id)
        {
            var rp = pisoBL.eliminarPiso(id);
            return RedirectToAction("Index");
        }
    }
}