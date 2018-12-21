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
                var dondurulecek = db.KullaniciBilgileriTablo.Include(p => p.SehirTablo).Where(p => p.SehirTablo_SehirId == sehirId && p.BagisciMi == true
).AsQueryable();
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

        public bool KullaniciIslemYapabilirMi(int? KullaniciId, int? BagisciId)
        {
            if (kullaniciDAL.KullaniciMerkezdeMi(KullaniciId))
            {
                return true;
            }
            else
            {
                var Bagisci = kullaniciDAL.KullaniciBul(BagisciId);
                if (Bagisci != null)
                {
                    var sehirId = kullaniciDAL.KullaniciSehir(KullaniciId);
                    if (sehirId == Bagisci.SehirTablo_SehirId)
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
        }

        public bool BagisciKaydet(KullaniciBilgileriTablo model)
        {
            var kulTablo = db.KullaniciBilgileriTablo.FirstOrDefault(p => p.KullaniciId == model.KullaniciId);
            if (kulTablo != null)
            {
                kulTablo.KullaniciAdi = model.KullaniciAdi;
                kulTablo.KullaniciSoyadi = model.KullaniciSoyadi;
                kulTablo.SehirTablo_SehirId = model.SehirTablo_SehirId;
                kulTablo.KullaniciTelefonNumarasi = model.KullaniciTelefonNumarasi;
                kulTablo.KullaniciEPosta = model.KullaniciEPosta;
                kulTablo.KullaniciAdres = model.KullaniciAdres;
                db.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool BagiscidanVarMi(KullaniciBilgileriTablo model)
        {
            var bagisciTablo = db.KullaniciBilgileriTablo.FirstOrDefault(p => p.KullaniciId != model.KullaniciId
                                                                              && p.KullaniciEPosta ==
                                                                              model.KullaniciEPosta);
            if (bagisciTablo != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool BagisciSil(int? id)
        {
            var bagisci = kullaniciDAL.KullaniciBul(id);
            if (bagisci != null)
            {
                bagisci.AktifMi = false;
                db.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool BagisciIdVarMi(int? id)
        {
            if (db.KullaniciBilgileriTablo.FirstOrDefault(p => p.KullaniciId == id) != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
