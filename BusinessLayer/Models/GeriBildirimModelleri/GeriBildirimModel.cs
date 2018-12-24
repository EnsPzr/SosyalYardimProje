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


        public int? KullaniciId { get; set; }

        [Display(Name = "Ad Soyad")]
        public String KullaniciAdiSoyadi { get; set; }

        [Display(Name = "Tel No")]
        public String KullaniciTel { get; set; }

        [MaxLength(50, ErrorMessage = "Konu en fazla {1} karakter olabilir"), MinLength(2, ErrorMessage = "Konu en az {1} karakter olabilir")]
        [Required(ErrorMessage = "Konu zorunludur.")]
        [Display(Name = "Konu")]
        public String Konu { get; set; }

        [MaxLength(50, ErrorMessage = "Mesaj en fazla {1} karakter olabilir"), MinLength(2, ErrorMessage = "Mesaj en az {1} karakter olabilir")]
        [Required(ErrorMessage = "Mesaj zorunludur.")]
        [Display(Name = "Mesaj")]
        [DataType(DataType.MultilineText)]
        public String Mesaj { get; set; }

        [Display(Name = "Geri Bil. Durumu")]
        public String DurumStr { get; set; }

        [Display(Name = "Geri Bil. Durumu")]
        [Required(ErrorMessage = "Geri Bildirim Durumu seçilmek zorundadır.")]
        public int? DurumInt { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Eklenme Tarihi")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? Tarih { get; set; }

        public String TarihStr { get; set; }
    }
}
