using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.BagisciSiniflar
{
    public class BagisciBagis
    {
        private KullaniciYonetimi kullaniciDAL =new KullaniciYonetimi();
        private SosyalYardimDB db = new SosyalYardimDB();

        public List<BagisTablo> TumBagislariGetir(int? kullaniciId)
        {
            return db.BagisTablo.Where(p => p.KullaniciBilgileriTablo_KullaniciId == kullaniciId).ToList();
        }
    }
}
