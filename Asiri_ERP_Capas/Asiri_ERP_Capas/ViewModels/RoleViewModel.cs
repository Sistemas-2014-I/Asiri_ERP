using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Asiri_ERP_Capas.ViewModels
{
    public class RoleViewModel
    {
        public RoleViewModel()
        {
        }

        public RoleViewModel(ApplicationRole role)
        {
            Id = role.Id;
            Name = role.Name;
            Description = role.Description;
        }

        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

    }

    public class EditRoleViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public IEnumerable<SelectListItem> PermisosList { get; set; }
    }
}