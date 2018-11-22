using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Data.Entity.Migrations;

namespace DataLayer.Siniflar
{
    public class Kullanici
    {
        private SosyalYardimDB db = new SosyalYardimDB();
        public List<KullaniciBilgileriTablo> TumKullanicilariGetir()
        {
            return db.KullaniciBilgileriTablo.Include(p => p.SehirTablo).ToList();
        }
        public List<KullaniciBilgileriTablo> TumKullanicilariGetir(int? id)
        {
            return db.KullaniciBilgileriTablo.Include(p => p.SehirTablo).Where(p => p.SehirTablo_SehirId == id).ToList();
        }
        public List<KullaniciBilgileriTablo> FiltreliKullanicilariGetir(string aranan, int? SehirId, int? KullaniciId)
        {
            var kullanici = KullaniciGetir(KullaniciId);
            if (Convert.ToBoolean(kullanici.KullaniciMerkezdeMi))
            {
                IQueryable<KullaniciBilgileriTablo> sorgu = db.KullaniciBilgileriTablo.Include(p => p.SehirTablo)
                    .Where(p => (p.KullaniciAdi.Contains(aranan) ||
                                 p.KullaniciSoyadi.Contains(aranan) ||
                                 p.KullaniciTelegramKullaniciAdi.Contains(aranan) ||
                                 p.KullaniciTelefonNumarasi.Contains(aranan) ||
                                 p.KullaniciEPosta.Contains(aranan)
                                 || p.SehirTablo.SehirAdi.Contains(aranan)));
                if (SehirId != null)
                {
                    sorgu = sorgu.Where(p => p.SehirTablo_SehirId == SehirId);
                }
                return sorgu.ToList();
            }
            else
            {
                IQueryable<KullaniciBilgileriTablo> sorgu = db.KullaniciBilgileriTablo.Include(p => p.SehirTablo)
                    .Where(p => (p.KullaniciAdi.Contains(aranan) ||
                                 p.KullaniciSoyadi.Contains(aranan) ||
                                 p.KullaniciTelegramKullaniciAdi.Contains(aranan) ||
                                 p.KullaniciTelefonNumarasi.Contains(aranan) ||
                                 p.KullaniciEPosta.Contains(aranan)) &&p.SehirTablo_SehirId==kullanici.SehirTablo_SehirId);
                return sorgu.ToList();
            }
            
        }
        
        public List<SehirTablo> TumSehirler()
        {
            return db.SehirTablo.ToList();
        }

        public KullaniciBilgileriTablo KullaniciGetir(int? id)
        {
            return db.KullaniciBilgileriTablo.Include(p => p.SehirTablo).FirstOrDefault(p => p.KullaniciId == id);
        }

        public bool KullaniciSil(int? id)
        {
            KullaniciGetir(id).AktifMi = false;
            if (db.SaveChanges()>0) return true;
            else return false;
        }

        public bool KullaniciVarMi(String eposta)
        {
            return db.KullaniciBilgileriTablo.FirstOrDefault(p => p.KullaniciEPosta == eposta) != null ? true : false;
        }
        public bool KullaniciVarMi(String eposta, int? id)
        {
            return db.KullaniciBilgileriTablo.FirstOrDefault(p => p.KullaniciEPosta == eposta&&p.KullaniciId!=id) != null ? true : false;
        }

        public bool KullaniciEkle(KullaniciBilgileriTablo eklenecekKullanici)
        {
            db.KullaniciBilgileriTablo.Add(eklenecekKullanici);
            if (db.SaveChanges() > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool KullaniciGuncelle(KullaniciBilgileriTablo guncellenmisKullanici)
        {
            db.KullaniciBilgileriTablo.AddOrUpdate(guncellenmisKullanici);
            if (db.SaveChanges() > 0)
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
