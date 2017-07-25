using Asiri_ERP_Capas.DA;
using Common;
using Service.Auth;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Asiri_ERP_Capas.ViewModels;
using Microsoft.AspNet.Identity.Owin;
using Model;
using Microsoft.AspNet.Identity;
using Common.Helper;

namespace Asiri_ERP_Capas.Controllers
{
    
    public class UsersController : Controller
    {
        private AsiriContext db = new AsiriContext();
        public UserDA rda = new UserDA();

        private ApplicationUserManager _userManager;

        public UsersController()
        {
        }

        public UsersController(ApplicationUserManager userManager)
        {
            UserManager = userManager;

        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().Get<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }



        // GET: Users
        public ActionResult Index()
        {
            #region
            var idusuario = User.Identity.GetUserId();

            if (idusuario == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var custom = db.CustomPermiso.Where(x => x.UserID == idusuario).ToList();
            var MenuUrl = db.Menu.SingleOrDefault(x => x.MenuURL == UrlSystem.UrlUsers);
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



            var aspNetUsers = db.AspNetUsers.Include(a => a.RHUt01_empleado);

            return View(aspNetUsers.ToList());
        }


        //GetEmpleados
        public JsonResult getEmpleados(string term)
        {
            List < string > empleados;
            empleados = db.RHUt01_empleado.Where(x => x.RHUt09_persona.numDocIdentidad.StartsWith(term)).Select(x => x.RHUt09_persona.numDocIdentidad + "," + x.RHUt09_persona.apellidoPaterno + "," + x.RHUt09_persona.apellidoMaterno + "," + x.RHUt09_persona.nombrePersona).ToList();
            return Json(empleados, JsonRequestBehavior.AllowGet);
        }

        // GET: Users/Details/5
        public async Task<ActionResult> Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var user = await UserManager.FindByIdAsync(id);
            var aspNetUsers = db.AspNetUsers.Include(a => a.RHUt01_empleado);
            return View(new UserViewModel()
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                RolesList = db.AspNetRoles.ToList().Select(x => new SelectListItem()
                {
                    Selected = rda.tieneRol(x.Id, user.Id),
                    Text = x.Name,
                    Value = x.Id
                })
            });
        }

        // GET: Users/Create
        public ActionResult Create()
        {
            ViewBag.Empleado = new SelectList(db.RHUt01_empleado.Select(x => x.RHUt09_persona), "idPersona", "nombrePersona");
            return View();
        }

        // POST: Users/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(AspNetUsers model, string[] roles)
        {
            var user = new ApplicationUser() { Email = model.Email, UserName = model.Email };
            var result = await UserManager.CreateAsync(user);
            var UserRoles = new List<UserRolViewModel>();
            if (result.Succeeded)
            {
                new UserDA().RegistrarUsuarioRol(new UserRolViewModel { UserId = user.Id, RolId = roles[0] });
            }

            ViewBag.Empleado = new SelectList(db.RHUt01_empleado.Select(x => x.RHUt09_persona), "idPersona", "nombrePersona");
            return View(model);
        }

        // GET: Users/Edit/5
        public async Task<ActionResult> Edit(string id)
        {
            //ViewBag.Empleado = new SelectList(db.RHUt01_empleado.Select(x => x.RHUt09_persona), "idPersona", "nombrePersona");
            //return View(aspNetUsers);

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AspNetUsers aspNetUs = db.AspNetUsers.Find(id);
            if (aspNetUs == null)
            {
                return HttpNotFound();
            }
            var user = await UserManager.FindByIdAsync(id);
            var aspNetUsers = db.AspNetUsers.Include(a => a.RHUt01_empleado);
            ViewBag.idEmpleado = new SelectList(db.RHUt01_empleado.Select(x => x.RHUt09_persona), "idPersona", "nombrePersona", aspNetUs.RHUt01_empleado.idPersona);
            EditUserViewModel uvm = new EditUserViewModel()
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                RolesList = db.AspNetRoles.ToList().Select(x => new SelectListItem()
                {
                    Selected = rda.tieneRol(x.Id, user.Id),
                    Text = x.Name,
                    Value = x.Id
                })
            };
            return View(uvm);
        }

        // POST: Users/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(EditUserViewModel aspNetUsers, string[] roles)
        {
            if (ModelState.IsValid)
            {
                var user = await UserManager.FindByIdAsync(aspNetUsers.Id);
                if (user == null)
                {
                    return HttpNotFound();
                }
                aspNetUsers.idEmpleado = (from e in db.RHUt01_empleado where aspNetUsers.idEmpleado == e.idPersona select e.idEmpleado).First();
                user.UserName = aspNetUsers.UserName;
                user.Email = aspNetUsers.Email;
                user.idEmpleado = aspNetUsers.idEmpleado;

                await UserManager.UpdateAsync(user);

                rda.ActualizarRolUser(aspNetUsers.Id, roles[0]);

                return RedirectToAction("Index");
            }
            ViewBag.Empleado = new SelectList(db.RHUt01_empleado.Select(x => x.RHUt09_persona), "idPersona", "nombrePersona");
            return View(aspNetUsers);
        }

        // GET: Users/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AspNetUsers aspNetUsers = db.AspNetUsers.Find(id);
            if (aspNetUsers == null)
            {
                return HttpNotFound();
            }
            return View(aspNetUsers);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            var user = db.AspNetUsers.Find(id);
            rda.EliminarUserRol(user.Id);
            db.AspNetUsers.Remove(user);
            db.SaveChanges();
            return RedirectToAction("Index");
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