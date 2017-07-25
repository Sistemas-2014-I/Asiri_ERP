using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.View.ViewModel
{
    public class NumeracionVM
    {
        [Key]
        public int idNumeracion { get; set; }

        [Required]
        [StringLength(10)]
        public string serie { get; set; }

        [Required]
        [StringLength(10)]
        public string num_corr_inicial { get; set; }

        [Required]
        [StringLength(10)]
        public string num_corr_final { get; set; }

        [Required]
        [StringLength(10)]
        public string num_corr_actual { get; set; }

        public string tipoComprobante { get; set; }

        public string  sucursal { get; set; }

        //[Column(TypeName = "datetime2")]
        //public DateTime fecRegistro { get; set; }

        //[Required]
        //[StringLength(128)]
        //public string idUsuario { get; set; }

        //[Column(TypeName = "datetime2")]
        //public DateTime? fecModificacion { get; set; }

        //[StringLength(128)]
        //public string idUsuarioModificar { get; set; }
    }
}
