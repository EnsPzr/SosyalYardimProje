using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Models.OrtakModeller
{
    public class SehirModel
    {
        [Key]
        [Required(ErrorMessage = "Şehir seçilmek zorundadır")]
        public int? SehirId { get; set; }

        [Display(Name = "Şehir")]
        public String SehirAdi { get; set; }
    }
}
