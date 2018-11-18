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
            var Kullanici = db.KullaniciBilgileriTablo.FirstOrDefault(p=>p.KullaniciGuId==KullaniciGuId);
            return Kullanici;
        }

        public String KullaniciBul(String Eposta, String Sifre)
        {
            var KullaniciGuId = db.KullaniciBilgileriTablo.FirstOrDefault(p => p.KullaniciEPosta == Eposta
                                                                               && p.KullaniciSifre == Sifre).KullaniciGuId;
            if (KullaniciGuId != null) return KullaniciGuId;
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
    }
}
