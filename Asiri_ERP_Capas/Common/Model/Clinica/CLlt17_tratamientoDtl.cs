namespace Common.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class CLlt17_tratamientoDtl
    {
        [Key]
        public long idTratamientoDtl { get; set; }

        public bool esServicio { get; set; }

        [StringLength(300)]
        public string indicacionServicio { get; set; }

        public long idTratamiento { get; set; }

        public long? idProducto { get; set; }

        public long? idServicio { get; set; }

        public virtual CLlt16_tratamiento CLlt16_tratamiento { get; set; }

        public virtual PROt02_producto PROt02_producto { get; set; }

        public virtual PROt04_servicio PROt04_servicio { get; set; }
    }
}
