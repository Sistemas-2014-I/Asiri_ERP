namespace Common
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CustomPermiso")]
    public partial class CustomPermiso
    {
        public int CustomPermisoID { get; set; }

        [Required]
        [StringLength(128)]
        public string UserID { get; set; }

        public int MenuID { get; set; }

        public virtual AspNetUsers AspNetUsers { get; set; }

        public virtual Menu Menu { get; set; }
    }
}
