using System;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Common.Helper;
using System.Data.Entity;
using Common;
using Microsoft.AspNet.Identity;
using Service.Servicio;
using Asiri_ERP_Capas.DA;

namespace Asiri_ERP.Controllers.Servicio
{
    public class ServicioController : Controller
    {
        private AsiriContext db = new AsiriContext();

        // GET: Servicio
        public ActionResult Index()
        {
            #region
            var idusuario = User.Identity.GetUserId();

            if (idusuario == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var custom = db.CustomPermiso.Where(x => x.UserID == idusuario).ToList();
            var MenuUrl = db.Menu.SingleOrDefault(x => x.MenuURL == UrlSystem.UrlServicio);
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
            var model = new ServicioBL().List();
            return View(model);
        }
        // GET: Servicio/Create
        public ActionResult Create()
        {
            string codigo;
            do
            {
                codigo = DateTime.Now.DateForCode();
            } while (db.PROt02_producto.Any(x => x.codProducto == codigo));
            Session["codserv"] = codigo;
            ViewBag.idTipoDeServicio = new SelectList(db.PROt05_tipoDeServicio, "idTipoDeServicio", "descTipoDeServicio");
            //ViewBag.idTipoDeEspeciliadad = new SelectList(db.RHUt13_tipoEspecialidad, "idTipoDeEspeciliadad", "nombreEspecialidad");
            ViewBag.idEspecialidad = new SelectList(db.RHUt04_especialidad, "idEspecialidad", "nombreEspecialidad");
            return View();
        }
        // POST: Servicio/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idServicio,codServicio,nombreServicio,descServicio,precio,precioMinimo,precioMaximo,esGratis,activo,fecRegistro,fecModificacion,fecEliminacion,idUsuario,idUsuarioModificar,idUsuarioEliminar,idTipoDeServicio,idEspecialidad",Exclude = "idTipoDeEspeciliadad")] PROt04_servicio model)
        {
            string cod = Session["codserv"].ToString();
            model.codServicio = cod;
            var max = model.precioMaximo;
            var min = model.precioMinimo;
            var precio = model.precio;
            if (min != null && max != null)
            {
                if (min >= max)
                    ModelState.AddModelError("precioMaximo", "El precio máximo debe ser mayor al mínimo");
                    ModelState.AddModelError("precioMinimo", "El precio mínimo debe ser menor al máximo");
            }
            if (precio <= min)
                ModelState.AddModelError("precioMinimo", "El precio mínimo debe ser menor al campo precio");
            if (precio >= max)
                ModelState.AddModelError("precioMaximo", "El precio máximo debe ser mayor al campo precio");
            //if (model.idEspecialidad==0)
            //    ModelState.AddModelError("idEspecialidad", "Debe seleccionar una especialidad");
            model.idUsuario = User.Identity.GetUserId();
            if (ModelState.IsValid)
            {
                new ServicioBL().Add(model);
                Session["codserv"] = null;
                return RedirectToAction("Index");
            }
            ViewBag.idTipoDeServicio = new SelectList(db.PROt05_tipoDeServicio, "idTipoDeServicio", "descTipoDeServicio", model.idTipoDeServicio);
            //ViewBag.idTipoDeEspeciliadad = new SelectList(db.RHUt13_tipoEspecialidad, "idTipoDeEspeciliadad", model.);
            ViewBag.idEspecialidad = new SelectList(db.RHUt04_especialidad, "idEspecialidad", "nombreEspecialidad", model.idEspecialidad);
            return View(model);
        }

        // GET: Servicio/Edit/5
        public ActionResult Edit(long? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PROt04_servicio model = new ServicioBL().GetService(id);
            if (model == null)
            {
                return HttpNotFound();
            }
            ViewBag.idTipoDeServicio = new SelectList(db.PROt05_tipoDeServicio, "idTipoDeServicio", "descTipoDeServicio", model.idTipoDeServicio);
            ViewBag.idEspecialidad = new SelectList(db.RHUt04_especialidad, "idEspecialidad", "nombreEspecialidad", model.idEspecialidad);
            ViewBag.codServicio = model.codServicio;
            return View(model);
        }

        // POST: Servicio/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idServicio,codServicio,nombreServicio,descServicio,precio,precioMinimo,precioMaximo,esGratis,activo,fecRegistro,fecModificacion,fecEliminacion,idUsuario,idUsuarioModificar,idUsuarioEliminar,idTipoDeServicio,idEspecialidad")] PROt04_servicio model)
        {
            var max = model.precioMaximo;
            var min = model.precioMinimo;
            var precio = model.precio;
            if (min != null && max != null)
            {
                if (min >= max)
                {
                    ModelState.AddModelError("precioMaximo", "El precio máximo debe ser mayor al mínimo");
                    ModelState.AddModelError("precioMinimo", "El precio mínimo debe ser menor al máximo");
                }
            }
            if (precio <= min)
                ModelState.AddModelError("precioMinimo", "El precio mínimo debe ser menor al campo precio");
            if (precio >= max)
                ModelState.AddModelError("precioMaximo", "El precio máximo debe ser mayor al campo precio");
            model.idUsuarioModificar = User.Identity.GetUserId();
            if (ModelState.IsValid)
            {
                new ServicioBL().Edit(model);
                return RedirectToAction("Index");
            }
            ViewBag.idTipoDeServicio = new SelectList(db.PROt05_tipoDeServicio, "idTipoDeServicio", "descTipoDeServicio", model.idTipoDeServicio);
            ViewBag.idEspecialidad = new SelectList(db.RHUt04_especialidad, "idEspecialidad", "nombreEspecialidad", model.idEspecialidad);
            return View(model);
        }

        public ActionResult GetService(string id)
        {
            PROt04_servicio model = new ServicioBL().GetService(int.Parse(id));
            return PartialView("Modal/_DetailService", model);
        }
        public ActionResult ChangeStatus(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PROt04_servicio model = new ServicioBL().GetService(id);
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
            new ServicioBL().ChangeStatus(model);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult GetServices(int id)
        {
            //var services = new ServicioBL().GetServicesByTypeService(id);
            db.Configuration.ProxyCreationEnabled = false;
            var services = db.PROt04_servicio.Include(p => p.PROt05_tipoDeServicio).Include(p => p.RHUt04_especialidad).Where(p => p.idTipoDeServicio == id).Select(x=> new {x.idServicio,x.nombreServicio}).ToList();
            return Json(services);
        }
        [HttpPost]
        public JsonResult QuickCreate(string desc)
        {
            // falta validar del lado de servidor
            var model = new PROt05_tipoDeServicio() {descTipoDeServicio = desc};
            //var models = new PROt01_categoria { nombreCategoria = nom, descCategoria = desc };
            if (ModelState.IsValid)
            {
                new TipoServicioBL().Add(model);
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
    }
}