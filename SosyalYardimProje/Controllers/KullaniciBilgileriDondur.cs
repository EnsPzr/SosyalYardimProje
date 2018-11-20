using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SosyalYardimProje.Controllers
{
    public static class KullaniciBilgileriDondur
    {
        public static int? KullaniciId()
        {
            if (HttpContext.Current.Session["KullaniciId"] != null)
            {
                String kullaniciId= HttpContext.Current.Session["KullaniciId"].ToString();
                return Convert.ToInt32(kullaniciId);
            }
            else return null;
        }
    }
}