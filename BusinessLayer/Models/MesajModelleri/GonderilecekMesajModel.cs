using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Models.MesajModelleri
{
    public class GonderilecekMesajModel
    {
        public int? GonderenId { get; set; }

        [Display(Name = "Kime Gönderilecek")]
        [Required(ErrorMessage = "Gönderilecek grup seçilmek zorundadır.")]
        public int? KimeGonderilecek { get; set; }

        [Display(Name = "Şehir")]
        [Required(ErrorMessage = "Gönderilecek grup seçilmelidir.")]
        public int? SehirId { get; set; }

        [Display(Name = "Mesaj Metni")]
        [DataType(DataType.MultilineText)]
        [Required(ErrorMessage = "Gönderilecek Metin boş olamaz")]
        [MinLength(3,ErrorMessage = "Mesaj en az {1} karakter olabilir."),MaxLength(250, ErrorMessage = "Mesaj en fazla {1} olabilir.")]
        public String MesajMetni { get; set; }
    }
}
