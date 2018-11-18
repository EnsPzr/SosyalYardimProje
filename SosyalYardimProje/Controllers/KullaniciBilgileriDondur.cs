using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SosyalYardimProje.Controllers
{
    public static class KullaniciBilgileriDondur
    {
        public static String KullaniciGuId()
        {
            if (HttpContext.Current.Session["KullaniciGuId"] != null)
                return HttpContext.Current.Session["KullaniciGuId"].ToString();
            else return null;
        }
    }
}