using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Models.KasaModelleri
{
    public class KasaJsModel
    {
        public bool BasariliMi { get; set; }

        public int? KasaSayisi { get; set; }

        public List<KasaModel> KasaList { get; set; }
    }
}
