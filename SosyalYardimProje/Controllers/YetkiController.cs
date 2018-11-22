using SosyalYardimProje.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BusinessLayer.Models.KullaniciModelleri;
using BusinessLayer.Siniflar;
using System.Threading;

namespace SosyalYardimProje.Controllers
{
    public class YetkiController : Controller
    {
        private Yetki yetkiBAL = new Yetki();
        private Kullanici kullaniciBAL = new Kullanici();
        [KullaniciLoginFilter]
        [HttpGet]
        public ActionResult Liste()
        {
            Tanimla();
            return View();
        }

        [SadeceLoginFilter]
        [HttpGet]
        public JsonResult KullanicilariGetir()
        {
            KullaniciJSModel kullanicilar = new KullaniciJSModel();
            kullanicilar.KullaniciModelList = yetkiBAL.KullanicilariGetir(KullaniciBilgileriDondur.KullaniciId());
            kullanicilar.BasariliMi = true;
            kullanicilar.KullaniciSayisi = kullanicilar.KullaniciModelList.Count;
            Thread.Sleep(2000);
            return Json(kullanicilar, JsonRequestBehavior.AllowGet);
        }

        [SadeceLoginFilter]
        [HttpGet]
        public JsonResult FiltreliKullanicilariGetir(String aranan, int? sehirId)
        {
            KullaniciJSModel kullanicilar = new KullaniciJSModel();
            kullanicilar.KullaniciModelList =
                yetkiBAL.FiltreliKullanicilariGetir(aranan, sehirId, KullaniciBilgileriDondur.KullaniciId());
            kullanicilar.KullaniciSayisi = kullanicilar.KullaniciModelList.Count;
            kullanicilar.BasariliMi = true;
            Thread.Sleep(2000);
            return Json(kullanicilar, JsonRequestBehavior.AllowGet);
        }

        public void Tanimla()
        {
            var sehirler = kullaniciBAL.SehirleriGetir(KullaniciBilgileriDondur.KullaniciId()).Select(p =>
                new SelectListItem()
                {
                    Text = p.SehirAdi,
                    Value = p.SehirId.ToString()
                }).ToList();
            ViewBag.sehirler = sehirler;
        }
    }
}