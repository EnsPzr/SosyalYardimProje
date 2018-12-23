using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Models.MesajModelleri
{
    public class MesajJsModel
    {
        public bool BasariliMi { get; set; }

        public int? MesajSayisi { get; set; }

        public List<MesajModel> MesajList { get; set; }
    }
}
