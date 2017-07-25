namespace Common.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class CLIt02_archivoEstudioCompl
    {
        [Key]
        public long idArchivoEstudioCompl { get; set; }

        [Required]
        public byte[] pathArchivo { get; set; }

        public long idEstudioCompl { get; set; }

        public virtual CLlt10_estudioCompl CLlt10_estudioCompl { get; set; }
    }
}
