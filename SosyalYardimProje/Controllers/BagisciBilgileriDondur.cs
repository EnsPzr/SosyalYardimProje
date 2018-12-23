using DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BusinessLayer.BagisciSiniflar;

namespace SosyalYardimProje.Controllers
{
    public class BagisciBilgileriDondur
    {
        private static BusinessLayer.BagisciSiniflar.BagisciYonetimi bagisciYonetimi = new BagisciYonetimi();
        public static int? KullaniciId()
        {
            if (HttpContext.Current.Session["KullaniciId"] != null)
            {
                String kullaniciId = HttpContext.Current.Session["KullaniciId"].ToString();
                return Convert.ToInt32(kullaniciId);
            }
            else return 1;
        }

        public static KullaniciBilgileriTablo KullaniciBilgileriGetir()
        {
            if (HttpContext.Current.Session["KullaniciId"] != null)
            {
                String kullaniciIdStr = HttpContext.Current.Session["KullaniciId"].ToString();
                int? KullaniciId = Convert.ToInt32(kullaniciIdStr);
                return bagisciYonetimi.LoginKullaniciBul(KullaniciId);
            }
            else return bagisciYonetimi.LoginKullaniciBul(1004);
        }

        public static String HataMesajlariniOku(List<String> hataListesi)
        {
            String hatalar = "";
            foreach (var hata in hataListesi)
            {
                hatalar += hata + "\n";
            }

            return hatalar;
        }
    }
}