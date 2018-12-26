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
            else return 1004;
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