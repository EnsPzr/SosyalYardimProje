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

        public ActionResult Duzenle(int? id)
        {
            if (id != null)
            {
                if (ihtiyacSahibiBAL.IhtiyacSahibiGoruntulenebilirMi(id, BagisciBilgileriDondur.KullaniciId()))
                {
                    var ihtiyacSahibi = ihtiyacSahibiBAL.IhtiyacSahibiGetir(id);
                    if (ihtiyacSahibi != null)
                    {
                        Tanimla();
                        return View(ihtiyacSahibi);
                    }
                    else
                    {
                        TempData["hata"] = "İhtiyaç sahibi bulunamadı.";
                        return RedirectToAction("Liste");
                    }
                }
                else
                {
                    TempData["hata"] = "Sadece kendi eklediğiniz ihtiyaç sahipleri ile ilgili işlem yapabilirisiniz.";
                    return RedirectToAction("Liste");
                }
            }
            else
            {
                TempData["hata"] = "Lütfen güncellemek istediğiniz ihtiyaç sahibini seçiniz.";
                return RedirectToAction("Liste");
            }
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Duzenle(IhtiyacSahibiModel duzenlenmisIhtiyacSahibi)
        {
            if (ModelState.IsValid)
            {
                if (ihtiyacSahibiBAL.IhtiyacSahibiGoruntulenebilirMi(duzenlenmisIhtiyacSahibi.IhtiyacSahibiId, BagisciBilgileriDondur.KullaniciId()))
                {
                    var ihtiyacSahibi = ihtiyacSahibiBAL.IhtiyacSahibiGetir(duzenlenmisIhtiyacSahibi.IhtiyacSahibiId);
                    if (ihtiyacSahibi != null)
                    {
                        var onay = ihtiyacSahibiBAL.IhtiyacSahibiGuncelle(duzenlenmisIhtiyacSahibi);
                        if (onay.TamamlandiMi == true)
                        {
                            TempData["uyari"] = "İhtiyaç sahibi güncelleme işlemi başarı ile sonuçlandı";
                            return RedirectToAction("Liste");
                        }
                        else
                        {
                            string hata = BagisciBilgileriDondur.HataMesajlariniOku(onay.HataMesajlari);
                            TempData["hata"] = hata;
                            Tanimla();
                            return View(duzenlenmisIhtiyacSahibi);
                        }
                    }
                    else
                    {
                        TempData["hata"] =
                            "Güncellemek istediğiniz ihtiyaç sahibi bulunamadı.";
                        return RedirectToAction("Liste");
                    }
                }
                else
                {
                    TempData["hata"] =
                        "Sadece kendi eklediğiniz ihtiyaç sahipleri ile ilgili işlem yapabilirsiniz.";
                    return RedirectToAction("Liste");
                }
            }
            else
            {
                Tanimla();
                return View(duzenlenmisIhtiyacSahibi);
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