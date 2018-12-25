using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BusinessLayer;
using DataLayer;
using SosyalYardimProje.Filters;
using KullaniciYonetimi = BusinessLayer.KullaniciYonetimi;

namespace SosyalYardimProje.Controllers
{
    [HataFiltresi]
    public static class KullaniciBilgileriDondur
    {
        private static BusinessLayer.KullaniciYonetimi kullaniciYonetimi = new BusinessLayer.KullaniciYonetimi();
        public static int? KullaniciId()
        {
            if (HttpContext.Current.Session["KullaniciId"] != null)
            {
                String kullaniciId = HttpContext.Current.Session["KullaniciId"].ToString();
                return Convert.ToInt32(kullaniciId);
            }
            else return null;
        }

        public static KullaniciBilgileriTablo KullaniciBilgileriGetir()
        {
            if (HttpContext.Current.Session["KullaniciId"] != null)
            {
                String kullaniciIdStr = HttpContext.Current.Session["KullaniciId"].ToString();
                int? KullaniciId = Convert.ToInt32(kullaniciIdStr);
                return kullaniciYonetimi.LoginKullaniciBul(KullaniciId);
            }
            else return kullaniciYonetimi.LoginKullaniciBul(1);
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