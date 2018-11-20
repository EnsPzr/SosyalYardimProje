using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using BusinessLayer.Models.KullaniciModelleri;
using BusinessLayer.Siniflar;
using SosyalYardimProje.Filters;

namespace SosyalYardimProje.Controllers
{

    public class KullaniciController : Controller
    {
        Kullanici kullaniciBusinessLayer = new Kullanici();
        [KullaniciLoginFilter]
        public ActionResult Liste()
        {
            Tanimla();
            return View(new KullaniciModel());
        }
        [SadeceLoginFilter]
        [HttpGet]
        public JsonResult KullanicilariGetir()
        {
            var jsKullaniciModel = new KullaniciJSModel()
            {
                KullaniciModelList =
                    kullaniciBusinessLayer.TumKullanicilariGetir(KullaniciBilgileriDondur.KullaniciId()),
                BasariliMi = true
            };
            jsKullaniciModel.KullaniciSayisi = jsKullaniciModel.KullaniciModelList.Count;
            Thread.Sleep(2000);
            return Json(jsKullaniciModel, JsonRequestBehavior.AllowGet);
        }

        [SadeceLoginFilter]
        [HttpGet]
        public JsonResult FiltreliKullanicilariGetir(KullaniciFiltrelemeModel filtreliKullaniciModel)
        {
            var jsKullaniciModel = new KullaniciJSModel()
            {
                KullaniciModelList =
                    kullaniciBusinessLayer.TumKullanicilariGetir(KullaniciBilgileriDondur.KullaniciId()),
                BasariliMi = true
            };
            jsKullaniciModel.KullaniciSayisi = jsKullaniciModel.KullaniciModelList.Count;
            Thread.Sleep(2000);
            return Json(jsKullaniciModel, JsonRequestBehavior.AllowGet);
        }
        public void Tanimla()
        {
            var sehirler = kullaniciBusinessLayer.SehirleriGetir(KullaniciBilgileriDondur.KullaniciId()).Select(p =>
                new SelectListItem()
                {
                    Text = p.SehirAdi,
                    Value = p.SehirId.ToString()
                }).ToList();
            ViewBag.sehirler = sehirler;
        }
    }
}