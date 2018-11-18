using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BusinessLayer.Models;
using BusinessLayer.Models.KullaniciModelleri;

namespace SosyalYardimProje.Controllers
{
    public class GirisController : Controller
    {
        private BusinessLayer.KullaniciYonetimi kullaniciYonetimi = new BusinessLayer.KullaniciYonetimi();
        public PartialViewResult Navbar()
        {
            String guId = "FFC81558-FFD2-4BB5-AA2F-BC9AA6BE0808";
            List<NavbarModel> navbarListModel = kullaniciYonetimi.NavbarOlustur(guId);
            return PartialView("navbarPartial", navbarListModel);
        }
        [Route("Giris")]
        public ActionResult Giris()
        {
            return View();
        }

        [Route("Giris")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Giris(KullaniciGirisModel girisModel)
        {
            if (ModelState.IsValid)
            {
                var KullaniciGuId = kullaniciYonetimi.KullaniciBul(girisModel.EPosta, girisModel.Sifre);
                if (KullaniciGuId != null)
                {

                }
                else
                {
                    ModelState.AddModelError("","E Posta veya Şifre Hatalı.");
                    return View(girisModel);
                }
            }
            else
            {
                return View(girisModel);
            }
        }
    }
}