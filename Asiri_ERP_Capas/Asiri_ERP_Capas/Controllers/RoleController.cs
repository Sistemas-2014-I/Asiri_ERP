using Asiri_ERP_Capas.DA;
using Asiri_ERP_Capas.ViewModels;
using Common;
using Model;
using Service.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity;
using Common.Helper;

namespace Asiri_ERP_Capas.Controllers
{
    
    public class RoleController : Controller
    {
        private AsiriContext db = new AsiriContext();

        private RolDA rda = new RolDA();

        private ApplicationRoleManager _roleManager;
        private ApplicationUserManager _userManager;

        public RoleController()
        {
        }

        public RoleController(ApplicationUserManager userManager,ApplicationRoleManager roleManager)
        {
            RoleManager = roleManager;
            UserManager = userManager;
        }



        public ApplicationUserManager UserManager
        {
            get
            {

                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            set
            {
                _userManager = value;
            }
        }

        public ApplicationRoleManager RoleManager
        {
            get
            {
                return _roleManager ?? HttpContext.GetOwinContext().Get<ApplicationRoleManager>();
            }
            private set
            {
                _roleManager = value;
            }
        }


        public ActionResult Opciones()
        {
            return View(db.AspNetRoles.ToList());
        }

        // GET: Role
        public ActionResult Index()
        {
            #region
            var idusuario = User.Identity.GetUserId();

            if (idusuario == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var custom = db.CustomPermiso.Where(x => x.UserID == idusuario).ToList();
            var MenuUrl = db.Menu.SingleOrDefault(x => x.MenuURL == UrlSystem.UrlRole);
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


            return View(RoleManager.Roles);
        }

        //get:Create
        public ActionResult Create()
        {
            return View();
        }

        //post
        [HttpPost]
        public async Task<ActionResult> Create(RoleViewModel model)
        {

            if (ModelState.IsValid)
            {
                if(db.AspNetRoles.Where(x=>x.Name == model.Name).Any())
                {
                    ModelState.AddModelError("", "El nombre del rol ya esta en uso");
                }




                var role = new ApplicationRole() { Name = model.Name, Description = model.Description };
                var result = await RoleManager.CreateAsync(role);
                if (!result.Succeeded)
                {
                    ModelState.AddModelError("", result.Errors.First());
                }

                return RedirectToAction("Index");
            }

            
            return View(model);

        }



        //get:Edit
        public async Task<ActionResult> Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var role = await RoleManager.FindByIdAsync(id);
            if (role == null)
            {
                return HttpNotFound();
            }
            RoleViewModel roleModel = new RoleViewModel { Id = role.Id, Name = role.Name, Description = role.Description };
            return View(roleModel);
        }

        //post
        [HttpPost]
        public async Task<ActionResult> Edit(RoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                var role = await RoleManager.FindByIdAsync(model.Id);
                if (role == null)
                {
                    return HttpNotFound();
                }
                role.Name = model.Name;
                role.Description = model.Description;
                await RoleManager.UpdateAsync(role);
                return RedirectToAction("Index");
            }
            return View();
        }

        //Details
        public async Task<ActionResult> Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var role = await RoleManager.FindByIdAsync(id);
            //Obtiene usuarios en este rol
            var users = new List<ApplicationUser>();
            foreach (var user in UserManager.Users.ToList())
            {
                if (await UserManager.IsInRoleAsync(user.Id, role.Name))
                {
                    users.Add(user);
                }
            }
            ViewBag.Users = users;
            ViewBag.UserCount = users.Count();
            return View(role);
        }

        //Delete
        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var role = await RoleManager.FindByIdAsync(id);
            if (role == null)
            {
                return HttpNotFound();
            }
            return View(new RoleViewModel(role));
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string id,string deleteUser)
        {
            if (ModelState.IsValid)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                var role = await RoleManager.FindByIdAsync(id);
                if (role == null)
                {
                    return HttpNotFound();
                }
                IdentityResult result;
                if (deleteUser != null)
                {
                    result = await RoleManager.DeleteAsync(role);
                }
                else
                {
                    result = await RoleManager.DeleteAsync(role);
                }
                if (!result.Succeeded)
                {
                    ModelState.AddModelError("", result.Errors.First());
                    return View();
                }
                return RedirectToAction("Index");
            }
            return View();
        }
    }
}