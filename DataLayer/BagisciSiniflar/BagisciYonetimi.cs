using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.BagisciSiniflar
{
    public class BagisciYonetimi
    {
        private SosyalYardimDB db = new SosyalYardimDB();

        public KullaniciBilgileriTablo BagisciBul(String ePosta, String sifre)
        {
            var bagisci = db.KullaniciBilgileriTablo.FirstOrDefault(p =>
                p.BagisciMi == true && p.KullaniciEPosta == ePosta
                                    && p.KullaniciSifre == sifre);
            if (bagisci != null)
            {
                return bagisci;
            }
            else
            {
                return null;
            }
        }

        public KullaniciBilgileriTablo KullaniciBul(int? id)
        {
            return db.KullaniciBilgileriTablo.FirstOrDefault(p => p.KullaniciId == id);
        }
    }
}
