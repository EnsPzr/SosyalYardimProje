using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Models.MesajModelleri
{
    public class MesajDetayJsModel
    {
        public bool BasariliMi { get; set; }

        public int? MesajDetaySayisi { get; set; }

        public List<MesajDetayModel> MesajDetayList { get; set; }
    }
}
