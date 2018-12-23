using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Models.BagisciGiris
{
    public class BagisciGirisModel
    {
        [Required(ErrorMessage = "E Posta adresi zorunludur.")]
        [Display(Name = "E Posta Adresi")]
        [MinLength(5, ErrorMessage = "E Posta en az {1} karakter olabilir."), MaxLength(30, ErrorMessage = "E Posta en fazla {1} karakter olabilir.")]
        [EmailAddress(ErrorMessage = "Lütfen geçerli bir e posta adresi giriniz.")]
        public String Eposta { get; set; }

        [Required(ErrorMessage = "Şifre adresi zorunludur.")]
        [Display(Name = "Şifre")]
        [MinLength(6, ErrorMessage = "Şifre en az {1} karakter olabilir."), MaxLength(30, ErrorMessage = "Şifre en fazla {1} karakter olabilir.")]
        [DataType(DataType.Password)]
        public String Sifre { get; set; }
    }
}
