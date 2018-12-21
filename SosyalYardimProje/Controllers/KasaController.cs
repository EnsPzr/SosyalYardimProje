using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using BusinessLayer;
using BusinessLayer.Models.KasaModelleri;
using BusinessLayer.Siniflar;

namespace SosyalYardimProje.Controllers
{
    public class KasaController : Controller
    {
        private Kasa kasaBAL = new Kasa();
        private Kullanici kullaniciBAL = new Kullanici();
        public ActionResult Liste()
        {
            Tanimla();
            return View();
        }

        [HttpGet]
        public JsonResult TumKasaGetir()
        {
            KasaJsModel model = new KasaJsModel()
            {
                BasariliMi = true,
                KasaList = kasaBAL.TumKasaGetir(KullaniciBilgileriDondur.KullaniciId())
            };
            model.KasaSayisi = model.KasaList.Count;
            Thread.Sleep(2000);
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult FiltreliKasaGetir(string aranan, string tarih, int? sehirId)
        {
            if (aranan.Equals(""))
            {
                aranan = null;
            }

            if (tarih.Equals(""))
            {
                tarih = null;
            }
            else
            {
                try
                {
                    Convert.ToDateTime(tarih);
                }
                catch (Exception)
                {
                    tarih = null;
                }
            }
            KasaJsModel model = new KasaJsModel()
            {
                BasariliMi = true,
                KasaList = kasaBAL.FiltreliKasaGetir(KullaniciBilgileriDondur.KullaniciId(), aranan, tarih, sehirId)
            };
            model.KasaSayisi = model.KasaList.Count;
            Thread.Sleep(2000);
            return Json(model, JsonRequestBehavior.AllowGet);
        }
        public void Tanimla()
        {
            var sehirler = kullaniciBAL.SehirleriGetir(KullaniciBilgileriDondur.KullaniciId()).Select(p =>
                new SelectListItem()
                {
                    Text = p.SehirAdi,
                    Value = p.SehirId.ToString()
                }).ToList();
            ViewBag.sehirlerSelect = sehirler;
        }
    }
}