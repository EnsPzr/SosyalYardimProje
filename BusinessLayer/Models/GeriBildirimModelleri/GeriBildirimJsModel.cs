using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Models.GeriBildirimModelleri
{
    public class GeriBildirimJsModel
    {
        public bool BasariliMi { get; set; }

        public int? GeriBildirimSayisi { get; set; }

        public List<GeriBildirimModel> GeriBildirimList { get; set; }
    }
}
