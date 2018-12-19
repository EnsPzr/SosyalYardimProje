using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Models.DepoModelleri
{
    public class DepoJsModel
    {
        public bool BasariliMi { get; set; }

        public int? DepoEsyaSayisi { get; set; }

        public List<DepoModel> DepoList { get; set; }
    }
}
