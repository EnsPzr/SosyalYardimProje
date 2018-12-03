using System;
using System.Collections.Generic;
using System.Linq;
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
                int? sehirId = kullaniciDAL.KullaniciBul(KullaniciId).SehirTablo_SehirId;
                return db.IhtiyacSahibiTablo.Include(p => p.SehirTablo).Where(p =>
                    p.SehirTablo_SehirId == sehirId).ToList();
            }
        }

        public List<IhtiyacSahibiTablo> FiltreliIhtiyacSahipleriListesiniGetir(String aranan, int? sehirId,int? kullaniciId)
        {
            var ihtiyacSahipleri = db.IhtiyacSahibiTablo.Include(p => p.SehirTablo).Where(p =>
                p.IhtiyacSahibiAdi.Contains(aranan) ||
                p.IhtiyacSahibiSoyadi.Contains(aranan) ||
                p.IhtiyacSahibiTelNo.Contains(aranan) ||
                p.IhtiyacSahibiAdres.Contains(aranan) ||
                p.SehirTablo.SehirAdi.Contains(aranan) ||
                p.IhtiyacSahibiAciklama.Contains(aranan)).AsQueryable();
            if (sehirId != null)
            {
                ihtiyacSahipleri = ihtiyacSahipleri.Where(p => p.SehirTablo_SehirId == sehirId);
            }

            if (kullaniciId != null)
            {
                var kullanici = kullaniciDAL.KullaniciBul(kullaniciId);
                if (kullanici.KullaniciMerkezdeMi == false)
                {
                    ihtiyacSahipleri = ihtiyacSahipleri.Where(p =>
                        p.SehirTablo_SehirId == kullanici.SehirTablo_SehirId);
                }
            }
            return ihtiyacSahipleri.ToList();
        }
    }
}
