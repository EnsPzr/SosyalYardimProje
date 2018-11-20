using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Models.KullaniciModelleri
{
    public class KullaniciJSModel
    {
        public bool BasariliMi { get; set; }

        public List<KullaniciModel> KullaniciModelList { get; set; }
    }
}
