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
    [Authorize(Roles = "Administrador")]
    public class SucursalController : Controller
    {
        private AsiriContext db = new AsiriContext();
        private SucursalBL sucursalBL = new SucursalBL();

        public ActionResult Index()
        {
            #region
            var idusuario = User.Identity.GetUserId();

            if (idusuario == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var custom = db.CustomPermiso.Where(x => x.UserID == idusuario).ToList();
            var MenuUrl = db.Menu.SingleOrDefault(x => x.MenuURL == UrlSystem.UrlSucursal);
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
            return View(sucursalBL.Listar());
        }

        public ActionResult Create()
        {
            ViewBag.idTipoSucursal = new SelectList(db.MSTt05_tipoSucursal, "idTipoSucursal", "descTipoSucursal", "--Seleccione--");
            return View();
        }

        [HttpPost]
        public ActionResult Create(MSTt04_sucursal sucursal)
        {
            if (ModelState.IsValid)
            {
                sucursalBL.agregarSucursal(sucursal);
                return RedirectToAction("Index");
            }
            ViewBag.idTipoSucursal = new SelectList(db.MSTt05_tipoSucursal, "idTipoSucursal", "descTipoSucursal", sucursal.idTipoSucursal);
            return View(sucursal);
        }

        public ActionResult Edit(short id)
        {
            MSTt04_sucursal sucursal = sucursalBL.obtenerSucursal(id);
            ViewBag.idTipoSucursal = new SelectList(db.MSTt05_tipoSucursal, "idTipoSucursal", "descTipoSucursal", sucursal.idTipoSucursal);
            return View(sucursal);
        }

        [HttpPost]
        public ActionResult Edit(MSTt04_sucursal sucursal)
        {
            if (ModelState.IsValid)
            {
                if (sucursalBL.actualizarSucursal(sucursal))
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.idTipoSucursal = new SelectList(db.MSTt05_tipoSucursal, "idTipoSucursal", "descTipoSucursal", sucursal.idTipoSucursal);
                    return View(sucursal);
                }
            }
            ViewBag.idTipoSucursal = new SelectList(db.MSTt05_tipoSucursal, "idTipoSucursal", "descTipoSucursal", sucursal.idTipoSucursal);
            return View(sucursal);
        }

        public ActionResult Delete(int id)
        {
            if (sucursalBL.eliminarSucursal(id))
                return RedirectToAction("Index");
            else
                return RedirectToAction("Index");
        }
    }
}