using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
namespace DataLayer.Siniflar
{
    public class TeslimAlinacakBagis
    {
        private SosyalYardimDB db = new SosyalYardimDB();
        private KullaniciYonetimi kullaniciDAL = new KullaniciYonetimi();
        public List<BagisTablo> TumBagislariGetir(int? kullaniciId)
        {
            if (kullaniciDAL.KullaniciMerkezdeMi(kullaniciId))
            {
                return db.BagisTablo.Include(p => p.BagisDetayTablo).Include(p => p.KullaniciBilgileriTablo)
                    .Include(p => p.BagisDetayTablo.Select(q => q.BagisDetayResimTablo)).ToList();
            }
            else
            {
                int? sehirId = kullaniciDAL.KullaniciSehir(kullaniciId);
                return db.BagisTablo.Include(p => p.BagisDetayTablo).Include(p => p.KullaniciBilgileriTablo)
                    .Include(p => p.BagisDetayTablo.Select(q => q.BagisDetayResimTablo)).Where(p => p.KullaniciBilgileriTablo.SehirTablo_SehirId == sehirId).ToList();
            }
        }
    }
}
