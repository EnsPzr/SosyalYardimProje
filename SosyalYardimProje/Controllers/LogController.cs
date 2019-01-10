using SosyalYardimProje.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BusinessLayer.Siniflar;

namespace SosyalYardimProje.Controllers
{
    [HataFiltresi]
    public class LogController : Controller
    {
        private Log logBAL = new Log();
        [KullaniciLoginFilter]
        public ActionResult Liste()
        {
            KullaniciBilgileriDondur.LogKaydet(0, "Log Listesi Görüntülendi.");
            return View(logBAL.TumLoglariGetir(KullaniciBilgileriDondur.KullaniciId()));
        }
    }
}