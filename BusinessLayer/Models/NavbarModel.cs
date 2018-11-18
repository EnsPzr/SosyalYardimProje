using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Models
{
    public class NavbarModel
    {
        public String UrlText { get; set; }

        public String UrlYol { get; set; }

        public bool AltKategoriMi { get; set; }

        public int? AltKategoriSayisi { get; set; }
        
        public List<NavbarModel> altKategoriler { get; set; }
    }
}
