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

        public List<GeriBildirimTablo> FiltreliGeriBildirimleriGetir(int? kullaniciId, string aranan, string tarih, int? sehirId)
        {
            if (kullaniciDAL.KullaniciMerkezdeMi(kullaniciId))
            {
                var sorgu = db.GeriBildirimTablo.Include(p => p.KullaniciBilgileriTablo).AsQueryable();

                if (aranan != null)
                {
                    sorgu = sorgu.Where(p => p.GeriBildirimKonu.Contains(aranan)
                                             || p.GeriBildirimMesaj.Contains(aranan)
                                             || p.Tarih.ToString().Contains(aranan)
                                             || p.KullaniciBilgileriTablo.KullaniciAdi.Contains(aranan)
                                             || p.KullaniciBilgileriTablo.KullaniciSoyadi.Contains(aranan)
                                             || p.KullaniciBilgileriTablo.KullaniciEPosta.Contains(aranan)
                                             || p.KullaniciBilgileriTablo.KullaniciTelefonNumarasi.Contains(aranan));
                }

                if (tarih != null)
                {
                    DateTime? cTarih = Convert.ToDateTime(tarih);
                    sorgu = sorgu.Where(p => p.Tarih == cTarih);
                }

                if (sehirId != null)
                {
                    sorgu = sorgu.Where(p => p.KullaniciBilgileriTablo.SehirTablo_SehirId == sehirId);
                }

                return sorgu.ToList();
            }
            else
            {
                int? kullaniciSehirId = kullaniciDAL.KullaniciSehir(kullaniciId);
                var sorgu = db.GeriBildirimTablo.Include(p => p.KullaniciBilgileriTablo).Where(p => p.KullaniciBilgileriTablo.SehirTablo_SehirId == kullaniciSehirId).AsQueryable();

                if (aranan != null)
                {
                    sorgu = sorgu.Where(p => p.GeriBildirimKonu.Contains(aranan)
                                             || p.GeriBildirimMesaj.Contains(aranan)
                                             || p.Tarih.ToString().Contains(aranan)
                                             || p.KullaniciBilgileriTablo.KullaniciAdi.Contains(aranan)
                                             || p.KullaniciBilgileriTablo.KullaniciSoyadi.Contains(aranan)
                                             || p.KullaniciBilgileriTablo.KullaniciEPosta.Contains(aranan)
                                             || p.KullaniciBilgileriTablo.KullaniciTelefonNumarasi.Contains(aranan));
                }

                if (tarih != null)
                {
                    DateTime? cTarih = Convert.ToDateTime(tarih);
                    sorgu = sorgu.Where(p => p.Tarih == cTarih);
                }
                
                return sorgu.ToList();
            }
        }

        public GeriBildirimTablo GeriBildirimGetir(int? geriBildirimId)
        {
            var geriBildirim = db.GeriBildirimTablo.Include(p => p.KullaniciBilgileriTablo)
                .FirstOrDefault(p => p.GeriBildirimId == geriBildirimId);
            if (geriBildirim != null)
            {
                geriBildirim.GeriBildirimDurumu = 1;
                db.SaveChanges();
            }

            return geriBildirim;
        }

        public bool KullaniciIslemYapabilirMi(int? kullaniciId, int? geriBildirimId)
        {
            if (kullaniciDAL.KullaniciMerkezdeMi(kullaniciId))
            {
                return true;
            }
            else
            {
                var geriBildirim = db.GeriBildirimTablo.Include(p => p.KullaniciBilgileriTablo)
                    .FirstOrDefault(p => p.GeriBildirimId == geriBildirimId);
                if (geriBildirim != null)
                {
                    int? kullaniciSehirId = kullaniciDAL.KullaniciSehir(kullaniciId);
                    if (geriBildirim.KullaniciBilgileriTablo.SehirTablo_SehirId != null)
                    {
                        if (geriBildirim.KullaniciBilgileriTablo.SehirTablo_SehirId == kullaniciSehirId)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
        }

        public bool GeriBildirimKaydet(int? geriBildirimId, int? durumId)
        {
            var geriBildirim = db.GeriBildirimTablo.FirstOrDefault(p => p.GeriBildirimId == geriBildirimId);
            if (geriBildirim != null)
            {
                geriBildirim.GeriBildirimDurumu = durumId;
                db.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
