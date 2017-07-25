using Asiri_ERP_Capas.DA;
using Common;
using Common.Helper;
using Microsoft.AspNet.Identity;
using Service.Producto;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace Asiri_ERP.Controllers.Producto
{
    public class CategoriaController : Controller
    {
        private AsiriContext db = new AsiriContext();

        public ActionResult Index()
        {
            #region
            var idusuario = User.Identity.GetUserId();

            if (idusuario == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var custom = db.CustomPermiso.Where(x => x.UserID == idusuario).ToList();
            var MenuUrl = db.Menu.SingleOrDefault(x => x.MenuURL == UrlSystem.UrlCategoria);
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


            var model = new CategoriaBL().List();
            return View(model);
        }
        // GET: Categoria/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Categoria/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idCategoria,fecRegistro,idUsuario,nombreCategoria,descCategoria,activo")] Common.Model.PROt01_categoria model)
        {
            model.idUsuario = User.Identity.GetUserId();
            if (ModelState.IsValid)
            {
                new CategoriaBL().Add(model);
                return RedirectToAction("Index");
            }
            return View(model);
        }

        // GET: Categoria/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Common.Model.PROt01_categoria model = new CategoriaBL().GetCategory(id);
            if (model == null)
            {
                return HttpNotFound();
            }
            return View(model);
        }

        // POST: Categoria/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idCategoria,fecRegistro,fecModificacion,fecEliminacion,idUsuario,idUsuarioModificar,idUsuarioEliminar,nombreCategoria,descCategoria,activo")] Common.Model.PROt01_categoria model)
        {
            model.idUsuarioModificar = User.Identity.GetUserId();
            if (ModelState.IsValid)
            {
                new CategoriaBL().Edit(model);
                return RedirectToAction("Index");
            }
            return View(model);
        }
        // GET: Categoria/GetCategory/5
        public ActionResult GetCategory(string id)
        {
            Common.Model.PROt01_categoria model = new CategoriaBL().GetCategory(int.Parse(id));
            return PartialView("_DetailCategory", model);
        }
        // GET: Categoria/ChangeStatus/5
        public ActionResult ChangeStatus(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Common.Model.PROt01_categoria model = new CategoriaBL().GetCategory(id);
            new CategoriaBL().ChangeStatus(model);
            if (model.activo)
            {
                model.idUsuarioEliminar = User.Identity.GetUserId();
            }
            else
            {
                model.idUsuarioModificar = User.Identity.GetUserId();
            }
            if (model == null)
            {
                return HttpNotFound();
            }
            return RedirectToAction("Index");
        }
    }
}