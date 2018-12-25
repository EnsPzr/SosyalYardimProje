using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using BusinessLayer.Models;
using BusinessLayer.Models.KullaniciModelleri;
using SosyalYardimProje.Filters;

namespace SosyalYardimProje.Controllers
{
    public class GirisController : Controller
    {
        private BusinessLayer.KullaniciYonetimi kullaniciYonetimi = new BusinessLayer.KullaniciYonetimi();
        [ChildActionOnly]
        public PartialViewResult Navbar()
        {
            List<NavbarModel> navbarListModel = kullaniciYonetimi.NavbarOlustur(KullaniciBilgileriDondur.KullaniciId());
            return PartialView("navbarPartial", navbarListModel);
        }
        [HttpGet]
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
                var KullaniciId = kullaniciYonetimi.KullaniciBul(girisModel.EPosta, girisModel.Sifre);
                if (!(KullaniciId.Count()==0))
                {
                    var Kullanici = kullaniciYonetimi.LoginKullaniciBul(Convert.ToInt32(KullaniciId));
                    if (Convert.ToBoolean(Kullanici.AktifMi))
                    {
                        Session["KullaniciId"] = KullaniciId;
                        Session["Bilgi"] = Kullanici.KullaniciAdi + " " + Kullanici.KullaniciSoyadi;
                        return RedirectToAction("AnaSayfa", "Giris");
                    }
                    else
                    {
                        TempData["hata"] = "Kullanıcı Aktif Değil. İl görevliniz ile iletişime geçiniz.";
                        return RedirectToAction("Giris", "Giris");
                    }
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
        [HttpGet]
        [KullaniciLoginFilter]
        public ActionResult AnaSayfa()
        {
            return View();
        }

        [ChildActionOnly]
        public PartialViewResult anasayfaPartial()
        {
            var Kullanici = kullaniciYonetimi.LoginKullaniciBul(KullaniciBilgileriDondur.KullaniciId());
            var anaSayfaModel = kullaniciYonetimi.AnaSayfaModeli(Kullanici.SehirTablo_SehirId);
            return PartialView(anaSayfaModel);
        }
    }
}