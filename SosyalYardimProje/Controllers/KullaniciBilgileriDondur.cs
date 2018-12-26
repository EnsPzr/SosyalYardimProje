using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BusinessLayer;
using BusinessLayer.Siniflar;
using SosyalYardimProje.Filters;
using KullaniciYonetimi = BusinessLayer.KullaniciYonetimi;

namespace SosyalYardimProje.Controllers
{
    [HataFiltresi]
    public static class KullaniciBilgileriDondur
    {
        private static BusinessLayer.KullaniciYonetimi kullaniciYonetimi = new BusinessLayer.KullaniciYonetimi();
        private static Kullanici kullaniciBAL = new Kullanici();
        public static int? KullaniciId()
        {
            if (HttpContext.Current.Session["KullaniciId"] != null)
            {
                String kullaniciId = HttpContext.Current.Session["KullaniciId"].ToString();
                return Convert.ToInt32(kullaniciId);
            }
            else return null;
        }

        public static bool? KullaniciMerkezdeMi()
        {
            if (HttpContext.Current.Session["KullaniciId"] != null)
            {
                String kullaniciId = HttpContext.Current.Session["KullaniciId"].ToString();
                if (kullaniciId != null)
                {
                    int? kulId = Convert.ToInt32(kullaniciId);
                    return kullaniciYonetimi.KullaniciMerkezdeMi(kulId);
                }
                else
                {
                    return false;
                }
            }
            else return null;
        }

        public static int? KullaniciSehir()
        {
            if (HttpContext.Current.Session["KullaniciId"] != null)
            {
                String kullaniciId = HttpContext.Current.Session["KullaniciId"].ToString();
                if (kullaniciId != null)
                {
                    int? kulId = Convert.ToInt32(kullaniciId);
                    return kullaniciYonetimi.KullaniciSehirGetir(kulId);
                }
                else
                {
                    return -1;
                }
            }
            else return -1;
        }

        public static bool? KullaniciBagisciMi()
        {
            if (HttpContext.Current.Session["KullaniciId"] != null)
            {
                String kullaniciId = HttpContext.Current.Session["KullaniciId"].ToString();
                if (kullaniciId != null)
                {
                    int? kulId = Convert.ToInt32(kullaniciId);
                    return kullaniciYonetimi.BagisciMi(kulId);
                }
                else
                {
                    return false;
                }
            }
            else return null;
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

        public static bool SessionSil()
        {
            if (HttpContext.Current.Session["KullaniciId"] != null)
            {
                HttpContext.Current.Session.Remove("KullaniciId");
                return true;
            }
            else return true;
        }
    }
}