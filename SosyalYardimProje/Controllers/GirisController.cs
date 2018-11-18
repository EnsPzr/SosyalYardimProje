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
            String guId = KullaniciBilgileriDondur.KullaniciGuId();
            List<NavbarModel> navbarListModel = kullaniciYonetimi.NavbarOlustur("FFC81558-FFD2-4BB5-AA2F-BC9AA6BE0808");
            return PartialView("navbarPartial", navbarListModel);
        }

        public ActionResult Giris()
        {
            return View();
        }
    }
}