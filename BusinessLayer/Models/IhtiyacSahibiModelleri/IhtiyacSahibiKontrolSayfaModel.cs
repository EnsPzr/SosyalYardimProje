using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Models.IhtiyacSahibiModelleri
{
    public class IhtiyacSahibiKontrolSayfaModel
    {
        public int? IhtiyacSahibiKontrolId { get; set; }
        [Display(Name ="Ad Soyad")]
        public String IhtiyacSahibiAdiSoyadi { get; set; }

        [Display(Name = "Tel")]
        public String IhtiyacSahibiTel { get; set; }

        [Display(Name = "Adres")]
        public String IhtiyacSahibiAdres { get; set; }

        [Display(Name = "İl")]
        public String IhtiyacSahibiIl { get; set; }

        [Display(Name="Muhtaç Mı?")]
        public bool? MuhtacMi { get; set; }

        public List<IhtiyacSahibiVerileceklerModel> verileceklerList { get; set; }

        [Display(Name = "Tahmini Teslim")]
        public DateTime? TahminiTeslim { get; set; }

        [Display(Name ="Nakdi Bağış")]
        public Decimal? NakdiBagisMiktari { get; set; }
    }
}
