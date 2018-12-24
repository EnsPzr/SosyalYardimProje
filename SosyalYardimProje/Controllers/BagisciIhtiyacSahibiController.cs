using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BusinessLayer.Siniflar;

namespace SosyalYardimProje.Controllers
{
    public class BagisciIhtiyacSahibiController : Controller
    {
        private IhtiyacSahibi ihtiyacSahibiBAL = new IhtiyacSahibi();
        public ActionResult Liste()
        {
            var ihtiyacSahipleri = ihtiyacSahibiBAL.TumIhtiyacSahipleriniGetir(BagisciBilgileriDondur.KullaniciId());
            return View(ihtiyacSahipleri);
        }
    }
}