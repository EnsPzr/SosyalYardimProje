using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer.Models.MesajModelleri;
using BusinessLayer.Models.OrtakModeller;
using DataLayer;

namespace BusinessLayer.Siniflar
{
    public class Mesaj
    {
        private DataLayer.Siniflar.Mesaj mesajDAL = new DataLayer.Siniflar.Mesaj();
        private DataLayer.KullaniciYonetimi kullaniciDAL = new DataLayer.KullaniciYonetimi();
        public List<MesajModel> TumMesajlariGetir(int? kullaniciId)
        {
            var mesajlar = mesajDAL.TumMesajlariGetir(kullaniciId);
            var gonMesajlar = mesajlar.Select(p => new MesajModel()
            {
                AliciInt = p.KimeAtildi,
                AliciStr = p.KimeAtildi != null ? p.KimeAtildi == 0 ? "Herkes" : "Koordinatörler" : "Hiç Kimse",
                KullaniciAdiSoyadi = p.KullaniciBilgileriTablo.KullaniciAdi + " " + p.KullaniciBilgileriTablo.KullaniciSoyadi,
                KullaniciId = p.KullaniciBilgleriTablo_KullaniciId,
                MesajId = p.MesajId,
                Tarih = p.Tarih,
                TarihStr = p.Tarih != null ? Convert.ToDateTime(p.Tarih).ToShortDateString() : ""
            }).ToList();
            return gonMesajlar;
        }

        public List<MesajModel> FiltreliMesajlariGetir(int? kullaniciId, int? arananKullaniciId, string aranan,
            string tarih, int? kimeGonderildi)
        {
            var mesajlar = mesajDAL.FiltreliMesajlariGetir(kullaniciId, arananKullaniciId, aranan, tarih, kimeGonderildi);
            var gonMesajlar = mesajlar.Select(p => new MesajModel()
            {
                AliciInt = p.KimeAtildi,
                AliciStr = p.KimeAtildi != null ? p.KimeAtildi == 0 ? "Herkes" : "Koordinatörler" : "Hiç Kimse",
                KullaniciAdiSoyadi = p.KullaniciBilgileriTablo.KullaniciAdi + " " + p.KullaniciBilgileriTablo.KullaniciSoyadi,
                KullaniciId = p.KullaniciBilgleriTablo_KullaniciId,
                MesajId = p.MesajId,
                Tarih = p.Tarih,
                TarihStr = p.Tarih != null ? Convert.ToDateTime(p.Tarih).ToShortDateString() : ""
            }).ToList();
            return gonMesajlar;
        }

        public List<MesajDetayModel> TumMesajlarDetaylariGetir(int? mesajId)
        {
            var mesajlar = mesajDAL.TumMesajDetayGetir(mesajId);
            var gonMesajlar = mesajlar.Select(p => new MesajDetayModel()
            {
                KullaniciAdiSoyadi = p.KullaniciBilgileriTablo.KullaniciAdi + " " + p.KullaniciBilgileriTablo.KullaniciSoyadi,
                MesajMetni = p.MesajMetni
            }).ToList();
            return gonMesajlar;
        }

        public List<MesajDetayModel> FiltreliMesajlarDetaylariGetir(int? mesajId, string aranan)
        {
            var mesajlar = mesajDAL.FiltreliMesajDetayGetir(mesajId, aranan);
            var gonMesajlar = mesajlar.Select(p => new MesajDetayModel()
            {
                KullaniciAdiSoyadi = p.KullaniciBilgileriTablo.KullaniciAdi + " " + p.KullaniciBilgileriTablo.KullaniciSoyadi,
                MesajMetni = p.MesajMetni
            }).ToList();
            return gonMesajlar;
        }

        public IslemOnayModel MesajGonder(GonderilecekMesajModel model)
        {
            IslemOnayModel onay = new IslemOnayModel();
            if (kullaniciDAL.KullaniciMerkezdeMi(model.GonderenId))
            {
                MesajTablo mesajTablo = new MesajTablo();
                mesajTablo.KimeAtildi = model.KimeGonderilecek;
                mesajTablo.KullaniciBilgleriTablo_KullaniciId = model.GonderenId;
                mesajTablo.Tarih = DateTime.Today;
                mesajTablo.Zaman = DateTime.Now.TimeOfDay;

                MesajDetayTablo mesajDetayTablo = new MesajDetayTablo();
                mesajDetayTablo.MesajMetni = model.MesajMetni;
                onay.TamamlandiMi = mesajDAL.MesajGonder(mesajTablo, mesajDetayTablo, model.SehirId);
            }
            else
            {
                if (model.KimeGonderilecek == 0)
                {
                    MesajTablo mesajTablo = new MesajTablo();
                    mesajTablo.KimeAtildi = model.KimeGonderilecek;
                    mesajTablo.KullaniciBilgleriTablo_KullaniciId = model.GonderenId;
                    mesajTablo.Tarih = DateTime.Today;
                    mesajTablo.Zaman = DateTime.Now.TimeOfDay;

                    MesajDetayTablo mesajDetayTablo = new MesajDetayTablo();
                    mesajDetayTablo.MesajMetni = model.MesajMetni;
                    onay.TamamlandiMi = mesajDAL.MesajGonder(mesajTablo, mesajDetayTablo, null);
                }
                else
                {
                    onay.TamamlandiMi = false;
                    onay.HataMesajlari.Add("Koordinatör olduğunuzdan dolayı sadece herkes seçeneğini seçebilirsiniz.");
                }
            }

            return onay;
        }

        public bool KullaniciIslemYapabilirMi(int? kullaniciId, int? mesajId)
        {
            return mesajDAL.KullaniciIslemYapabilirMi(kullaniciId, mesajId);
        }
    }
}
