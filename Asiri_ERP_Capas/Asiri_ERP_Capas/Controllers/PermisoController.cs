using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using Microsoft.AspNet.Identity;
using System.Net;
using Common;
using Common.Helper;
using Asiri_ERP_Capas.DA;
using System.Linq;
using System.Collections.Generic;

using System;

namespace Asiri_ERP_Capas.Controllers
{
    public class PermisoController : Controller
    {
        private AsiriContext db = new AsiriContext();

        public ActionResult Index()
        {
            #region
            var idusuario = User.Identity.GetUserId();
            var custom = db.CustomPermiso.Where(x => x.UserID == idusuario).ToList();
            if (idusuario == null)
            {
                return RedirectToAction("Login","Account");

            }
            var MenuUrl = db.Menu.SingleOrDefault(x => x.MenuURL == UrlSystem.UrlPermiso);
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
                return RedirectToAction("Error", "Account");
            }

            #endregion


            ViewBag.RoleID = new SelectList(db.AspNetRoles, "Id", "Name");
            ViewBag.UserID = new SelectList(db.AspNetUsers, "Id", "Email");
            IEnumerable<PermisoMenu> menus = db.Database.SqlQuery<PermisoMenu>("SP_GetMenu @UserId ='" + User.Identity.GetUserId() + "', @RoleId = NULL");
            ViewBag.Menus = from a in db.Menu
                            where a.MenuID == 0
                            select a;
            var permission = db.Permiso.Include(r => r.Menu).Include(s => s.AspNetRoles);
            return View(permission.ToList());
        }


        [HttpPost]
        public ActionResult Index(string RoleID, string UserID)
        {

            IEnumerable<PermisoMenu> menus = db.Database.SqlQuery<PermisoMenu>("SP_GetMenu @UserId ='" + User.Identity.GetUserId() + "',@RoleId = NULL");
            if (!string.IsNullOrEmpty(UserID))
            {
                menus = null;

                menus = db.Database.SqlQuery<PermisoMenu>("SP_GetMenu @UserId ='" + UserID + "', @RoleId = NULL");
            }
            if (!string.IsNullOrEmpty(RoleID))
            {
                menus = null;
                menus = db.Database.SqlQuery<PermisoMenu>("SP_GetMenu @UserId =NULL, @RoleId = '" + RoleID + "'");
            }

            ViewBag.RoleID = new SelectList(db.AspNetRoles, "Id", "Name");
            ViewBag.UserID = new SelectList(db.AspNetUsers, "Id", "Email");

            ViewBag.Menus = menus.ToList();
            return View();
        }

        [HttpPost]
        public ActionResult SavePermiso(string query, string qtype, string id)
        {
            string fquery = "{\"PERMISO\":" + query + "}";

            if (qtype == "r")
            {
                /*DELETING ROWS*/
                if (query == "[]")
                {
                    db.Database.ExecuteSqlCommand($"DELETE FROM Permiso WHERE RoleID = '{id}'");
                }
                else
                {
                    var data = Newtonsoft.Json.Linq.JObject.Parse(fquery);
                    String roleId = Convert.ToString(data["PERMISO"][0]["RoleID"]);

                    var rows = from r in db.Permiso
                               where r.RoleID == roleId
                               select r;
                    foreach (var row in rows)
                    {
                        db.Permiso.Remove(row);
                    }
                    db.SaveChanges();

                    int menu;
                    string role;
                    for (int i = 0; i < data["PERMISO"].Count(); i++)
                    {
                        menu = (int)data["PERMISO"][i]["MenuID"];
                        role = (string)data["PERMISO"][i]["RoleID"];
                        db.Permiso.Add(new Permiso { MenuID = menu, RoleID = role });
                    }
                    try
                    {
                        db.SaveChanges();
                        TempData["ResultMessage"] = "Los datos se guardaron correctamente";
                        TempData["ResultType"] = "S";
                    }
                    catch (Exception ex)
                    {

                        TempData["ResultMessage"] = "Error al guardar los datos! " + ex.Message;
                        TempData["ResultType"] = "E";
                    }
                }
            }
            if (qtype == "u")
            {
                if (query == "[]")
                {
                    db.Database.ExecuteSqlCommand($"DELETE FROM CustomPermiso WHERE UserID = '{id}'");
                }
                else
                {

                    /*DELETING ROWS*/
                    var data = Newtonsoft.Json.Linq.JObject.Parse(fquery);
                    String userId = Convert.ToString(data["PERMISO"][0]["UserID"]);
                    var rows = from r in db.CustomPermiso
                               where r.UserID == userId
                               select r;
                    foreach (var row in rows)
                    {
                        db.CustomPermiso.Remove(row);
                    }
                    db.SaveChanges();

                    int menu;
                    string user;
                    for (int i = 0; i < data["PERMISO"].Count(); i++)
                    {
                        menu = (int)data["PERMISO"][i]["MenuID"];
                        user = (String)data["PERMISO"][i]["UserID"];
                        db.CustomPermiso.Add(new CustomPermiso { MenuID = menu, UserID = user });
                    }
                    try
                    {
                        db.SaveChanges();
                        TempData["ResultMessage"] = "Los datos se guardaron correctamente";
                        TempData["ResultType"] = "S";
                    }
                    catch (Exception ex)
                    {

                        TempData["ResultMessage"] = "Error al guardar los datos! " + ex.Message;
                        TempData["ResultType"] = "E";
                    }
                }

            }


            return RedirectToAction("Index");
        }



        //MENU
        public ActionResult MenuIndex()
        {
            return View(db.Menu.ToList());
        }

        //Details
        public ActionResult MenuDetails(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Menu menu = db.Menu.Find(id);
            if (menu == null)
            {
                return HttpNotFound();
            }
            return View(menu);
        }

        //Crear
        //Get
        public ActionResult MenuCreate()
        {
            return View();
        }
        //Post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult MenuCreate([Bind(Include = "MenuID,DisplayName,ParentMenuID,OrderNumber,MenuURL,MenuIcon")] Menu menu)
        {
            if (ModelState.IsValid)
            {
                db.Menu.Add(menu);
                db.SaveChanges();
                return RedirectToAction("MenuIndex");
            }

            return View(menu);
        }

        //Editar
        //Get
        public ActionResult MenuEdit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Menu menu = db.Menu.Find(id);
            if (menu == null)
            {
                return HttpNotFound();
            }
            return View(menu);
        }
        //Post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult MenuEdit([Bind(Include = "MenuID,DisplayName,ParentMenuID,OrderNumber,MenuURL,MenuIcon")] Menu menu)
        {
            if (ModelState.IsValid)
            {
                db.Entry(menu).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("MenuIndex");
            }
            return View(menu);
        }

        //Delete
        //Get
        public ActionResult MenuDelete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Menu menu = db.Menu.Find(id);
            if (menu == null)
            {
                return HttpNotFound();
            }
            return View(menu);
        }
        //Post
        [HttpPost, ActionName("MenuDelete")]
        [ValidateAntiForgeryToken]
        public ActionResult MenuDeleteConfirmed(int id)
        {
            Menu menu = db.Menu.Find(id);
            db.Menu.Remove(menu);
            db.SaveChanges();
            return RedirectToAction("MenuIndex");
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