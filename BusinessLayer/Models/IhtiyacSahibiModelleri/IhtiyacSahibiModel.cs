using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer.Models.OrtakModeller;

namespace BusinessLayer.Models.IhtiyacSahibiModelleri
{
    public class IhtiyacSahibiModel
    {
        [Key]
        public int? IhtiyacSahibiId { get; set; }

        [Display(Name = "İhtiyac Sahibi Adı")]
        [Required(ErrorMessage = "İhtiyac sahibi adı zorunludur.")]
        [MaxLength(30,ErrorMessage = "İhtiyac sahibi adı en fazla {1} karakter olabilir."),MinLength(3,ErrorMessage = "İhtiyac sahibi adı en az {1} karakter olabilir.")]
        public String IhtiyacSahibiAdi { get; set; }

        [Display(Name = "İhtiyac Sahibi Soyadı")]
        [Required(ErrorMessage = "İhtiyac sahibi Soyadı zorunludur.")]
        [MaxLength(30, ErrorMessage = "İhtiyac sahibi Soyadı en fazla {1} karakter olabilir."), MinLength(3, ErrorMessage = "İhtiyac sahibi Soyadı en az {1} karakter olabilir.")]
        public String IhtiyacSahibiSoyadi { get; set; }

        [Display(Name = "İhtiyac Sahibi Tel No")]
        [Required(ErrorMessage = "İhtiyac sahibi Tel No zorunludur.")]
        [MaxLength(50, ErrorMessage = "İhtiyac sahibi Tel No en fazla {1} karakter olabilir."), MinLength(6, ErrorMessage = "İhtiyac sahibi Tel No en az {1} karakter olabilir.")]
        [Phone(ErrorMessage ="Lütfen geçerli bir telefon numarası giriniz.")]
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

        public SehirModel Sehir { get; set; }
    }
}
