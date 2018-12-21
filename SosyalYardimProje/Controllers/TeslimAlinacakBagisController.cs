using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using BusinessLayer;
using BusinessLayer.Models.TeslimAlinacakBagis;
using BusinessLayer.Siniflar;

namespace SosyalYardimProje.Controllers
{
    public class TeslimAlinacakBagisController : Controller
    {
        private Kullanici kullaniciBAL = new Kullanici();
        private TeslimAlinacakBagis bagisBAL = new TeslimAlinacakBagis();
        public ActionResult Liste()
        {
            Tanimla();
            return View();
        }

        [HttpGet]
        public JsonResult TumBagiscilariGetir()
        {
            TeslimAlinacakBagisJsModel model = new TeslimAlinacakBagisJsModel()
            {
                BagisList = bagisBAL.TumBagislariGetir(KullaniciBilgileriDondur.KullaniciId()),
                BasariliMi = true,
                BagisSayisi = bagisBAL.TumBagislariGetir(KullaniciBilgileriDondur.KullaniciId()).Count
            };
            Thread.Sleep(2000);
            return Json(model, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult FiltreliBagiscilariGetir(int? sehirId, string aranan, string tarih)
        {
            if (!(tarih.Equals("")))
            {
                try
                {
                    Convert.ToDateTime(tarih);
                }
                catch (Exception e)
                {
                    tarih = null;
                }
            }
            else
            {
                tarih = null;
            }

            if ((aranan.Equals("")))
            {
                aranan = null;
            }
            TeslimAlinacakBagisJsModel model = new TeslimAlinacakBagisJsModel()
            {
                BagisList = bagisBAL.FiltreliBagislariGetir(KullaniciBilgileriDondur.KullaniciId(),sehirId,aranan,tarih),
                BasariliMi = true,
                BagisSayisi = bagisBAL.FiltreliBagislariGetir(KullaniciBilgileriDondur.KullaniciId(), sehirId, aranan, tarih).Count
            };
            Thread.Sleep(2000);
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Duzenle(int? bagisId=1)
        {
            if (bagisId != null)
            {
                if (bagisBAL.KullaniciBagisDetayiGorebilirMi(KullaniciBilgileriDondur.KullaniciId(), bagisId))
                {
                    var bagis = bagisBAL.Detay(bagisId);
                    if (bagis != null)
                    {
                        return View(bagis);
                    }
                    else
                    {
                        TempData["hata"] = "Bağış bulunamadı.";
                        return RedirectToAction("Liste");
                    }
                }
                else
                {
                    TempData["hata"] = "Bu bağışı düzenlemek için yetjniz bulunmamaktadır.";
                    return RedirectToAction("Liste");
                }
            }
            else
            {
                TempData["hata"] = "Düzenlemek için bağış seçiniz";
                return RedirectToAction("Liste");
            }
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Duzenle(TeslimAlinacakBagisModel model)
        {
            return View();
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