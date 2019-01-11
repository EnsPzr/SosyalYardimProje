using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using BusinessLayer.Models;
using BusinessLayer.Models.KullaniciModelleri;
using SosyalYardimProje.Filters;

namespace SosyalYardimProje.Controllers
{
    [HataFiltresi]
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
                    var Kullanici = kullaniciYonetimi.LoginKullaniciModelBul(Convert.ToInt32(KullaniciId));
                    if (Convert.ToBoolean(Kullanici.AktifMi))
                    {
                        Session["KullaniciId"] = KullaniciId;
                        Session["Bilgi"] = Kullanici.KullaniciAdi + " " + Kullanici.KullaniciSoyadi;
                        KullaniciBilgileriDondur.LogKaydet(6, "Koordinatör Girişi Yapıldı.");
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
            if (KullaniciBilgileriDondur.KullaniciBagisciMi()==true)
            {
                return RedirectToAction("AnaSayfa", "BagisciIslemleri");
            }
            else
            {
                return View();
            }
        }

        [ChildActionOnly]
        public PartialViewResult anasayfaPartial()
        {
            var Kullanici = KullaniciBilgileriDondur.KullaniciId();
            var anaSayfaModel = kullaniciYonetimi.AnaSayfaModeli(Kullanici);
            return PartialView(anaSayfaModel);
        }

        public ActionResult Error()
        {
            if (TempData["error"] == null)
                return RedirectToAction("Giris", "Giris");

            Exception model = TempData["error"] as Exception;
            KullaniciBilgileriDondur.LogKaydet(7, "Sistemde hata oluştu=>"+model.Message);
            return View(model);
        }

        [SadeceLoginFilter]
        public ActionResult YetkiYok()
        {
            return View();
        }

        public ActionResult Cikis()
        {
            var sonuc = KullaniciBilgileriDondur.KullaniciBagisciMi();
            if (sonuc == true)
            {
                KullaniciBilgileriDondur.SessionSil();
                return RedirectToAction("Giris", "BagisciIslemleri");
            }
            else
            {
                KullaniciBilgileriDondur.SessionSil();
                return RedirectToAction("Giris", "Giris");
            }
            
        }
    }
}