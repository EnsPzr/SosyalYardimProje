using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

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
        public List<SehirTablo> TumSehirler()
        {
            return db.SehirTablo.ToList();
        }
    }
}
