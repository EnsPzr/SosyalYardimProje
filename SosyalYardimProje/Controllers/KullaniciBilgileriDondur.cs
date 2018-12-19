﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BusinessLayer;
using DataLayer;
using KullaniciYonetimi = BusinessLayer.KullaniciYonetimi;

namespace SosyalYardimProje.Controllers
{
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
            else return 1;
        }

        public static KullaniciBilgileriTablo KullaniciBilgileriGetir()
        {
            if (HttpContext.Current.Session["KullaniciId"] != null)
            {
                String kullaniciIdStr = HttpContext.Current.Session["KullaniciId"].ToString();
                int? KullaniciId = Convert.ToInt32(kullaniciIdStr);
                return kullaniciYonetimi.LoginKullaniciBul(KullaniciId);
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
    }
}