using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BusinessLayer.Models.IhtiyacSahibiModelleri;
using BusinessLayer.Siniflar;

namespace SosyalYardimProje.Controllers
{
    public class BagisciIhtiyacSahibiController : Controller
    {
        private IhtiyacSahibi ihtiyacSahibiBAL = new IhtiyacSahibi();
        private Kullanici kullaniciBAL = new Kullanici();
        public ActionResult Liste()
        {
            var ihtiyacSahipleri = ihtiyacSahibiBAL.TumIhtiyacSahipleriniGetir(BagisciBilgileriDondur.KullaniciId());
            return View(ihtiyacSahipleri);
        }

        public ActionResult Ekle()
        {
            Tanimla();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Ekle(IhtiyacSahibiModel model)
        {
            if (ModelState.IsValid)
            {
                var onay = ihtiyacSahibiBAL.IhtiyacSahibiKaydet(model,BagisciBilgileriDondur.KullaniciId());
                if (onay.TamamlandiMi == true)
                {
                    TempData["uyari"] = "İhtiyaç sahibi ekleme işlemi başarı ile tamamlandı.";
                    return RedirectToAction("Liste");
                }
                else
                {
                    string hatalar = KullaniciBilgileriDondur.HataMesajlariniOku(onay.HataMesajlari);
                    TempData["hata"] = hatalar;
                    Tanimla();
                    return View(model);
                }
            }
            else
            {
                Tanimla();
                return View(model);
            }
        }

        public void Tanimla()
        {
            var sehirlerSelectList = kullaniciBAL.TumSehirleriGetir().Select(p => new SelectListItem()
            {
                Text = p.SehirAdi,
                Value = p.SehirId.ToString()
            }).ToList();
            ViewBag.sehirlerSelectList = sehirlerSelectList;
        }
    }
}