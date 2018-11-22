using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer.Models.KullaniciModelleri;
using BusinessLayer.Models.YetkiModelleri;
using BusinessLayer.Models.OrtakModeller;
using DataLayer;
namespace BusinessLayer.Siniflar
{
    public class Yetki
    {
        private DataLayer.Siniflar.Yetki yetkiDAL = new DataLayer.Siniflar.Yetki();
        private Kullanici kullaniciBAL = new Kullanici();

        public List<KullaniciModel> KullanicilariGetir(int? id)
        {
            return kullaniciBAL.TumKullanicilariGetir(id);
        }

        public List<KullaniciModel> FiltreliKullanicilariGetir(string aranan, int? sehirId, int? kullaniciId)
        {
            return kullaniciBAL.FiltreliKullanicilariGetir(aranan, sehirId, kullaniciId);
        }

        public bool KullaniciAyniBolgedeMi(int? kullaniciId, int? loginKullaniciId)
        {
            var arananKullanici = kullaniciBAL.KullaniciGetir(kullaniciId);
            var loginKullanici = kullaniciBAL.KullaniciGetir(loginKullaniciId);
            if (loginKullanici.KullaniciMerkezdeMi == true)
            {
                return true;
            }
            else
            {
                if (loginKullanici.Sehir.SehirId == arananKullanici.Sehir.SehirId)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public List<YetkiModel> YetkileriGetir(int? kullaniciId)
        {
            var yetkiler = yetkiDAL.YetkileriGetir(kullaniciId);
            if (yetkiler == null)
            {
                return null;
            }
            else
            {
                List<YetkiModel> yetkilerListModel = new List<YetkiModel>();
                for (int i = 0; i < yetkiler.Count; i++)
                {
                    var eklenecekYetki = new YetkiModel();
                    eklenecekYetki.RotaAdi = yetkiler[i].RotaTablo.LinkAdi;
                    eklenecekYetki.RotaId = yetkiler[i].RotaTablo_RotaId;
                    eklenecekYetki.Kullanici = kullaniciBAL.KullaniciGetir(kullaniciId);
                    eklenecekYetki.YetkiId = yetkiler[i].YetkiId;
                    eklenecekYetki.GirebilirMi = yetkiler[i].GirebilirMi;
                    yetkilerListModel.Add(eklenecekYetki);
                }
                return yetkilerListModel;
            }
        }

        public IslemOnayModel YetkileriKaydet(List<YetkiModel> yetkiler)
        {
            IslemOnayModel onay = new IslemOnayModel();
            int sayac = 0;
            for (int i = 0; i < yetkiler.Count; i++)
            {

                bool? cevap = yetkiDAL.YetkiyiKaydet(yetkiler[i].YetkiId, yetkiler[i].GirebilirMi);
                if (cevap == true)
                {
                    sayac++;
                }
            }

            if (!(sayac == yetkiler.Count))
            {
                onay.TamamlandiMi = false;
                onay.HataMesajlari.Add("Yetkilerden bazıları kayıt edilirken hatalar oluştu.");
            }
            else
            {
                onay.TamamlandiMi = true;
            }
            return onay;
        }

        public List<YetkiModel> YetkidenKullaniciBul(int? YetkiId)
        {
            var Yetki = yetkiDAL.YetkiGetir(YetkiId);
            return YetkileriGetir(Yetki.KullaniciBilgileriTablo_KullaniciId);
        }
    }
}
