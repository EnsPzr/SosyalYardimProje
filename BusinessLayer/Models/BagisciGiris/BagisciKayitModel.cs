using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Models.BagisciGiris
{
    public class BagisciKayitModel
    {
        [Display(Name = "Ad")]
        [MinLength(6,ErrorMessage = "Ad en az {1} karakter olabilir"), MaxLength(25, ErrorMessage = "Ad en fazla {1} karakter olabilir")]
        [Required(ErrorMessage = "Adınızı giriniz")]
        public String BagisciAdi { get; set; }

        [Display(Name = "Soyad")]
        [MinLength(6, ErrorMessage = "Soyad en az {1} karakter olabilir"), MaxLength(25, ErrorMessage = "Soyad en fazla {1} karakter olabilir")]
        [Required(ErrorMessage = "Soyadınızı giriniz")]
        public String BagisciSoyadi { get; set; }

        [Display(Name = "Tel No")]
        [MinLength(6, ErrorMessage = "Tel No en az {1} karakter olabilir"), MaxLength(50, ErrorMessage = "Tel No en fazla {1} karakter olabilir")]
        [Required(ErrorMessage = "Tel No giriniz")]
        public String BagisciTelNo { get; set; }

        [Display(Name = "E Posta")]
        [MinLength(6, ErrorMessage = "E Posta en az {1} karakter olabilir"), MaxLength(35, ErrorMessage = "E Posta en fazla {1} karakter olabilir")]
        [Required(ErrorMessage = "E Posta giriniz")]
        [EmailAddress(ErrorMessage = "Lütfen geçerli bir E Posta adresi giriniz")]
        public String BagisciEPosta { get; set; }

        [Display(Name = "Şifre")]
        [MinLength(8, ErrorMessage = "Şifre en az {1} karakter olabilir"), MaxLength(40, ErrorMessage = "Şifre en fazla {1} karakter olabilir")]
        [Required(ErrorMessage = "Şifre giriniz")]
        public String BagisciSifre { get; set; }

        [Display(Name = "Şifre Tekrar")]
        [MinLength(8, ErrorMessage = "Şifre Tekrar en az {1} karakter olabilir"), MaxLength(40, ErrorMessage = "Şifre Tekrar en fazla {1} karakter olabilir")]
        [Required(ErrorMessage = "Şifre Tekrar giriniz")]
        [Compare("BagisciSifre")]
        public String BagisciSifreTekrar { get; set; }

        [Display(Name = "Adres")]
        [MinLength(8, ErrorMessage = "Adres en az {1} karakter olabilir"), MaxLength(150, ErrorMessage = "Adres en fazla {1} karakter olabilir")]
        [Required(ErrorMessage = "Adres Tekrar giriniz")]
        [DataType(DataType.MultilineText)]
        public String BagisciAdres { get; set; }

        [Display(Name = "Şehir")]
        [Required(ErrorMessage = "Şehir seçiniz")]
        public int? SehirId { get; set; }
    }
}
