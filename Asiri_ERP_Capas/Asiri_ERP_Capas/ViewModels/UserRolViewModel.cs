using Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Asiri_ERP_Capas.ViewModels
{
    public class UserRolViewModel
    {
        [Key, Column(Order = 0)]
        public string UserId { get; set; }

        [Key, Column(Order = 1)]
        [Required]
        public string RolId { get; set; }

        public virtual AspNetUsers AspNetUsers { get; set; }
        public virtual AspNetRoles AspNetRoles { get; set; }
    }

}