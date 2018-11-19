using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Runtime.Remoting.Messaging;

namespace DataLayer
{
    public class KullaniciYonetimi
    {
        private SosyalYardimDB db = new SosyalYardimDB();
        public KullaniciBilgileriTablo KullaniciBul(String KullaniciGuId)
        {
            var Kullanici = db.KullaniciBilgileriTablo.FirstOrDefault(p => p.KullaniciGuId == KullaniciGuId);
            return Kullanici;
        }

        public String KullaniciBul(String Eposta, String Sifre)
        {
            var Kullanici = db.KullaniciBilgileriTablo.FirstOrDefault(p => p.KullaniciEPosta == Eposta
                                                                               && p.KullaniciSifre == Sifre);
            if (Kullanici != null) return Kullanici.KullaniciGuId;
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
                p.KullaniciBilgileriTablo_KullaniciGuId == kullanici.KullaniciGuId
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
                return db.BagisTablo.Include(p => p.KullaniciBilgileriTablo).Where(p => p.TeslimAlindiMi == false&&p.KullaniciBilgileriTablo.SehirTablo_Sehirid==id).ToList();
            }
        }

        public List<IhtiyacSahibiKontrolTablo> TeslimBekleyenEsyalar(int? id)
        {
            if (id == null)
            {
                return db.IhtiyacSahibiKontrolTablo.Include(p => p.IhtiyacSahibiTablo)
                    .Include(p => p.IhtiyacSahibiVerileceklerTablo).Where(p => p.MuhtacMi == true && p.TeslimYapildiMi == false).ToList();
            }
            else
            {
                return db.IhtiyacSahibiKontrolTablo.Include(p => p.IhtiyacSahibiTablo)
                    .Include(p => p.IhtiyacSahibiVerileceklerTablo).Where(p => p.IhtiyacSahibiTablo.SehirTablo_SehirId == id && p.MuhtacMi == true && p.TeslimYapildiMi == false).ToList();
            }
        }

        public List<IhtiyacSahibiKontrolTablo> OnayBekleyenIhtiyacSahipleri(int? id)
        {
            if (id == null)
            {
                return db.IhtiyacSahibiKontrolTablo.Include(p => p.IhtiyacSahibiTablo).Where(p => p.MuhtacMi == null)
                    .ToList();
            }
            else
            {
                return db.IhtiyacSahibiKontrolTablo.Include(p => p.IhtiyacSahibiTablo).Where(p => p.MuhtacMi == null && p.IhtiyacSahibiTablo.SehirTablo_SehirId == id)
                    .ToList();
            }
        }
    }
}
