using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
namespace DataLayer.Siniflar
{
    public class IhtiyacSahibi
    {
        private KullaniciYonetimi kullaniciDAL= new KullaniciYonetimi();
        private SosyalYardimDB db = new SosyalYardimDB();
        public List<IhtiyacSahibiTablo> TumIhtiyacSahipleriniGetir(int? KullaniciId)
        {
            if (kullaniciDAL.KullaniciBul(KullaniciId).KullaniciMerkezdeMi == true)
            {
                return db.IhtiyacSahibiTablo.Include(p=>p.SehirTablo).ToList();
            }
            else
            {
                return db.IhtiyacSahibiTablo.Include(p => p.SehirTablo).Where(p =>
                    p.SehirTablo_SehirId == kullaniciDAL.KullaniciBul(KullaniciId).SehirTablo_SehirId).ToList();
            }
        }
    }
}
