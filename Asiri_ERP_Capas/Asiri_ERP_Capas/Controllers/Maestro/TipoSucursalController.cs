using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Service.Maestro;
using Common;
using Common.Helper;
using Asiri_ERP_Capas.DA;
using Microsoft.AspNet.Identity;

namespace Asiri_ERP_Capas.Controllers.Maestro
{
    
    public class TipoSucursalController : Controller
    {
        private AsiriContext db = new AsiriContext();
        private TIpoSucursalBL tipoSucursalBL = new TIpoSucursalBL();

        public ActionResult Index()
        {
            #region
            var idusuario = User.Identity.GetUserId();

            if (idusuario == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var custom = db.CustomPermiso.Where(x => x.UserID == idusuario).ToList();
            var MenuUrl = db.Menu.SingleOrDefault(x => x.MenuURL == UrlSystem.UrlTipoSucursal);
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

            return View(tipoSucursalBL.Listar());
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(MSTt05_tipoSucursal tipoSucursal)
        {
            if (ModelState.IsValid)
            {
                tipoSucursalBL.agregarTipoSucursal(tipoSucursal);
                return RedirectToAction("Index");
            }
            return View(tipoSucursal);
        }

        public ActionResult Edit(short id)
        {
            MSTt05_tipoSucursal tipoSucursal = tipoSucursalBL.obtenerTipoSucursal(id);
            return View(tipoSucursal);
        }

        [HttpPost]
        public ActionResult Edit(MSTt05_tipoSucursal tipoSucursal)
        {
            if (ModelState.IsValid)
            {
                if (tipoSucursalBL.actualizarTipoSucursal(tipoSucursal))
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return View(tipoSucursal);
                }
            }
            return View(tipoSucursal);
        }

        public ActionResult Delete(int id)
        {
            if (tipoSucursalBL.eliminarTipoSucursal(id))
                return RedirectToAction("Index");
            else
                return RedirectToAction("Index");
        }
    }
}