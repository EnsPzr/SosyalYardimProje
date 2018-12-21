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

        public List<KullaniciBilgileriTablo> FiltreliBagiscilariGetir(int? KullaniciId, int? SehirId, String aranan)
        {
            if (kullaniciDAL.KullaniciMerkezdeMi(KullaniciId))
            {
                return db.KullaniciBilgileriTablo.Include(p => p.SehirTablo).Where(p => (p.BagisciMi == true) &&
                                                                                        (p.SehirTablo_SehirId ==
                                                                                         SehirId && (
                                                                                             p.KullaniciAdi.Contains(
                                                                                                 aranan)
                                                                                             || p.KullaniciSoyadi
                                                                                                 .Contains(aranan)
                                                                                             || p.SehirTablo.SehirAdi
                                                                                                 .Contains(aranan)
                                                                                             ||p.KullaniciEPosta.Contains(aranan)
                                                                                             ||p.KullaniciAdres.Contains(aranan)))).ToList();
            }
            else
            {
                int? sehirId = kullaniciDAL.KullaniciSehir(KullaniciId);
                return db.KullaniciBilgileriTablo.Include(p => p.SehirTablo).Where(p => (p.BagisciMi == true && p.SehirTablo_SehirId == sehirId)&&
                                                                                        (p.KullaniciAdi.Contains(
                                                                                             aranan)
                                                                                         || p.KullaniciSoyadi
                                                                                             .Contains(aranan)
                                                                                         || p.SehirTablo.SehirAdi
                                                                                             .Contains(aranan)
                                                                                         || p.KullaniciEPosta.Contains(aranan)
                                                                                         || p.KullaniciAdres.Contains(aranan)))
                    .ToList();
            }
        }
    }
}
