using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer.Models.OrtakModeller;

namespace BusinessLayer.Models.KullaniciModelleri
{
    public class KullaniciModel
    {
        [Key]
        [Required]
        public int? KullaniciId { get; set; }

        [Display(Name = "Ad")]
        [MaxLength(25,ErrorMessage = "Ad en fazla {1} karakter olabilir."),MinLength(3,ErrorMessage = "Ad en az {1} karakter olabilir.")]
        [Required(ErrorMessage = "Ad boş geçilemez.")]
        public String KullaniciAdi { get; set; }

        [Display(Name = "Soyad")]
        [MaxLength(25, ErrorMessage = "Soyad en fazla {1} karakter olabilir."), MinLength(3, ErrorMessage = "Soyad en az {1} karakter olabilir.")]
        [Required(ErrorMessage = "Soyad boş geçilemez.")]
        public String KullaniciSoyadi { get; set; }

        public SehirModel Sehir { get; set; }

        [Display(Name = "Onaylı")]
        public bool? KullaniciOnayliMi { get; set; }

        [Display(Name = "Telegram Kullanıcı Adı")]
        [MaxLength(20, ErrorMessage = "Soyad en fazla {1} karakter olabilir."), MinLength(3, ErrorMessage = "Soyad en az {1} karakter olabilir.")]
        public String KullaniciTelegramKullaniciAdi { get; set; }

        [Display(Name = "TC Kimlik No")]
        [MaxLength(11, ErrorMessage = "TC {1} karakter olabilir."),MinLength(11, ErrorMessage = "TC {1} karakter olabilir.")]
        [Required(ErrorMessage = "TC Kimlik No boş geçilemez.")]
        public String KullaniciTCKimlik { get; set; }

        [DataType(DataType.PhoneNumber,ErrorMessage = "Lütfen geçerli bir telefon numarası giriniz.")]
        [Display(Name = "Tel No")]
        [MaxLength(50, ErrorMessage = "Tel No en fazla {1} karakter olabilir."),MinLength(6, ErrorMessage = "Tel No en az {1} karakter olabilir.")]
        [Required(ErrorMessage = "Tel No boş geçilemez.")]
        public String KullaniciTelNo { get; set; }

        [Display(Name = "Merkezde")]
        public bool? KullaniciMerkezdeMi { get; set; }

        [Display(Name = "E Posta")]
        [EmailAddress(ErrorMessage = "Lütfen geçerli bir E Posta adresi giriniz.")]
        [MaxLength(35, ErrorMessage = "E Posta en fazla {1} karakter olabilir."), MinLength(6, ErrorMessage = "E Posta en az {1} karakter olabilir.")]
        [Required(ErrorMessage = "E Posta boş geçilemez.")]
        public String KullaniciEPosta { get; set; }

        [Display(Name = "Şifre")]
        [MaxLength(20, ErrorMessage = "Şifre en fazla {1} karakter olabilir."), MinLength(6, ErrorMessage = "Şifre en az {1} karakter olabilir.")]
        //[Required(ErrorMessage = "Şifre boş geçilemez.")]
        public String KullaniciSifre { get; set; }

        [Display(Name = "Şifre Tekrar")]
        [MaxLength(20, ErrorMessage = "Şifre Tekrar en fazla {1} karakter olabilir."), MinLength(6, ErrorMessage = "Şifre Tekrar en az {1} karakter olabilir.")]
        //[Required(ErrorMessage = "Şifre Tekrar boş geçilemez.")]
        [Compare("KullaniciSifre")]
        public String KullaniciSifreTekrar { get; set; }

        [Display(Name = "Aktif Mi?")]
        public bool? AktifMi { get; set; }

        public int? Sira { get; set; }
        public String AktifMiStr { get; set; }
        public String KullaniciOnayliMiStr { get; set; }
        public String KullaniciMerkezdeMiStr { get; set; }
        public KullaniciModel()
        {
            Sehir= new SehirModel();
            this.KullaniciSifre = "123456";
            this.KullaniciSifreTekrar = "123456";
        }
    }
}
