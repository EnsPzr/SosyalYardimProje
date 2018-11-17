using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
