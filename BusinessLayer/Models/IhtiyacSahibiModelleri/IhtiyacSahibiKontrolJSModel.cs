using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Models.IhtiyacSahibiModelleri
{
    public class IhtiyacSahibiKontrolJSModel
    {
        public bool? BasariliMi { get; set; }

        public int? IhtiyacSahibiSayisi { get; set; }

        public List<IhtiyacSahibiKontrolModel> IhtiyacSahibiKontrolListe { get; set; }

        public IhtiyacSahibiKontrolJSModel()
        {
            IhtiyacSahibiKontrolListe = new List<IhtiyacSahibiKontrolModel>();
        }
    }
}
