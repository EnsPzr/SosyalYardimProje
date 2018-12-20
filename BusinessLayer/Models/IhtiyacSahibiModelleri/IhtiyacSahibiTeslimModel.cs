using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Models.IhtiyacSahibiModelleri
{
    public class IhtiyacSahibiTeslimModel
    {
        public int? IhtiyacSahibiKontrolId { get; set; }
        [Display(Name = "Ad Soyad")]
        public String IhtiyacSahibiAdiSoyadi { get; set; }

        [Display(Name = "Tel")]
        public String IhtiyacSahibiTel { get; set; }

        [Display(Name = "Adres")]
        public String IhtiyacSahibiAdres { get; set; }

        [Display(Name = "İl")]
        public String IhtiyacSahibiIl { get; set; }

        public List<IhtiyacSahibiTeslimEdilecekEsyaModel> ihtiyacSahibiTeslimEdilecekEsyaList { get; set; }

        [Display(Name = "Nakdi Bağış")]
        public String MaddiBagis { get; set; }

        [Display(Name = "Nakdi Bağış Yapıldı Mı?")]
        public bool? MaddiBagisYapildiMi { get; set; }
        public IhtiyacSahibiTeslimModel()
        {
            ihtiyacSahibiTeslimEdilecekEsyaList=new List<IhtiyacSahibiTeslimEdilecekEsyaModel>();
        }
    }
}
