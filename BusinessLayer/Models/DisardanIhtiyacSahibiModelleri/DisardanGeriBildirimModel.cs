using BusinessLayer.Models.OrtakModeller;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Models.DisardanIhtiyacSahibiModelleri
{
    public class DisardanGeriBildirimModel
    {
        [Display(Name = "Geri Bildirim Gönderen Ad")]
        [Required(ErrorMessage = "Ad alanı boş geçilemez")]
        [MinLength(3, ErrorMessage = "Ad minumum {1} karakter olabilir"), MaxLength(25, ErrorMessage = "Ad maksimum {1} karakter olabilir")]
        public String BagisciAdi { get; set; }

        [Display(Name = "Geri Bildirim Gönderen Soyad")]
        [Required(ErrorMessage = "Soyad alanı boş geçilemez")]
        [MinLength(3, ErrorMessage = "Soyad minumum {1} karakter olabilir"), MaxLength(25, ErrorMessage = "Soyad maksimum {1} karakter olabilir")]
        public String BagisciSoyadi { get; set; }

        public SehirModel SehirBagisci { get; set; }

        [Display(Name = "Geri Bildirim Gönderen Tel No")]
        [Required(ErrorMessage = "Tel No alanı boş geçilemez")]
        [MinLength(10, ErrorMessage = "Tel No minumum {1} karakter olabilir"), MaxLength(50, ErrorMessage = "Tel No maksimum {1} karakter olabilir")]
        [DataType(DataType.PhoneNumber, ErrorMessage = "Lütfen geçerli bir telefon numarası giriniz")]
        public String TelNo { get; set; }

        [Display(Name = "Geri Bildirim Gönderen E Posta")]
        [Required(ErrorMessage = "E Posta alanı boş geçilemez")]
        [MinLength(6, ErrorMessage = "E Posta minumum {1} karakter olabilir"), MaxLength(35, ErrorMessage = "E Posta  maksimum {1} karakter olabilir")]
        [EmailAddress(ErrorMessage = "Lütfen geçerli bir e posta adresi giriniz")]
        public String BagisciEPosta { get; set; }

        [Display(Name = "Geri Bildirim Gönderen Adres")]
        [MinLength(6, ErrorMessage = "Adres minumum {1} karakter olabilir"), MaxLength(35, ErrorMessage = "Adres  maksimum {1} karakter olabilir")]
        public String BagisciAdres { get; set; }

        [Display(Name = "Geri Bildirim Gönderen Şifre")]
        [Required(ErrorMessage = "Şifre alanı boş geçilemez")]
        [MinLength(8, ErrorMessage = "Şifre minumum {1} karakter olabilir"), MaxLength(40, ErrorMessage = "Şifre  maksimum {1} karakter olabilir")]
        [DataType(DataType.Password)]
        public String BagisciSifre { get; set; }

        [Display(Name = "Geri Bildirim Gönderen Şifre Tekrar")]
        [Required(ErrorMessage = "Şifre tekrar alanı boş geçilemez")]
        [MinLength(8, ErrorMessage = "Şifre tekrar minumum {1} karakter olabilir"), MaxLength(40, ErrorMessage = "Şifre  maksimum {1} karakter olabilir")]
        [Compare("BagisciSifre", ErrorMessage = "Şifreler aynı olmak zorundadır.")]
        [DataType(DataType.Password)]
        public String BagisciSifreTekrar { get; set; }

        [MaxLength(50, ErrorMessage = "Konu en fazla {1} karakter olabilir"), MinLength(2, ErrorMessage = "Konu en az {1} karakter olabilir")]
        [Required(ErrorMessage = "Konu zorunludur.")]
        [Display(Name = "Konu")]
        public String Konu { get; set; }

        [MaxLength(500, ErrorMessage = "Mesaj en fazla {1} karakter olabilir"), MinLength(2, ErrorMessage = "Mesaj en az {1} karakter olabilir")]
        [Required(ErrorMessage = "Mesaj zorunludur.")]
        [Display(Name = "Mesaj")]
        [DataType(DataType.MultilineText)]
        public String Mesaj { get; set; }
    }
}
