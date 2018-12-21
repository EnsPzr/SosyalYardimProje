using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Models.TeslimAlinacakBagis
{
    public class TeslimAlinacakBagisJsModel
    {
        public bool BasariliMi { get; set; }

        public int? BagisSayisi { get; set; }

        public List<TeslimAlinacakBagisModel> BagisList { get; set; }
    }
}
