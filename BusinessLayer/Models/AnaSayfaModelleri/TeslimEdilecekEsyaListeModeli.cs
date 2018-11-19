using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Models.AnaSayfaModelleri
{
    public class TeslimEdilecekEsyaListeModeli
    {
        [Display(Name = "Muhtaç Adı Soyadı")]
        public String MuhtacAdiSoyadi { get; set; }


        [Display(Name = "Tahmini Teslim Tarihi")]
        public DateTime? TahminiTeslimTarihi { get; set; }
    }
}
