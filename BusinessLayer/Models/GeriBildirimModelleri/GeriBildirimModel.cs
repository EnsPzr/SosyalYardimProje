using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Models
{
    public class GeriBildirimModel
    {
        [Key]
        public int? GeriBildirimId { get; set; }
        
        [Display(Name = "Ad Soyad")]
        public String KullaniciAdiSoyadi { get; set; }

        [Display(Name = "Tel No")]
        public String KullaniciTel { get; set; }

        [Display(Name = "Konu")]
        public String Konu { get; set; }

        [Display(Name = "Mesaj")]
        [DataType(DataType.MultilineText)]
        public String Mesaj { get; set; }

        [Display(Name = "Geri Bil. Durumu")]
        public String DurumStr { get; set; }

        [Display(Name = "Geri Bil. Durumu")]
        [Required(ErrorMessage = "Geri Bildirim Durumu seçilmek zorundadır.")]
        public int? DurumInt { get; set; }

        [Display(Name = "Eklenme Tarihi")]
        public DateTime? Tarih { get; set; }
    }
}
