using BusinessLayer.Models.OrtakModeller;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Models.DisardanIhtiyacSahibiModelleri
{
    public class DisardanNakdiBagisModel
    {
        [Display(Name = "Bağış Yapan Ad")]
        [Required(ErrorMessage = "Ad alanı boş geçilemez")]
        [MinLength(3, ErrorMessage = "Ad minumum {1} karakter olabilir"), MaxLength(25, ErrorMessage = "Ad maksimum {1} karakter olabilir")]
        public String BagisciAdi { get; set; }

        [Display(Name = "Bağış Yapan Soyad")]
        [Required(ErrorMessage = "Soyad alanı boş geçilemez")]
        [MinLength(3, ErrorMessage = "Soyad minumum {1} karakter olabilir"), MaxLength(25, ErrorMessage = "Soyad maksimum {1} karakter olabilir")]
        public String BagisciSoyadi { get; set; }

        public SehirModel SehirBagisci { get; set; }

        [Display(Name = "Bağış Yapan Tel No")]
        [Required(ErrorMessage = "Tel No alanı boş geçilemez")]
        [MinLength(10, ErrorMessage = "Tel No minumum {1} karakter olabilir"), MaxLength(50, ErrorMessage = "Tel No maksimum {1} karakter olabilir")]
        [DataType(DataType.PhoneNumber, ErrorMessage = "Lütfen geçerli bir telefon numarası giriniz")]
        public String TelNo { get; set; }

        [Display(Name = "Bağış Yapan E Posta")]
        [Required(ErrorMessage = "E Posta alanı boş geçilemez")]
        [MinLength(6, ErrorMessage = "E Posta minumum {1} karakter olabilir"), MaxLength(35, ErrorMessage = "E Posta  maksimum {1} karakter olabilir")]
        [EmailAddress(ErrorMessage = "Lütfen geçerli bir e posta adresi giriniz")]
        public String BagisciEPosta { get; set; }

        [Display(Name = "Bağış Yapan Adres")]
        [MinLength(6, ErrorMessage = "Adres minumum {1} karakter olabilir"), MaxLength(35, ErrorMessage = "Adres  maksimum {1} karakter olabilir")]
        public String BagisciAdres { get; set; }

        [Display(Name = "Bağış Yapan Şifre")]
        [Required(ErrorMessage = "Şifre alanı boş geçilemez")]
        [MinLength(8, ErrorMessage = "Şifre minumum {1} karakter olabilir"), MaxLength(40, ErrorMessage = "Şifre  maksimum {1} karakter olabilir")]
        [DataType(DataType.Password)]
        public String BagisciSifre { get; set; }

        [Display(Name = "Bağış Yapan Şifre Tekrar")]
        [Required(ErrorMessage = "Şifre tekrar alanı boş geçilemez")]
        [MinLength(8, ErrorMessage = "Şifre tekrar minumum {1} karakter olabilir"), MaxLength(40, ErrorMessage = "Şifre  maksimum {1} karakter olabilir")]
        [Compare("BagisciSifre", ErrorMessage = "Şifreler aynı olmak zorundadır.")]
        [DataType(DataType.Password)]
        public String BagisciSifreTekrar { get; set; }

        [Display(Name = "Miktar")]
        [Required(ErrorMessage = "Miktar girilmelidir.")]
        public double Miktar { get; set; }

        [Required(ErrorMessage = "Kart numarası zorunludur.")]
        [MinLength(16, ErrorMessage = "Kart numarası {1} hane olmalıdır."), MaxLength(16, ErrorMessage = "Kart numarası {1} hane olmalıdır.")]
        public String KartNo { get; set; }

        [Required(ErrorMessage = "Kart üstündeki isim girilmelidir.")]
        [Display(Name = "Kart üstündeki isim")]
        public String KartUstuIsim { get; set; }

        [Required(ErrorMessage = "Güvenlik Kodu zorunludur.")]
        [Display(Name = "Güvenlik Kodu")]
        [MinLength(3, ErrorMessage = "Güvenlik kodu {1} haneli olmalıdır."), MaxLength(3, ErrorMessage = "Güvenlik kodu {1} haneli olmalıdır.")]
        public String GuvenlikKodu { get; set; }

        public SehirModel BagisSehir { get; set; }

        [Required(ErrorMessage = "Son Kullanma Ayı seçilmelidir.")]
        public int? Ay { get; set; }

        [Required(ErrorMessage = "Son kullanma Yılı seçilmelidir.")]
        public int? Yil { get; set; }
    }
}
