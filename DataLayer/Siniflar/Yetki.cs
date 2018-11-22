using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
namespace DataLayer.Siniflar
{
    public class Yetki
    {
        private SosyalYardimDB db = new SosyalYardimDB();
        public List<YetkiTablo> YetkileriGetir(int? kullaniciId)
        {
            return db.YetkiTablo.Include(p => p.RotaTablo).Include(p => p.KullaniciBilgileriTablo)
                .Where(p => p.KullaniciBilgileriTablo_KullaniciId == kullaniciId).ToList();
        }

        public bool YetkiyiKaydet(int? yetkiId, bool? yetkiDurumu)
        {
            var yetki = db.YetkiTablo.FirstOrDefault(p => p.YetkiId == yetkiId);
            if (yetki != null)
            {
                if (yetki.GirebilirMi == yetkiDurumu)
                {
                    return true;
                }
                else
                {
                    if (yetkiDurumu == true)
                    {
                        yetki.GirebilirMi = true;
                    }
                    else
                    {
                        yetki.GirebilirMi = false;
                    }
                    if (db.SaveChanges() > 0)
                    {
                        return true;
                    }
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public YetkiTablo YetkiGetir(int? id)
        {
            return db.YetkiTablo.Include(p => p.RotaTablo).Include(p => p.KullaniciBilgileriTablo)
                .FirstOrDefault(p => p.YetkiId == id);
        }
    }
}
