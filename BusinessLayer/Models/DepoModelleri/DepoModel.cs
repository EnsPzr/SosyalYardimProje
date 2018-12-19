using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer.Models.EsyaModelleri;
using BusinessLayer.Models.OrtakModeller;

namespace BusinessLayer.Models
{
    public class DepoModel
    {
        public int? DepoEsyaId { get; set; }
        public int? EsyaId { get; set; }

        public String EsyaAdi { get; set; }

        public SehirModel Sehir { get; set; }

        public int? Adet { get; set; }
    }
}
