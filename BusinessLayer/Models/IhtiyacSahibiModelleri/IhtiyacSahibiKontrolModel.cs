using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Models.IhtiyacSahibiModelleri
{
    public class IhtiyacSahibiKontrolModel
    {
        [Key]
        public int? IhtiyacSahibiKontrolId { get; set; }

        [Display(Name = "İhtiyaç Sahibi Adı Soyadı")]
        public String IhtiyacSahibiAdiSoyadi { get; set; }

        [Display(Name = "İhtiyaç Sahibi Tel No")]
        public String IhtiyacSahibiTelNo { get; set; }

        [Display(Name = "İhtiyaç Sahibi Adres")]
        public String IhtiyacSahibiAdres { get; set; }

        [Display(Name = "Muhtaç Mı?")]
        public String MuhtacMi { get; set; }

        [Display(Name = "Kontrol Sırası Eklenme Tarihi")]
        public DateTime? EklenmeTarih { get; set; }

        [Display(Name = "Kontrol Gerçekleşme Tarihi")]
        public DateTime? KontrolTarih { get; set; }

        [Display(Name = "Teslim Edilecek Eşyalar için Son Tarih")]
        public DateTime? TahminiTeslimTarihi { get; set; }

        [Display(Name = "Teslim Durumu")]
        public String TeslimTamamlandiMi { get; set; }
    }
}
