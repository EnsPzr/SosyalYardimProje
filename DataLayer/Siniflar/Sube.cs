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
    }
}
