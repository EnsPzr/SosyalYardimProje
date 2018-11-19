using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Models.AnaSayfaModelleri
{
    public class OnayBekleyenIhtiyacSahipleriModeli
    {
        [Display(Name = "Muhtaç Adı Soyadı")]
        public String MuhtacAdiSoyadi { get; set; }

        [Display(Name = "Eklenme Tarihi")]
        public DateTime? Tarih { get; set; }
    }
}
