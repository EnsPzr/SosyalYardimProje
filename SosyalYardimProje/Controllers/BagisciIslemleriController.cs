using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BusinessLayer.BagisciSiniflar;
using BusinessLayer.Models.BagisciGiris;

namespace SosyalYardimProje.Controllers
{
    public class BagisciIslemleriController : Controller
    {
        private BagisciYonetimi bagisciBAL = new BagisciYonetimi();
        public ActionResult Giris()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Giris(BagisciGirisModel model)
        {
            if (ModelState.IsValid)
            {
                var bagisci = bagisciBAL.BagisciBul(model.Eposta, model.Sifre);
                if (bagisci != null)
                {
                    Session["KullaniciId"] = bagisci.KullaniciId;
                    Session["Bilgi"] = bagisci.KullaniciAdi + " " + bagisci.KullaniciSoyadi;
                    return RedirectToAction("BagisciAnaSayfa");
                }
                else
                {
                    ModelState.AddModelError("","E Posta veya Şifre hatalı.");
                    return View(model);
                }
            }
            else
            {
                return View(model);
            }
        }
    }
}