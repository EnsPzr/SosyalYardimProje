using BusinessLayer.Models.OrtakModeller;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Models.DisardanIhtiyacSahibiModelleri
{
    public class DisardanIhtiyacSahibiModel
    {
        [Display(Name = "Kayıt Eden Ad")]
        [Required(ErrorMessage = "Ad alanı boş geçilemez")]
        [MinLength(3, ErrorMessage = "Ad minumum {1} karakter olabilir"), MaxLength(25, ErrorMessage = "Ad maksimum {1} karakter olabilir")]
        public String BagisciAdi { get; set; }

        [Display(Name = "Kayıt Eden Soyad")]
        [Required(ErrorMessage = "Soyad alanı boş geçilemez")]
        [MinLength(3, ErrorMessage = "Soyad minumum {1} karakter olabilir"), MaxLength(25, ErrorMessage = "Soyad maksimum {1} karakter olabilir")]
        public String BagisciSoyadi { get; set; }

        public SehirModel SehirBagisci { get; set; }

        [Display(Name = "Kayıt Eden Tel No")]
        [Required(ErrorMessage = "Tel No alanı boş geçilemez")]
        [MinLength(10, ErrorMessage = "Tel No minumum {1} karakter olabilir"), MaxLength(50, ErrorMessage = "Tel No maksimum {1} karakter olabilir")]
        [DataType(DataType.PhoneNumber, ErrorMessage = "Lütfen geçerli bir telefon numarası giriniz")]
        public String TelNo { get; set; }

        [Display(Name = "Kayıt Eden E Posta")]
        [Required(ErrorMessage = "E Posta alanı boş geçilemez")]
        [MinLength(6, ErrorMessage = "E Posta minumum {1} karakter olabilir"), MaxLength(35, ErrorMessage = "E Posta  maksimum {1} karakter olabilir")]
        [EmailAddress(ErrorMessage = "Lütfen geçerli bir e posta adresi giriniz")]
        public String BagisciEPosta { get; set; }

        [Display(Name = "Kayıt Eden Adres")]
        [MinLength(6, ErrorMessage = "Adres minumum {1} karakter olabilir"), MaxLength(35, ErrorMessage = "Adres  maksimum {1} karakter olabilir")]
        public String Adres { get; set; }

        [Display(Name = "Kayıt Eden Şifre")]
        [Required(ErrorMessage = "Şifre alanı boş geçilemez")]
        [MinLength(8, ErrorMessage = "Şifre minumum {1} karakter olabilir"), MaxLength(40, ErrorMessage = "Şifre  maksimum {1} karakter olabilir")]
        public String BagisciSifre { get; set; }

        [Display(Name = "Kayıt Eden Şifre Tekrar")]
        [Required(ErrorMessage = "Şifre tekrar alanı boş geçilemez")]
        [MinLength(8, ErrorMessage = "Şifre tekrar minumum {1} karakter olabilir"), MaxLength(40, ErrorMessage = "Şifre  maksimum {1} karakter olabilir")]
        [Compare("Sifre")]
        public String BagisciSifreTekrar { get; set; }


        [Display(Name = "İhtiyac Sahibi Adı")]
        [Required(ErrorMessage = "İhtiyac sahibi adı zorunludur.")]
        [MaxLength(30, ErrorMessage = "İhtiyac sahibi adı en fazla {1} karakter olabilir."), MinLength(3, ErrorMessage = "İhtiyac sahibi adı en az {1} karakter olabilir.")]
        public String IhtiyacSahibiAdi { get; set; }

        [Display(Name = "İhtiyac Sahibi Soyadı")]
        [Required(ErrorMessage = "İhtiyac sahibi Soyadı zorunludur.")]
        [MaxLength(30, ErrorMessage = "İhtiyac sahibi Soyadı en fazla {1} karakter olabilir."), MinLength(3, ErrorMessage = "İhtiyac sahibi Soyadı en az {1} karakter olabilir.")]
        public String IhtiyacSahibiSoyadi { get; set; }

        [Display(Name = "İhtiyac Sahibi Tel No")]
        [Required(ErrorMessage = "İhtiyac sahibi Tel No zorunludur.")]
        [MaxLength(50, ErrorMessage = "İhtiyac sahibi Tel No en fazla {1} karakter olabilir."), MinLength(6, ErrorMessage = "İhtiyac sahibi Tel No en az {1} karakter olabilir.")]
        [Phone(ErrorMessage = "Lütfen geçerli bir telefon numarası giriniz.")]
        public String IhtiyacSahibiTelNo { get; set; }

        [Display(Name = "İhtiyac Sahibi Adres")]
        [Required(ErrorMessage = "İhtiyac sahibi Adres zorunludur.")]
        [MaxLength(50, ErrorMessage = "İhtiyac sahibi Adres en fazla {1} karakter olabilir."), MinLength(6, ErrorMessage = "İhtiyac sahibi Adres en az {1} karakter olabilir.")]
        public String IhtiyacSahibiAdres { get; set; }

        [Display(Name = "İhtiyac Sahibi Açıklama")]
        [Required(ErrorMessage = "İhtiyac sahibi Açıklama zorunludur.")]
        [MaxLength(50, ErrorMessage = "İhtiyac sahibi Açıklama en fazla {1} karakter olabilir."), MinLength(6, ErrorMessage = "İhtiyac sahibi Açıklama en az {1} karakter olabilir.")]
        [DataType(DataType.MultilineText)]
        public String IhtiyacSahibiAciklama { get; set; }

        public SehirModel SehirIhtiyacSahibi { get; set; }
    }
}
