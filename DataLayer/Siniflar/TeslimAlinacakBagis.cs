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

        public List<BagisTablo> FiltreliBagislariGetir(int? kullaniciId, int? sehirId, String aranan, String tarih)
        {
            if (kullaniciDAL.KullaniciMerkezdeMi(kullaniciId))
            {
                var sorgu = db.BagisTablo.Include(p => p.BagisDetayTablo).Include(p => p.KullaniciBilgileriTablo)
                    .Include(p => p.BagisDetayTablo.Select(q => q.BagisDetayResimTablo)).AsQueryable();
                if (sehirId != null)
                {
                    sorgu = sorgu.Where(p => p.KullaniciBilgileriTablo.SehirTablo_SehirId == sehirId);
                }

                if (aranan != null)
                {
                    sorgu = sorgu.Where(p => p.KullaniciBilgileriTablo.KullaniciAdi.Contains(aranan)
                                             || p.KullaniciBilgileriTablo.KullaniciSoyadi.Contains(aranan)
                                             || p.KullaniciBilgileriTablo.KullaniciAdres.Contains(aranan)
                                             || p.KullaniciBilgileriTablo.KullaniciEPosta.Contains(aranan)
                                             || p.KullaniciBilgileriTablo.KullaniciTelefonNumarasi.Contains(aranan));
                }

                if (tarih != null)
                {
                    DateTime? tarihDate = Convert.ToDateTime(tarih);
                    sorgu = sorgu.Where(p => p.EklenmeTarihi == tarihDate
                                             || p.TahminiTeslimAlmaTarihi == tarihDate);
                }

                return sorgu.ToList();
            }
            else
            {
                int? SehirId = kullaniciDAL.KullaniciSehir(kullaniciId);
                var sorgu = db.BagisTablo.Include(p => p.BagisDetayTablo).Include(p => p.KullaniciBilgileriTablo)
                    .Include(p => p.BagisDetayTablo.Select(q => q.BagisDetayResimTablo)).Where(p => p.KullaniciBilgileriTablo.SehirTablo_SehirId == sehirId).AsQueryable();
                if (aranan != null)
                {
                    sorgu = sorgu.Where(p => p.KullaniciBilgileriTablo.KullaniciAdi.Contains(aranan)
                                             || p.KullaniciBilgileriTablo.KullaniciSoyadi.Contains(aranan)
                                             || p.KullaniciBilgileriTablo.KullaniciAdres.Contains(aranan)
                                             || p.KullaniciBilgileriTablo.KullaniciEPosta.Contains(aranan)
                                             || p.KullaniciBilgileriTablo.KullaniciTelefonNumarasi.Contains(aranan));
                }

                if (tarih != null)
                {
                    DateTime? tarihDate = Convert.ToDateTime(tarih);
                    sorgu = sorgu.Where(p => p.EklenmeTarihi == tarihDate
                                             || p.TahminiTeslimAlmaTarihi == tarihDate);
                }
                return sorgu.ToList();
            }
        }
    }
}
