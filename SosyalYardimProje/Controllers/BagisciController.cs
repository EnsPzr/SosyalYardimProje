using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BusinessLayer.Models.BagisciModelleri;
using BusinessLayer.Siniflar;

namespace SosyalYardimProje.Controllers
{
    public class BagisciController : Controller
    {
        private Bagisci bagisciBAL = new Bagisci();
        public ActionResult Liste()
        {
            return View();
        }

        [HttpGet]
        public JsonResult TumBagiscilariGetir()
        {
            BagisciJSModel model = new BagisciJSModel()
            {
                BagisciList = bagisciBAL.TumBagiscilariGetir(KullaniciBilgileriDondur.KullaniciId()),
                BasariliMi = true,
                BagisciSayisi = bagisciBAL.TumBagiscilariGetir(KullaniciBilgileriDondur.KullaniciId()).Count
            };
            return Json(model, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult FiltreliBagiscilariGetir(int? sehirId, string aranan)
        {
            BagisciJSModel model = new BagisciJSModel()
            {
                BagisciList = bagisciBAL.FiltreliBagiscilariGetir(KullaniciBilgileriDondur.KullaniciId(), sehirId, aranan),
                BasariliMi = true,
                BagisciSayisi = bagisciBAL.FiltreliBagiscilariGetir(KullaniciBilgileriDondur.KullaniciId(), sehirId, aranan).Count
            };
            return Json(model, JsonRequestBehavior.AllowGet);
        }
    }
}