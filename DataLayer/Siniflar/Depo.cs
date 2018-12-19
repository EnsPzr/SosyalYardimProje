using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
namespace DataLayer.Siniflar
{
    public class Depo
    {
        private KullaniciYonetimi kullaniciDAL = new KullaniciYonetimi();
        private SosyalYardimDB db = new SosyalYardimDB();
        public List<DepoTablo> DepoGetir(int? KullaniciId)
        {
            if (kullaniciDAL.KullaniciMerkezdeMi(KullaniciId))
            {
                return db.DepoTablo.Include(p => p.SehirTablo).Include(p => p.EsyaTablo).ToList();
            }
            else
            {
                int? KullaniciSehirId = kullaniciDAL.KullaniciSehir(KullaniciId);
                return db.DepoTablo.Include(p => p.SehirTablo).Include(p => p.EsyaTablo).Where(p => p.SehirTablo_SehirId == KullaniciSehirId).ToList();
            }
        }
    }
}
