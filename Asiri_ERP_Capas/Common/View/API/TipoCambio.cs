using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.View.API
{
        [JsonObject(MemberSerialization.OptIn)]
        public class TipoCambio
        {
            [JsonProperty]
            public decimal valor_inicial { get; set; }
            [JsonProperty]
            public string moneda_inicial { get; set; }
            [JsonProperty]
            public decimal valor_final { get; set; }
            [JsonProperty]
            public string moneda_final { get; set; }
            [JsonProperty]
            public decimal tasa_compra { get; set; }
            [JsonProperty]
            public DateTime fecha_tasa { get; set; }
        }
}
