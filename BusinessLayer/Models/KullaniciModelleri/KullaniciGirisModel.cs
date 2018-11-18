using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Models.KullaniciModelleri
{
    public class KullaniciGirisModel
    {
        [Display(Name = "E Posta")]
        [Required(ErrorMessage = "E Posta boş bırakılamaz")]
        [EmailAddress(ErrorMessage = "Lütfen geçerli bir E Posta adresi giriniz")]
        [MinLength(6,ErrorMessage = "E Posta en az {1} karakter olmalıdır"), MaxLength(50,ErrorMessage = "E Posta en fazla {1} karakter olmalıdır")]
        public String EPosta { get; set; }

        [Display(Name = "Şifre")]
        [Required(ErrorMessage = "Şifre boş bırakılamaz")]
        [DataType(DataType.Password)]
        [MinLength(6, ErrorMessage = "Şifre en az {1} karakter olmalıdır"), MaxLength(50, ErrorMessage = "Şifre en fazla {1} karakter olmalıdır")]
        public String Sifre { get; set; }
    }
}
