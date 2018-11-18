using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BusinessLayer.Models;

namespace SosyalYardimProje.Controllers
{
    public class GirisController : Controller
    {
        private BusinessLayer.KullaniciYonetimi kullaniciYonetimi = new BusinessLayer.KullaniciYonetimi();
        public PartialViewResult Navbar()
        {
            List<NavbarModel> navbarListModel = kullaniciYonetimi.NavbarOlustur(KullaniciBilgileriDondur.KullaniciGuId());
            return PartialView("navbarPartial", navbarListModel);
        }

        public ActionResult Giris()
        {
            return View();
        }
    }
}