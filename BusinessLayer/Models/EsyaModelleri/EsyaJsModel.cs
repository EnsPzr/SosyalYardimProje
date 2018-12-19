using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Models.EsyaModelleri
{
    public class EsyaJsModel
    {
        public bool BasariliMi { get; set; }

        public int? EsyaSayisi { get; set; }

        public List<EsyaModel> EsyaList { get; set; }
    }
}
