using Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Asiri_ERP_Capas.ViewModels
{
    public class RolPermisoViewModel
    {
        [Key, Column(Order = 0)]
        public string RoleId { get; set; }

        [Key, Column(Order = 1)]
        public int PermisoId { get; set; }

        public virtual AspNetRoles AspNetRoles { get; set; }
        public virtual AspNetPermisos Permisos { get; set; }
    }
}