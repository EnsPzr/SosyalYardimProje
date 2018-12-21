using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
namespace DataLayer.Siniflar
{
    public class Bagisci
    {
        private KullaniciYonetimi kullaniciDAL = new KullaniciYonetimi();
        private SosyalYardimDB db = new SosyalYardimDB();
        public List<KullaniciBilgileriTablo> TumBagiscilariGetir(int? KullaniciId)
        {
            if (kullaniciDAL.KullaniciMerkezdeMi(KullaniciId))
            {
                return db.KullaniciBilgileriTablo.Include(p=>p.SehirTablo).Where(p => p.BagisciMi == true).ToList();
            }
            else
            {
                int? sehirId = kullaniciDAL.KullaniciSehir(KullaniciId);
                return db.KullaniciBilgileriTablo.Include(p => p.SehirTablo).Where(p => p.BagisciMi == true && p.SehirTablo_SehirId == sehirId)
                    .ToList();
            }
        }
    }
}
