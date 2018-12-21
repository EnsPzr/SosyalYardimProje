using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Models.BagisciModelleri
{
    public class BagisciJSModel
    {
        public bool BasariliMi { get; set; }

        public int? BagisciSayisi { get; set; }

        public List<BagisciModel> BagisciList { get; set; }
    }
}
