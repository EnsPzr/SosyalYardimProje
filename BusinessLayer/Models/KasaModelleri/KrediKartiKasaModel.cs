using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer.Models.OrtakModeller;

namespace BusinessLayer.Models.KasaModelleri
{
    public class KrediKartiKasaModel
    {
        [Display(Name = "Ad")]
        [MaxLength(25, ErrorMessage = "Ad en fazla {1} karakter olabilir."), MinLength(3, ErrorMessage = "Ad en az {1} karakter olabilir.")]
        [Required(ErrorMessage = "Ad boş geçilemez.")]
        public String BagisciAdi { get; set; }

        [Display(Name = "Soyad")]
        [MaxLength(25, ErrorMessage = "Soyad en fazla {1} karakter olabilir."), MinLength(3, ErrorMessage = "Soyad en az {1} karakter olabilir.")]
        [Required(ErrorMessage = "Soyad boş geçilemez.")]
        public String BagisciSoyadi { get; set; }

        [DataType(DataType.PhoneNumber, ErrorMessage = "Lütfen geçerli bir telefon numarası giriniz.")]
        [Display(Name = "Tel No")]
        [MaxLength(50, ErrorMessage = "Tel No en fazla {1} karakter olabilir."), MinLength(6, ErrorMessage = "Tel No en az {1} karakter olabilir.")]
        [Required(ErrorMessage = "Tel No boş geçilemez.")]
        public String BagisciTelNo { get; set; }

        [Display(Name = "E Posta")]
        [EmailAddress(ErrorMessage = "Lütfen geçerli bir E Posta adresi giriniz.")]
        [MaxLength(35, ErrorMessage = "E Posta en fazla {1} karakter olabilir."), MinLength(6, ErrorMessage = "E Posta en az {1} karakter olabilir.")]
        [Required(ErrorMessage = "E Posta boş geçilemez.")]
        public String BagisciEPosta { get; set; }

        [Display(Name = "Miktar")]
        [Required(ErrorMessage = "Miktar girilmelidir.")]
        public double Miktar { get; set; }

        [DataType(DataType.CreditCard,ErrorMessage = "Lütfen geçerli bir kart bilgisi giriniz.")]
        [Required(ErrorMessage = "Kart numarası zorunludur.")]
        public String KartNo { get; set; }

        [Required(ErrorMessage = "Kart üstündeki isim girilmelidir.")]
        [Display(Name = "Kart üstündeki isim")]
        public String KartUstuIsim { get; set; }

        [Required(ErrorMessage = "Güvenlik Kodu zorunludur.")]
        [Display(Name = "Güvenlik Kodu")]
        [MinLength(3,ErrorMessage = "Güvenlik kodu {1} haneli olmalıdır."), MaxLength(3, ErrorMessage = "Güvenlik kodu {1} haneli olmalıdır.")]
        public String GuvenlikKodu { get; set; }

        public SehirModel Sehir { get; set; }

        [Required(ErrorMessage = "Son Kullanma Ayı seçilmelidir.")]
        public int? Ay { get; set; }

        [Required(ErrorMessage = "Son kullanma Yılı seçilmelidir.")]
        public int? Yil { get; set; }


    }
}
