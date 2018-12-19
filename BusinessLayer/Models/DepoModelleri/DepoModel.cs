using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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

        [Required(ErrorMessage = "Eşya seçilmesi zorunludur")]
        public int? EsyaId { get; set; }

        public String EsyaAdi { get; set; }

        public SehirModel Sehir { get; set; }

        [Required(ErrorMessage = "Adet girilmek zorundadır.")]
        public int? Adet { get; set; }
    }
}
