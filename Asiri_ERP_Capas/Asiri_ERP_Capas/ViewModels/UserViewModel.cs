using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Asiri_ERP_Capas.ViewModels
{
    public class UserViewModel
    {

        public string Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }

        public IEnumerable<SelectListItem> RolesList { get; set; }
        public RHUt01_empleado RHUt01_empleado { get; set; }
        public RHUt09_persona RHUt09_persona { get; set; }

    }

    public class EditUserViewModel
    {

        public string Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }

        public IEnumerable<SelectListItem> RolesList { get; set; }
        public long idEmpleado { get; set; }
    }
}