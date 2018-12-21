using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer.Models.OrtakModeller;

namespace BusinessLayer.Siniflar
{
    public class Bagisci
    {
        public int? BagisciId { get; set; }

        [Display(Name = "Ad")]
        [Required(ErrorMessage = "Ad alanı boş geçilemez")]
        [MinLength(3,ErrorMessage = "Ad minumum {1} karakter olabilir"),MaxLength(25,ErrorMessage = "Ad maksimum {1} karakter olabilir")]
        public String BagisciAdi { get; set; }

        [Display(Name = "Soyad")]
        [Required(ErrorMessage = "Soyad alanı boş geçilemez")]
        [MinLength(3, ErrorMessage = "Soyad minumum {1} karakter olabilir"), MaxLength(25, ErrorMessage = "Soyad maksimum {1} karakter olabilir")]
        public String BagisciSoyadi { get; set; }

        public SehirModel Sehir { get; set; }

        [Display(Name = "Tel No")]
        [Required(ErrorMessage = "Tel No alanı boş geçilemez")]
        [MinLength(10, ErrorMessage = "Tel No minumum {1} karakter olabilir"), MaxLength(50, ErrorMessage = "Tel No maksimum {1} karakter olabilir")]
        [DataType(DataType.PhoneNumber,ErrorMessage = "Lütfen geçerli bir telefon numarası giriniz")]
        public String TelNo { get; set; }

        [Display(Name = "E Posta")]
        [Required(ErrorMessage = "E Posta alanı boş geçilemez")]
        [MinLength(6, ErrorMessage = "E Posta minumum {1} karakter olabilir"), MaxLength(35, ErrorMessage = "E Posta  maksimum {1} karakter olabilir")]
        [EmailAddress(ErrorMessage = "Lütfen geçerli bir e posta adresi giriniz")]
        public String BagisciEPosta { get; set; }

        [Display(Name = "Şifre")]
        public String BagisciSifre { get; set; }
    }
}
