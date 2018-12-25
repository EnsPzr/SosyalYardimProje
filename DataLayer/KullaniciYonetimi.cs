using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;

namespace DataLayer
{
    public class KullaniciYonetimi
    {
        private SosyalYardimDB db = new SosyalYardimDB();
        public KullaniciBilgileriTablo KullaniciBul(int? KullaniciId)
        {
            var Kullanici = db.KullaniciBilgileriTablo.Include(p=>p.SehirTablo).FirstOrDefault(p => p.KullaniciId == KullaniciId);
            return Kullanici;
        }

        public String KullaniciBul(String Eposta, String Sifre)
        {
            var Kullanici = db.KullaniciBilgileriTablo.FirstOrDefault(p => p.KullaniciEPosta == Eposta
                                                                               && p.KullaniciSifre == Sifre);
            if (Kullanici != null) return Kullanici.KullaniciId.ToString();
            else return String.Empty;
        }
        public String KullaniciBul(String Eposta)
        {
            var Kullanici = db.KullaniciBilgileriTablo.FirstOrDefault(p => p.KullaniciEPosta == Eposta);
            if (Kullanici != null) return Kullanici.KullaniciId.ToString();
            else return String.Empty;
        }
        public RotaTablo RotaBul(String ControllerAdi, String ActionAdi)
        {
            var Rota = db.RotaTablo.FirstOrDefault(p => p.ActionAdi == ActionAdi && p.ControllerAdi == ControllerAdi);
            return Rota;
        }

        public YetkiTablo YetkiVarMi(RotaTablo rota, KullaniciBilgileriTablo kullanici)
        {
            var YetkiVarMi = db.YetkiTablo.FirstOrDefault(p =>
                p.KullaniciBilgileriTablo_KullaniciId == kullanici.KullaniciId
                && p.RotaTablo_RotaId == rota.RotaId
                && p.GirebilirMi == true);
            return YetkiVarMi;
        }

        public List<YetkiTablo> TumYetkileriGetir()
        {
            var Yetkiler = db.YetkiTablo.Include(b => b.KullaniciBilgileriTablo).Include(b => b.RotaTablo).ToList();
            return Yetkiler;
        }

        public List<BagisTablo> TeslimAlinmaBekleyenBagislar(int? id)
        {
            if (id == null)
            {
                return db.BagisTablo.Include(p => p.KullaniciBilgileriTablo).Where(p=>p.TeslimAlindiMi==false).ToList();
            }
            else
            {
                return db.BagisTablo.Include(p => p.KullaniciBilgileriTablo).Where(p => p.TeslimAlindiMi == false&&p.KullaniciBilgileriTablo.SehirTablo_SehirId==id).ToList();
            }
        }

        public List<IhtiyacSahibiKontrolTablo> TeslimBekleyenEsyalar(int? id)
        {
            if (id == null)
            {
                return db.IhtiyacSahibiKontrolTablo.Include(p => p.IhtiyacSahibiTablo)
                    .Include(p => p.IhtiyacSahibiVerilecekEsyaTablo).Where(p => p.MuhtacMi == true && p.TeslimTamamlandiMi == false).ToList();
            }
            else
            {
                return db.IhtiyacSahibiKontrolTablo.Include(p => p.IhtiyacSahibiTablo)
                    .Include(p => p.IhtiyacSahibiVerilecekEsyaTablo).Where(p => p.IhtiyacSahibiTablo.SehirTablo_SehirId == id && p.MuhtacMi == true && p.TeslimTamamlandiMi == false).ToList();
            }
        }

        public List<IhtiyacSahibiKontrolTablo> OnayBekleyenIhtiyacSahipleri(int? id)
        {
            if (id == null)
            {
                return db.IhtiyacSahibiKontrolTablo.Include(p => p.IhtiyacSahibiTablo).Where(p => p.MuhtacMi == null ||p.MuhtacMi==false)
                    .ToList();
            }
            else
            {
                return db.IhtiyacSahibiKontrolTablo.Include(p => p.IhtiyacSahibiTablo).Where(p => (p.MuhtacMi == null||p.MuhtacMi==false) && p.IhtiyacSahibiTablo.SehirTablo_SehirId == id)
                    .ToList();
            }
        }

        public bool KullaniciAktifMi(int? id)
        {
            Dispose();
            var kullanici = db.KullaniciBilgileriTablo.FirstOrDefault(p => p.KullaniciId == id);
            if (kullanici != null)
            {
                if (Convert.ToBoolean(kullanici.AktifMi)) return true;
                else return false;
            }
            else
            {
                return false;
            }
        }

        public void Dispose()
        {
            db.Dispose();
            db = null;
            db = new SosyalYardimDB();
        }

        public bool KullaniciMerkezdeMi(int? id)
        {
            var kullanici = KullaniciBul(id);
            if (kullanici != null)
            {
                if (kullanici.KullaniciMerkezdeMi == true)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public int? KullaniciSehir(int? id)
        {
            var kullanici = KullaniciBul(id);
            if (kullanici != null)
            {
                return kullanici.SehirTablo_SehirId;
            }
            else
            {
                return 0;
            }
        }
    }
}
