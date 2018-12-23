using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
namespace DataLayer.Siniflar
{
    public class Mesaj
    {
        private SosyalYardimDB db = new SosyalYardimDB();
        private KullaniciYonetimi kullaniciDAL = new KullaniciYonetimi();
        public List<MesajTablo> TumMesajlariGetir(int? kullaniciId)
        {
            if (kullaniciDAL.KullaniciMerkezdeMi(kullaniciId))
            {
                return db.MesajTablo.Include(p => p.KullaniciBilgileriTablo).ToList();
            }
            else
            {
                return db.MesajTablo.Include(p => p.KullaniciBilgileriTablo).Where(p => p.KullaniciBilgleriTablo_KullaniciId == kullaniciId).ToList();
            }
        }
    }
}
