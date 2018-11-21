using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using BusinessLayer.Models.SubeModelleri;
using BusinessLayer.Siniflar;
using SosyalYardimProje.Filters;

namespace SosyalYardimProje.Controllers
{
    public class SubeController : Controller
    {
        Sube subeBusinessLayer = new Sube();
        private Kullanici kullaniciBL = new Kullanici();
        [KullaniciLoginFilter]
        [HttpGet]
        public ActionResult Liste()
        {
            return View();
        }

        [HttpGet]
        [SadeceLoginFilter]
        public JsonResult SubeleriGetir(string aranan)
        {
            SubeJSModel jsModel = new SubeJSModel();
            var subeler = subeBusinessLayer.TumSubeleriGetir();
            if (aranan != null)
            {
                subeler = subeBusinessLayer.FiltreliSubeleriGetir(aranan);
            }
            else
            {
                subeler = subeBusinessLayer.TumSubeleriGetir();
            }
            jsModel.SubeModelList = subeler;
            jsModel.BasariliMi = true;
            jsModel.SubeSayisi = subeler.Count;
            Thread.Sleep(2000);
            return Json(jsModel, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [KullaniciLoginFilter]
        public ActionResult Ekle()
        {
            Tanimla();
            return View();
        }

        [HttpPost]
        [KullaniciLoginFilter]
        [ValidateAntiForgeryToken]
        public ActionResult Ekle(SubeModel yeniSube)
        {
            if (ModelState.IsValid)
            {
                bool sehirGorevlisiVarMi = subeBusinessLayer.sehirGorevlisiVarMi(yeniSube.Sehir.SehirId);
                if (!sehirGorevlisiVarMi)
                {
                    if (subeBusinessLayer.SubeEkle(yeniSube))
                    {
                        TempData["uyari"] = "Şube ekleme işlemi başarı ile tamamlandı.";
                        return RedirectToAction("Liste");
                    }
                    else
                    {
                        TempData["hata"] = "Bilinmeyen bir hata oluştu.";
                        Tanimla();
                        return View(yeniSube);
                    }
                }
                else
                {
                    TempData["hata"] = "Şehir için zaten bir görevli seçimi yapılmış.";
                    Tanimla();
                    return View(yeniSube);
                }
            }
            else
            {
                Tanimla();
                return View(yeniSube);
            }
        }

        public void Tanimla()
        {
            var sehirler = kullaniciBL.SehirleriGetir(KullaniciBilgileriDondur.KullaniciId()).Select(p => new SelectListItem()
            {
                Text=p.SehirAdi,
                Value=p.SehirId.ToString()
            }).ToList();
            ViewBag.sehirler = sehirler;
            var kullanicilar = kullaniciBL.TumKullanicilariGetir(KullaniciBilgileriDondur.KullaniciId()).Select(p => new SelectListItem()
            {
                Value=p.KullaniciId.ToString(),
                Text=p.KullaniciAdi+" "+p.KullaniciSoyadi
            }).ToList();
            ViewBag.kullanicilar = kullanicilar;
        }
    }
}