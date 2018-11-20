using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Models.KullaniciModelleri
{
    public class KullaniciFiltrelemeModel
    {
        public String AraTxt { get; set; }
        public int? SehirId { get; set; }
        public bool? OnayliMi { get; set; }
        public bool? MerkezdeMi { get; set; }
        public bool? AktifMi { get; set; }
    }
}
