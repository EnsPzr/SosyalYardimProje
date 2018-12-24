using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
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

        public List<BagisTablo> FiltreliBagislariGetir(int? kullaniciId, string tarih)
        {
            var sorgu= db.BagisTablo.Where(p => p.KullaniciBilgileriTablo_KullaniciId == kullaniciId).AsQueryable();

            if (tarih != null)
            {
                DateTime trh = Convert.ToDateTime(tarih);
                sorgu = sorgu.Where(p => p.TahminiTeslimAlmaTarihi == trh||p.EklenmeTarihi==trh);
            }

            return sorgu.ToList();
        }
    }
}
