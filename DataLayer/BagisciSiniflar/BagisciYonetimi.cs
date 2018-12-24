using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
namespace DataLayer.BagisciSiniflar
{
    public class BagisciYonetimi
    {
        private SosyalYardimDB db = new SosyalYardimDB();

        public KullaniciBilgileriTablo BagisciBul(String ePosta, String sifre)
        {
            var bagisci = db.KullaniciBilgileriTablo.FirstOrDefault(p =>
                p.BagisciMi == true && p.KullaniciEPosta == ePosta
                                    && p.KullaniciSifre == sifre);
            if (bagisci != null)
            {
                return bagisci;
            }
            else
            {
                return null;
            }
        }

        public KullaniciBilgileriTablo KullaniciBul(int? id)
        {
            return db.KullaniciBilgileriTablo.FirstOrDefault(p => p.KullaniciId == id);
        }

        public List<BagisTablo> BagislariGetir(int? kullaniciId)
        {
            return db.BagisTablo.Where(p => p.KullaniciBilgileriTablo_KullaniciId == kullaniciId).ToList();
        }

        public List<BagisDetayTablo> BagisDetayBul(int? bagisId)
        {
            return db.BagisDetayTablo.Include(p=>p.EsyaTablo).Where(p => p.BagisTablo_BagisId == bagisId).ToList();
        }

        public bool BagisciVarMi(string ePosta)
        {
            if (db.KullaniciBilgileriTablo.FirstOrDefault(p => p.KullaniciEPosta == ePosta) != null)
            {
                return true;
            }

            return false;
        }

        public bool BagisciKaydet(KullaniciBilgileriTablo kullaniciTablo)
        {
            db.KullaniciBilgileriTablo.Add(kullaniciTablo);
            if (db.SaveChanges() > 0)
            {
                return true;
            }

            return false;

        }

        public KullaniciBilgileriTablo BagisciGetir(int? bagisicId)
        {
            return db.KullaniciBilgileriTablo.Include(p => p.SehirTablo)
                .FirstOrDefault(p => p.KullaniciId == bagisicId);
        }


        public bool BagisciGuncelle(KullaniciBilgileriTablo kullaniciTablo)
        {
            var kullanici = db.KullaniciBilgileriTablo.FirstOrDefault(p => p.KullaniciId == kullaniciTablo.KullaniciId);
            if (kullanici != null)
            {
                kullanici.KullaniciAdi = kullaniciTablo.KullaniciAdi;
                kullanici.KullaniciSoyadi = kullaniciTablo.KullaniciSoyadi;
                kullanici.KullaniciAdres = kullaniciTablo.KullaniciAdres;
                kullanici.SehirTablo_SehirId = kullaniciTablo.SehirTablo_SehirId;
                kullanici.KullaniciEPosta = kullaniciTablo.KullaniciEPosta;
                kullanici.KullaniciSifre = kullaniciTablo.KullaniciSifre;
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
