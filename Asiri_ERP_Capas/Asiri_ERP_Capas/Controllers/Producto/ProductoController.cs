using System;
using Service.Producto;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Common.Helper;
using Common;
using Microsoft.AspNet.Identity;
using Asiri_ERP_Capas.DA;

namespace Asiri_ERP.Controllers.Producto
{
    public class ProductoController : Controller
    {
        private AsiriContext db = new AsiriContext();

        // GET: Producto
        public ActionResult Index()
        {
            #region
            var idusuario = User.Identity.GetUserId();

            if (idusuario == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var custom = db.CustomPermiso.Where(x => x.UserID == idusuario).ToList();
            var MenuUrl = db.Menu.SingleOrDefault(x => x.MenuURL == UrlSystem.UrlProducto);
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

            var model = new ProductoBL().List();
            return View(model);
        }
        // GET: Producto/Create
        public ActionResult Create()
        {
            string codigo;
            do
            {
                codigo = DateTime.Now.DateForCode();
            } while (db.PROt02_producto.Any(x => x.codProducto == codigo));
            Session["codProd"] = codigo;
            ViewData["idCategoria"]  = new SelectList(db.PROt01_categoria, "idCategoria", "nombreCategoria");
            return View();
        }

        // POST: Producto/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idProducto,codProducto,codBarra,nombreProductoComercial,descProducto,obsvProducto,activo,fecRegistro,idUsuario,idCategoria,idMoneda")] Common.Model.PROt02_producto model)
        {
            string cod = Session["codProd"].ToString();
            model.codProducto = cod;
            model.codBarra = cod;
            model.idUsuario = User.Identity.GetUserId();
            if (ModelState.IsValid)
            {
                new ProductoBL().Add(model);
                Session["codProd"] = null;
                return RedirectToAction("Index");
            }

            ViewBag.idCategoria = new SelectList(db.PROt01_categoria, "idCategoria", "nombreCategoria", model.idCategoria);
            return View(model);
        }

        // GET: Producto/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Common.Model.PROt02_producto model = new ProductoBL().GetProduct(id);
            if (model == null)
            {
                return HttpNotFound();
            }
            ViewBag.idCategoria = new SelectList(db.PROt01_categoria, "idCategoria", "nombreCategoria", model.idCategoria);
            return View(model);
        }

        // POST: Producto/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idProducto,codProducto,codBarra,nombreProductoGenerico,nombreProductoComercial,descProducto,obsvProducto,activo,fecRegistro,fecModificacion,fecEliminacion,idUsuario,idUsuarioModificar,idUsuarioEliminar,idCategoria,idMoneda")] Common.Model.PROt02_producto model)
        {
            model.idUsuarioModificar = User.Identity.GetUserId();
            if (ModelState.IsValid)
            {
                new ProductoBL().Edit(model);
                return RedirectToAction("Index");
            }
            ViewBag.idCategoria = new SelectList(db.PROt01_categoria, "idCategoria", "nombreCategoria", model.idCategoria);
            return View(model);
        }

        public PartialViewResult GetProduct(int id)
        {
            Common.Model.PROt02_producto model = new ProductoBL().GetProduct(id);
            return PartialView("Modal/_DetailProduct", model);
        }
        [HttpPost]
        public JsonResult QuickCreate(string nom, string desc)
        {
            // falta validar del lado de servidor
            var model = new Common.Model.PROt01_categoria { nombreCategoria = nom, descCategoria = desc };
            if (ModelState.IsValid)
            {
                new CategoriaBL().Add(model);
            }
            return Json(model);
        }
        public PartialViewResult CreateCategory()
        {
            return PartialView("Modal/_CreateCategory");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public ActionResult ChangeStatus(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Common.Model.PROt02_producto model = new ProductoBL().GetProduct(id);
            new ProductoBL().ChangeStatus(model);
            if (model == null)
            {
                return HttpNotFound();
            }
            if (!model.activo)
            {
                model.idUsuarioEliminar = User.Identity.GetUserId();
            }
            else
            {
                model.idUsuarioModificar = User.Identity.GetUserId();
            }
            return RedirectToAction("Index");
        }
    }
}