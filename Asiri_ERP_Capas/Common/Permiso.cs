namespace Common
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Permiso")]
    public partial class Permiso
    {
        public int PermisoID { get; set; }

        [Required]
        [StringLength(128)]
        public string RoleID { get; set; }

        public int MenuID { get; set; }

        public virtual AspNetRoles AspNetRoles { get; set; }

        public virtual Menu Menu { get; set; }
    }
}
