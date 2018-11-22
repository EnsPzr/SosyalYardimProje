using SosyalYardimProje.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BusinessLayer.Models.KullaniciModelleri;
using BusinessLayer.Siniflar;
namespace SosyalYardimProje.Controllers
{
    public class YetkiController : Controller
    {
        private Yetki yetkiBAL = new Yetki();
        [KullaniciLoginFilter]
        [HttpGet]
        public ActionResult Liste()
        {
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
            return Json(kullanicilar, JsonRequestBehavior.AllowGet);
        }
    }
}