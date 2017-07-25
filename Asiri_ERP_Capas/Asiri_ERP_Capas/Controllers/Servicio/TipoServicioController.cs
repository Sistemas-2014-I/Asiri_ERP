using Service.Servicio;
using System.Net;
using System.Web.Mvc;
using Common;
using Common.Helper;
using Microsoft.AspNet.Identity;
using Asiri_ERP_Capas.DA;
using System.Linq;

namespace Asiri_ERP.Controllers.Servicio
{
    public class TipoServicioController : Controller
    {
        private AsiriContext db = new AsiriContext();
        // GET: TipoServicio
        public ActionResult Index()
        {
            #region
            var idusuario = User.Identity.GetUserId();

            if (idusuario == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var custom = db.CustomPermiso.Where(x => x.UserID == idusuario).ToList();
            var MenuUrl = db.Menu.SingleOrDefault(x => x.MenuURL == UrlSystem.UrlTipoServicio);
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
            var model = new TipoServicioBL().List();
            return View(model);
        }
        // GET: TipoServicio/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TipoServicio/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idTipoDeServicio,descTipoDeServicio,activo,fecRegistro,fecModificacion,fecEliminacion,idUsuario,idUsuarioModificar,idUsuarioEliminar")] PROt05_tipoDeServicio model)
        {
            model.idUsuario = User.Identity.GetUserId();
            if (ModelState.IsValid)
            {
                new TipoServicioBL().Add(model);
                return RedirectToAction("Index");
            }
            return View(model);
        }

        // GET: TipoServicio/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PROt05_tipoDeServicio model = new TipoServicioBL().GetTypeService(id);
            if (model == null)
            {
                return HttpNotFound();
            }
            return View(model);
        }

        // POST: TipoServicio/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idTipoDeServicio,descTipoDeServicio,activo,fecRegistro,fecModificacion,fecEliminacion,idUsuario,idUsuarioModificar,idUsuarioEliminar")] PROt05_tipoDeServicio model)
        {
            model.idUsuarioModificar = User.Identity.GetUserId();
            if (ModelState.IsValid)
            {
                new TipoServicioBL().Edit(model);
                return RedirectToAction("Index");
            }
            return View(model);
        }
        // GET: TipoServicio/GetServiceType/5
        public ActionResult GetServiceType(string id)
        {
            PROt05_tipoDeServicio model = new TipoServicioBL().GetTypeService(int.Parse(id));
            return PartialView("_DetailServiceType", model);
        }
        // solo cambia el estado, no edita
        // GET: TipoServicio/ChangeStatus/5
        public ActionResult ChangeStatus(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PROt05_tipoDeServicio model = new TipoServicioBL().GetTypeService(id);
            new TipoServicioBL().ChangeStatus(model);
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