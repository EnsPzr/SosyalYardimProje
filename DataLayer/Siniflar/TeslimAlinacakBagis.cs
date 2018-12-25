using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Runtime.InteropServices.WindowsRuntime;

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
                    .Include(p => p.BagisDetayTablo.Select(q => q.BagisDetayResimTablo)).OrderByDescending(p=>p.EklenmeTarihi).ToList();
            }
            else
            {
                int? sehirId = kullaniciDAL.KullaniciSehir(kullaniciId);
                return db.BagisTablo.Include(p => p.BagisDetayTablo).Include(p => p.KullaniciBilgileriTablo)
                    .Include(p => p.BagisDetayTablo.Select(q => q.BagisDetayResimTablo))
                    .Where(p => p.KullaniciBilgileriTablo.SehirTablo_SehirId == sehirId)
                    .OrderByDescending(p => p.EklenmeTarihi).ToList();
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

                return sorgu.OrderByDescending(p => p.EklenmeTarihi).ToList();
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
                return sorgu.OrderByDescending(p => p.EklenmeTarihi).ToList();
            }
        }

        public BagisTablo Detay(int? bagisId)
        {
            var bagis = db.BagisTablo.Include(p => p.KullaniciBilgileriTablo).Include(p => p.BagisDetayTablo)
                .Include(p => p.BagisDetayTablo.Select(q => q.BagisDetayResimTablo)).Where(p => p.BagisId == bagisId)
                .FirstOrDefault();
            if (bagis != null)
            {
                bagis.OnaylandiMi = true;
                db.SaveChanges();
            }
            return bagis;
        }
        public BagisTablo TeslimDetay(int? bagisId)
        {
            var bagis = db.BagisTablo.Include(p => p.KullaniciBilgileriTablo).Include(p => p.BagisDetayTablo)
                .Include(p => p.BagisDetayTablo.Select(q => q.BagisDetayResimTablo)).Where(p => p.BagisId == bagisId)
                .FirstOrDefault();
            return bagis;
        }

        public List<BagisDetayTablo> BagisDetay(int? bagisId)
        {
            var bagisDetay = db.BagisDetayTablo.Include(p => p.EsyaTablo
).Where(p => p.BagisTablo_BagisId == bagisId).ToList();
            return bagisDetay;
        }

        public List<BagisDetayResimTablo> BagisResim(int? bagisDetayId)
        {
            return db.BagisDetayResimTablo.Where(p => p.BagisDetayTablo_BagisDetayId == bagisDetayId).ToList();
        }

        public bool KullaniciBagisDetayiGorebilirMi(int? kullaniciId,int? bagisId)
        {
            if (kullaniciDAL.KullaniciMerkezdeMi(kullaniciId))
            {
                return true;
            }
            else
            {
                int? kulSehirId = kullaniciDAL.KullaniciSehir(kullaniciId);
                var bagis = db.BagisTablo.Include(p=>p.KullaniciBilgileriTablo).FirstOrDefault(p => p.BagisId==bagisId);
                if (bagis.KullaniciBilgileriTablo.SehirTablo_SehirId == kulSehirId)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }


        public bool TeslimBagisKaydet(BagisTablo bagisTablo,List<BagisDetayTablo> bagisDetayTablo)
        {
            var duzenlenecekBagis = db.BagisTablo.FirstOrDefault(p => p.BagisId == bagisTablo.BagisId);
            if (duzenlenecekBagis != null)
            {
                duzenlenecekBagis.OnaylandiMi = true;
                if (bagisTablo.TahminiTeslimAlmaTarihi != null)
                {
                    duzenlenecekBagis.TahminiTeslimAlmaTarihi = bagisTablo.TahminiTeslimAlmaTarihi;
                }
            }

            db.SaveChanges();
            int sayac = 0;
            int sayac2 = 0;
            for (int i = 0; i < bagisDetayTablo.Count; i++)
            {
                int? bagisDetayId = bagisDetayTablo[i].BagisDetayId;
                var duzenlenecekBagisDetay = db.BagisDetayTablo.FirstOrDefault(p => p.BagisDetayId== bagisDetayId);
                if (duzenlenecekBagisDetay != null)
                {
                    duzenlenecekBagisDetay.Adet = bagisDetayTablo[i].Adet;
                    duzenlenecekBagisDetay.AlinacakMi = bagisDetayTablo[i].AlinacakMi;
                    if (bagisDetayTablo[i].AlinacakMi == true)
                    {
                        sayac2++;
                    }
                    duzenlenecekBagisDetay.AlindiMi = bagisDetayTablo[i].AlindiMi;
                    if (bagisDetayTablo[i].AlindiMi == true)
                    {
                        sayac++;
                        duzenlenecekBagisDetay.AlinmaTarihi = DateTime.Now;
                    }
                }
            }

            db.SaveChanges();

            if (sayac2 == sayac)
            {
                duzenlenecekBagis.TeslimAlindiMi = true;
                db.SaveChanges();
            }

            return true;
        }
    }
}
