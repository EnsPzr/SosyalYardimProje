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

        [DataType(DataType.Date)]
        [Display(Name = "Gönderme Tarihi")]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? Tarih { get; set; }
    }
}
