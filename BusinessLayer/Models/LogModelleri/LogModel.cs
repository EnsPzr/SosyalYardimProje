using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Models.LogModelleri
{
    public class LogModel
    {
        [Display(Name = "Kullanıcı")]
        public String KullaniciAdiSoyadi { get; set; }

        public int? KullaniciId { get; set; }

        [Display(Name = "İşlem Tiği")]
        public String IslemTipiStr  { get; set; }

        public int? IslemTipi { get; set; }

        [Display(Name = "İşlem İçerik")]
        public String IslemIcerik { get; set; }

        [Display(Name = "Tarih")]
        public DateTime? Tarih { get; set; }
    }
}
