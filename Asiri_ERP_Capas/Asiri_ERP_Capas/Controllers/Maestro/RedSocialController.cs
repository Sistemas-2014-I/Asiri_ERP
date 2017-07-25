using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Service.Maestro;
using Common;
using Asiri_ERP_Capas.DA;
using Common.Helper;
using Microsoft.AspNet.Identity;

namespace Asiri_ERP_Capas.Controllers.Maestro
{
    
    public class RedSocialController : Controller
    {
        private AsiriContext db = new AsiriContext();
        private RedSocialBL redSocialBL = new RedSocialBL();

        public ActionResult Index()
        {
            #region
            var idusuario = User.Identity.GetUserId();

            if (idusuario == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var custom = db.CustomPermiso.Where(x => x.UserID == idusuario).ToList();
            var MenuUrl = db.Menu.SingleOrDefault(x => x.MenuURL == UrlSystem.UrlRedSocial);
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
            return View(redSocialBL.Listar());
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(MSTt03_redSocial redSocial)
        {
            if (ModelState.IsValid)
            {
                redSocialBL.agregarRedSocial(redSocial);
                return RedirectToAction("Index");
            }
            return View(redSocial);
        }

        public ActionResult Edit(short id)
        {
            MSTt03_redSocial redSocial = redSocialBL.obtenerRedSocial(id);
            return View(redSocial);
        }

        [HttpPost]
        public ActionResult Edit(MSTt03_redSocial redSocial)
        {
            if (ModelState.IsValid)
            {
                if (redSocialBL.actualizarRedSocial(redSocial))
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return View(redSocial);
                }
            }
            return View(redSocial);
        }

        public ActionResult Delete(int id)
        {
            if (redSocialBL.eliminarRedSocial(id))
                return RedirectToAction("Index");
            else
                return RedirectToAction("Index");
        }
    }
}