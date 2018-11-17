using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class KullaniciYonetimi
    {
        private SosyalYardimDB db = new SosyalYardimDB();
        public KullaniciBilgileriTablo KullaniciBul(String KullaniciGuId)
        {
            var Kullanici = db.KullaniciBilgileriTablo.FirstOrDefault(p=>p.KullaniciGuId==KullaniciGuId);
            return Kullanici;
        }
    }
}
