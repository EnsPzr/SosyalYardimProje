using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BusinessLayer.Models;
using BusinessLayer.Models.KullaniciModelleri;
using SosyalYardimProje.Filters;

namespace SosyalYardimProje.Controllers
{
    public class GirisController : Controller
    {
        private BusinessLayer.KullaniciYonetimi kullaniciYonetimi = new BusinessLayer.KullaniciYonetimi();
        public PartialViewResult Navbar()
        {
            String guId = "FFC81558-FFD2-4BB5-AA2F-BC9AA6BE0808";
            List<NavbarModel> navbarListModel = kullaniciYonetimi.NavbarOlustur(KullaniciBilgileriDondur.KullaniciGuId());
            return PartialView("navbarPartial", navbarListModel);
        }
        public ActionResult Giris()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Giris(KullaniciGirisModel girisModel)
        {
            if (ModelState.IsValid)
            {
                var KullaniciGuId = kullaniciYonetimi.KullaniciBul(girisModel.EPosta, girisModel.Sifre);
                if (!(KullaniciGuId.Count()==0))
                {
                    Session["KullaniciGuId"] = KullaniciGuId;
                    var Kullanici = kullaniciYonetimi.LoginKullaniciBul(KullaniciBilgileriDondur.KullaniciGuId());
                    Session["Bilgi"] = Kullanici.KullaniciAdi + " " + Kullanici.KullaniciSoyadi;
                    return RedirectToAction("AnaSayfa", "Giris");
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

        [Route("~/Anasayfa")]
        [KullaniciLoginFilter]
        public ActionResult AnaSayfa()
        {
            return View();
        }
    }
}