using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Models.IhtiyacSahibiModelleri
{
    public class IhtiyacSahibiJSModel
    {
        public bool? BasariliMi { get; set; }

        public int? IhtiyacSahibiSayisi { get; set; }

        public List<IhtiyacSahibiModel> IhtiyacSahipleri { get; set; }

        public IhtiyacSahibiJSModel()
        {
            IhtiyacSahipleri=new List<IhtiyacSahibiModel>();
        }
    }
}
