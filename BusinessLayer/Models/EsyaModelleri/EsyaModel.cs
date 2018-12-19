using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Models.EsyaModelleri
{
    public class EsyaModel
    {
        [Key]
        public int? EsyaId { get; set; }

        [Required(ErrorMessage = "Eşya adı alanı zorunludur.")]
        [MinLength(3,ErrorMessage = "Eşya adı en az {1} karakter uzunluğunda olabilir."), MaxLength(30, ErrorMessage = "Eşya adı en fazla {1} karakter uzunluğunda olabilir.")]
        [Display(Name = "Eşya Adı")]
        public String EsyaAdi { get; set; }
    }
}
