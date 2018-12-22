using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
namespace DataLayer.Siniflar
{
    public class GeriBildirim
    {
        private SosyalYardimDB db = new SosyalYardimDB();
        private KullaniciYonetimi kullaniciDAL = new KullaniciYonetimi();

        public List<GeriBildirimTablo> TumGeriBildirimleriGetir(int? kullaniciId)
        {
            if (kullaniciDAL.KullaniciMerkezdeMi(kullaniciId))
            {
                return db.GeriBildirimTablo.Include(p => p.KullaniciBilgileriTablo).ToList();
            }
            else
            {
                int? kullaniciSehirId = kullaniciDAL.KullaniciSehir(kullaniciId);
                return db.GeriBildirimTablo.Include(p => p.KullaniciBilgileriTablo)
                    .Include(p => p.KullaniciBilgileriTablo.SehirTablo).Where(p => p.KullaniciBilgileriTablo.SehirTablo_SehirId == kullaniciSehirId).ToList();
            }
        }
    }
}
