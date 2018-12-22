using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using BusinessLayer;
using BusinessLayer.Models.GeriBildirimModelleri;
using BusinessLayer.Siniflar;

namespace SosyalYardimProje.Controllers
{
    public class GeriBildirimController : Controller
    {
        private Kullanici kullaniciBAL = new Kullanici();
        private GeriBildirim geriBildirimBAL = new GeriBildirim();
        public ActionResult Liste()
        {
            Tanimla();
            return View();
        }

        [HttpGet]
        public JsonResult TumGeriBildirimleriGetir()
        {
            GeriBildirimJsModel model = new GeriBildirimJsModel()
            {
                BasariliMi = true,
                GeriBildirimList = geriBildirimBAL.TumGeriBildirimleriGetir(KullaniciBilgileriDondur.KullaniciId())
            };
            model.GeriBildirimSayisi = model.GeriBildirimList.Count;
            Thread.Sleep(2000);
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult FiltreliGeriBildirimleriGetir(string aranan, string tarih, int? sehirId)
        {
            if (aranan.Equals(""))
            {
                aranan = null;
            }

            if (tarih.Equals(""))
            {
                tarih = null;
            }
            GeriBildirimJsModel model = new GeriBildirimJsModel()
            {
                BasariliMi = true,
                GeriBildirimList = geriBildirimBAL.FiltreliGeriBildirimleriGetir(KullaniciBilgileriDondur.KullaniciId(), aranan, tarih, sehirId)
            };
            model.GeriBildirimSayisi = model.GeriBildirimList.Count;
            Thread.Sleep(2000);
            return Json(model, JsonRequestBehavior.AllowGet);
        }
        public void Tanimla()
        {
            var sehirlerSelect = kullaniciBAL.SehirleriGetir(KullaniciBilgileriDondur.KullaniciId()).Select(p =>
                new SelectListItem()
                {
                    Text = p.SehirAdi,
                    Value = p.SehirId.ToString()
                }).ToList();
            ViewBag.sehirlerSelect = sehirlerSelect;
        }
    }
}