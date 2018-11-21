using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
namespace DataLayer.Siniflar
{
    public class Sube
    {
        private SosyalYardimDB db = new SosyalYardimDB();
        public List<SubeTablo> TumSubeleriGetir()
        {
            return db.SubeTablo.Include(p => p.KullaniciBilgileriTablo).Include(p => p.SehirTablo).ToList();
        }

        public List<SubeTablo> FiltreliSubeleriGetir(String aranan)
        {
            return db.SubeTablo.Include(p => p.KullaniciBilgileriTablo).Include(p => p.SehirTablo)
                .Where(p => p.KullaniciBilgileriTablo.KullaniciAdi.Contains(aranan) ||
                            p.KullaniciBilgileriTablo.KullaniciSoyadi.Contains(aranan) ||
                            p.KullaniciBilgileriTablo.KullaniciTelegramKullaniciAdi.Contains(aranan) ||
                            p.KullaniciBilgileriTablo.KullaniciTelefonNumarasi.Contains(aranan) ||
                            p.KullaniciBilgileriTablo.KullaniciEPosta.Contains(aranan)
                            || p.SehirTablo.SehirAdi.Contains(aranan)).ToList();
        }

        public bool sehirGorevlisiVarMi(int? SehirId)
        {
            return db.SubeTablo.FirstOrDefault(p => p.SehirTablo_SehirId == SehirId) != null ? true : false;
        }

        public bool SubeEkle(SubeTablo eklenecekSube)
        {
            db.SubeTablo.Add(eklenecekSube);
            if (db.SaveChanges() > 0)
            {
                return true;
            }
            return false;
        }

        public bool SubeSil(int? id)
        {
            var silinecekSube = db.SubeTablo.FirstOrDefault(p => p.SubeId == id);
            if (silinecekSube != null)
            {
                db.SubeTablo.Remove(silinecekSube);
                if (db.SaveChanges()>0)
                {
                    return true;
                }

                return false;
            }
            else
            {
                return false;
            }
        }

        public SubeTablo SubeBul(int? id)
        {
            var subeBilgileri = db.SubeTablo.FirstOrDefault(p => p.SubeId == id);
            if (subeBilgileri != null)
            {
                return subeBilgileri;
            }
            else
            {
                return null;
            }
        }
    }
}
