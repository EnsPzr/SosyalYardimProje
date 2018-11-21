using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Models.SubeModelleri
{
    public class SubeJSModel
    {
        public bool BasariliMi { get; set; }

        public int? SubeSayisi { get; set; }

        public List<SubeModel> SubeModelList { get; set; }
    }
}
