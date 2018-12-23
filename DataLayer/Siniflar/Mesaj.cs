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
                return db.MesajTablo.Include(p => p.KullaniciBilgileriTablo)
                    .Where(p => p.KullaniciBilgleriTablo_KullaniciId == kullaniciId).ToList();
            }
        }

        public List<MesajTablo> FiltreliMesajlariGetir(int? kullaniciId, int? arananKullaniciId, string aranan,
            string tarih, int? kimeGonderildi)
        {
            if (kullaniciDAL.KullaniciMerkezdeMi(kullaniciId))
            {
                var sorgu = db.MesajTablo.Include(p => p.KullaniciBilgileriTablo).Include(p => p.MesajDetayTablo)
                    .AsQueryable();
                if (arananKullaniciId != null)
                {
                    sorgu = sorgu.Where(p => p.KullaniciBilgleriTablo_KullaniciId == arananKullaniciId);
                }

                if (tarih != null)
                {
                    DateTime? tarihDate = Convert.ToDateTime(tarih);
                    sorgu = sorgu.Where(p => p.Tarih == tarihDate);
                }

                if (kimeGonderildi != null)
                {
                    sorgu = sorgu.Where(p => p.KimeAtildi == kimeGonderildi);
                }
                return sorgu.ToList();
            }
            else
            {
                var sorgu = db.MesajTablo.Include(p => p.KullaniciBilgileriTablo).Include(p => p.MesajDetayTablo)
                    .Where(p => p.KullaniciBilgleriTablo_KullaniciId == kullaniciId).AsQueryable();

                if (tarih != null)
                {
                    DateTime? tarihDate = Convert.ToDateTime(tarih);
                    sorgu = sorgu.Where(p => p.Tarih == tarihDate);
                }
                if (kimeGonderildi != null)
                {
                    sorgu = sorgu.Where(p => p.KimeAtildi == kimeGonderildi);
                }
                return sorgu.ToList();
            }
        }

        public List<MesajDetayTablo> TumMesajDetayGetir(int? mesajId)
        {
            return db.MesajDetayTablo.Include(p => p.KullaniciBilgileriTablo).Where(p => p.MesajDetayId == mesajId).ToList();
        }

        public List<MesajDetayTablo> FiltreliMesajDetayGetir(int? mesajId, string aranan)
        {
            var sorgu = db.MesajDetayTablo.Include(p => p.KullaniciBilgileriTablo).Where(p => p.MesajTablo_MesajId == mesajId).AsQueryable();

            if (aranan != null)
            {
                sorgu = sorgu.Where(p => p.MesajMetni.Contains(aranan)
                                         || p.KullaniciBilgileriTablo.KullaniciAdi.Contains(aranan)
                                         || p.KullaniciBilgileriTablo.KullaniciSoyadi.Contains(aranan)
                                         || p.KullaniciBilgileriTablo.KullaniciEPosta.Contains(aranan));
            }

            return sorgu.ToList();
        }

        public bool KullaniciIslemYapabilirMi(int? kullaniciId, int? mesajId)
        {
            if (kullaniciDAL.KullaniciMerkezdeMi(kullaniciId))
            {
                return true;
            }
            else
            {
                if (db.MesajTablo.FirstOrDefault(p =>
                    p.KullaniciBilgleriTablo_KullaniciId == kullaniciId && p.MesajId == mesajId) != null)
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
}
