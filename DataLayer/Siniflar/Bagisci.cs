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
                return db.KullaniciBilgileriTablo.Include(p => p.SehirTablo).Where(p => p.BagisciMi == true).ToList();
            }
            else
            {
                int? sehirId = kullaniciDAL.KullaniciSehir(KullaniciId);
                return db.KullaniciBilgileriTablo.Include(p => p.SehirTablo).Where(p => p.BagisciMi == true && p.SehirTablo_SehirId == sehirId)
                    .ToList();
            }
        }

        public List<KullaniciBilgileriTablo> FiltreliBagiscilariGetir(int? KullaniciId, int? SehirId, String aranan)
        {
            if (kullaniciDAL.KullaniciMerkezdeMi(KullaniciId))
            {
                var dondurulecek = db.KullaniciBilgileriTablo.Include(p => p.SehirTablo).Where(p => p.BagisciMi == true).AsQueryable();
                if (SehirId != null)
                {
                    dondurulecek = dondurulecek.Where(p => p.SehirTablo_SehirId == SehirId);
                }

                if (!(aranan.Equals("")))
                {
                    dondurulecek = dondurulecek.Where(p => p.KullaniciAdi.Contains(aranan)
                                                           || p.KullaniciSoyadi.Contains(aranan)
                                                           || p.SehirTablo.SehirAdi.Contains(aranan)
                                                           || p.KullaniciEPosta.Contains(aranan)
                                                           || p.KullaniciAdres.Contains(aranan));
                }

                return dondurulecek.ToList();
            }
            else
            {
                int? sehirId = kullaniciDAL.KullaniciSehir(KullaniciId);
                var dondurulecek = db.KullaniciBilgileriTablo.Include(p => p.SehirTablo).Where(p => p.SehirTablo_SehirId == sehirId && p.BagisciMi == true).AsQueryable();
                if (!(aranan.Equals("")))
                {
                    dondurulecek = dondurulecek.Where(p => p.KullaniciAdi.Contains(aranan)
                                                           || p.KullaniciSoyadi.Contains(aranan)
                                                           || p.SehirTablo.SehirAdi.Contains(aranan)
                                                           || p.KullaniciEPosta.Contains(aranan)
                                                           || p.KullaniciAdres.Contains(aranan));
                }

                return dondurulecek.ToList();
            }
        }
    }
}
