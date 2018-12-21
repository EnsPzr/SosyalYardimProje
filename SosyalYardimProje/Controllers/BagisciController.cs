using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using BusinessLayer.Models.BagisciModelleri;
using BusinessLayer.Siniflar;

namespace SosyalYardimProje.Controllers
{
    public class BagisciController : Controller
    {
        private Bagisci bagisciBAL = new Bagisci();
        private Kullanici kullaniciBAL = new Kullanici();
        public ActionResult Liste()
        {
            Tanimla();
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
            Thread.Sleep(2000);
            return Json(model, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult FiltreliBagiscilariGetir(int? sehirId, string aranan)
        {
            if (sehirId != null || (!(aranan.Equals(""))))
            {
                BagisciJSModel model = new BagisciJSModel()
                {
                    BagisciList = bagisciBAL.FiltreliBagiscilariGetir(KullaniciBilgileriDondur.KullaniciId(), sehirId, aranan),
                    BasariliMi = true,
                    BagisciSayisi = bagisciBAL.FiltreliBagiscilariGetir(KullaniciBilgileriDondur.KullaniciId(), sehirId, aranan).Count
                };
                Thread.Sleep(2000);
                return Json(model, JsonRequestBehavior.AllowGet);
            }
            BagisciJSModel model2 = new BagisciJSModel()
            {
                BagisciList = bagisciBAL.TumBagiscilariGetir(KullaniciBilgileriDondur.KullaniciId()),
                BasariliMi = true,
                BagisciSayisi = bagisciBAL.TumBagiscilariGetir(KullaniciBilgileriDondur.KullaniciId()).Count
            };
            Thread.Sleep(2000);
            return Json(model2, JsonRequestBehavior.AllowGet);
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