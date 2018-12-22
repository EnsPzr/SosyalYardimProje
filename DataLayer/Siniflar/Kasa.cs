using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace DataLayer.Siniflar
{
    public class Kasa
    {
        private KullaniciYonetimi KullaniciDAL = new KullaniciYonetimi();
        private SosyalYardimDB db = new SosyalYardimDB();
        public List<KasaTablo> TumKasaGetir(int? kullaniciId)
        {
            if (KullaniciDAL.KullaniciMerkezdeMi(kullaniciId))
            {
                return db.KasaTablo.Include(p => p.KullaniciBilgileriTablo).Include(p => p.SehirTablo).ToList();
            }
            else
            {
                int? kullaniciSehirId = KullaniciDAL.KullaniciSehir(kullaniciId);
                return db.KasaTablo.Include(p => p.KullaniciBilgileriTablo).Include(p => p.SehirTablo).
                    Where(p => p.SehirTablo_SehirId == kullaniciSehirId).ToList();
            }
        }

        public List<KasaTablo> FiltreliKasaGetir(int? kullaniciId, string aranan, string tarih, int? sehirId, int? gelirGider)
        {
            if (KullaniciDAL.KullaniciMerkezdeMi(kullaniciId))
            {
                var sorgu = db.KasaTablo.Include(p => p.KullaniciBilgileriTablo).Include(p => p.SehirTablo)
                    .AsQueryable();
                if (aranan != null)
                {
                    sorgu = sorgu.Where(p => p.KullaniciBilgileriTablo.KullaniciAdi.Contains(aranan)
                                             || p.KullaniciBilgileriTablo.KullaniciSoyadi.Contains(aranan)
                                             || p.KullaniciBilgileriTablo.KullaniciEPosta.Contains(aranan)
                                             || p.Miktar.ToString().Contains(aranan)
                                             || p.SehirTablo.SehirAdi.Contains(aranan)
                                             || p.Aciklama.Contains(aranan)
                                             || p.Tarih.ToString().Contains(aranan));
                }

                if (tarih != null)
                {
                    DateTime Tarih = Convert.ToDateTime(tarih);
                    sorgu = sorgu.Where(p => p.Tarih == Tarih);
                }

                if (sehirId != null)
                {
                    sorgu = sorgu.Where(p => p.SehirTablo_SehirId == sehirId);
                }

                if (gelirGider != null)
                {
                    if (!(gelirGider == 0))
                    {
                        bool GelirGider = gelirGider == 1 ? true : false;
                        sorgu = sorgu.Where(p => p.GelirGider == GelirGider);
                    }
                }
                return sorgu.ToList();
            }
            else
            {
                int? kullaniciSehirId = KullaniciDAL.KullaniciSehir(kullaniciId);
                var sorgu = db.KasaTablo.Include(p => p.KullaniciBilgileriTablo).Include(p => p.SehirTablo)
                    .Where(p => p.SehirTablo_SehirId == kullaniciSehirId).AsQueryable();
                if (aranan != null)
                {
                    sorgu = sorgu.Where(p => p.KullaniciBilgileriTablo.KullaniciAdi.Contains(aranan)
                                             || p.KullaniciBilgileriTablo.KullaniciSoyadi.Contains(aranan)
                                             || p.KullaniciBilgileriTablo.KullaniciEPosta.Contains(aranan)
                                             || p.Miktar.ToString().Contains(aranan)
                                             || p.SehirTablo.SehirAdi.Contains(aranan)
                                             || p.Aciklama.Contains(aranan)
                                             || p.Tarih.ToString().Contains(aranan));
                }

                if (tarih != null)
                {
                    DateTime Tarih = Convert.ToDateTime(tarih);
                    sorgu = sorgu.Where(p => p.Tarih == Tarih);
                }
                if (gelirGider != null)
                {
                    if (!(gelirGider == 0))
                    {
                        bool GelirGider = gelirGider == 1 ? true : false;
                        sorgu = sorgu.Where(p => p.GelirGider == GelirGider);
                    }
                }
                return sorgu.ToList();
            }
        }

        public bool KullaniciIslemYapabilirMi(int? kullaniciId, int? sehirId)
        {
            if (KullaniciDAL.KullaniciMerkezdeMi(kullaniciId))
            {
                return true;
            }
            else
            {
                int? kullaniciSehirId = KullaniciDAL.KullaniciSehir(kullaniciId);
                if (kullaniciSehirId == sehirId)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public bool KasaIslemKaydet(KasaTablo model)
        {
            db.KasaTablo.Add(model);
            if (db.SaveChanges() > 0)
            {
                return true;
            }

            return false;
        }
    }
}
