using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Models.LogModelleri
{
    public class LogModel
    {
        public String KullaniciAdiSoyadi { get; set; }

        public int? KullaniciId { get; set; }

        public String IslemTipiStr  { get; set; }

        public int? IslemTipi { get; set; }

        public String IslemIcerik { get; set; }

        public DateTime? Tarih { get; set; }
    }
}
