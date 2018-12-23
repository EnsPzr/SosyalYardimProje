using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Models.MesajModelleri
{
    public class MesajModel
    {
        [Key]
        public int? MesajId { get; set; }

        public int? KullaniciId { get; set; }

        [Display(Name = "Gönderen")]
        public String KullaniciAdiSoyadi { get; set; }

        [Required(ErrorMessage = "Alıcı seçilmek zorundadır")]
        public int? AliciInt { get; set; }

        [Display(Name = "Alıcı")]
        public String AliciStr { get; set; }


    }
}
