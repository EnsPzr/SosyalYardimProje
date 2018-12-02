using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BusinessLayer.Siniflar;
namespace SosyalYardimProje.Controllers
{
    public class IhtiyacSahibiController : Controller
    {
        private IhtiyacSahibi ihtiyacSahibiBAL= new IhtiyacSahibi();
        public ActionResult Liste()
        {
            return View(ihtiyacSahibiBAL.TumIhtiyacSahipleriniGetir(KullaniciBilgileriDondur.KullaniciId()));
        }
    }
}