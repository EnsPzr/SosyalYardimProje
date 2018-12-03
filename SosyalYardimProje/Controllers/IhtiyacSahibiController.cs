﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using BusinessLayer;
using BusinessLayer.Models.IhtiyacSahibiModelleri;
using BusinessLayer.Siniflar;
using SosyalYardimProje.Filters;

namespace SosyalYardimProje.Controllers
{
    public class IhtiyacSahibiController : Controller
    {
        private IhtiyacSahibi ihtiyacSahibiBAL= new IhtiyacSahibi();
        private Kullanici kullaniciBusinessLayer = new Kullanici();
        [KullaniciLoginFilter]
        public ActionResult Liste()
        {
            Tanimla();
            return View(new IhtiyacSahibiModel());
        }
        [SadeceLoginFilter]
        public JsonResult IhtiyacSahipleriniGetir()
        {
            IhtiyacSahibiJSModel ihtiyacSahibiModel = new IhtiyacSahibiJSModel();
            ihtiyacSahibiModel.IhtiyacSahipleri =
                ihtiyacSahibiBAL.TumIhtiyacSahipleriniGetir(KullaniciBilgileriDondur.KullaniciId());
            ihtiyacSahibiModel.BasariliMi = true;
            ihtiyacSahibiModel.IhtiyacSahibiSayisi = ihtiyacSahibiModel.IhtiyacSahipleri.Count;
            Thread.Sleep(2000);
            return Json(ihtiyacSahibiModel, JsonRequestBehavior.AllowGet);
        }
        [SadeceLoginFilter]
        public JsonResult FiltreliIhtiyacSahipleriniGetir(String IhtiyacSahibiAranan, int? SehirId)
        {
            IhtiyacSahibiJSModel ihtiyacSahibiModel = new IhtiyacSahibiJSModel();
            ihtiyacSahibiModel.IhtiyacSahipleri =
                ihtiyacSahibiBAL.FiltreliIhtiyacSahibiListesiniGetir(IhtiyacSahibiAranan, SehirId, KullaniciBilgileriDondur.KullaniciId());
            ihtiyacSahibiModel.BasariliMi = true;
            ihtiyacSahibiModel.IhtiyacSahibiSayisi = ihtiyacSahibiModel.IhtiyacSahipleri.Count;
            Thread.Sleep(2000);
            return Json(ihtiyacSahibiModel, JsonRequestBehavior.AllowGet);
        }
        [KullaniciLoginFilter]
        public ActionResult Ekle()
        {
            Tanimla();
            return View();
        }

        [KullaniciLoginFilter]
        [HttpPost]
        public ActionResult Ekle(IhtiyacSahibiModel yeniIhtiyacSahibi)
        {
            if (ModelState.IsValid)
            {
                var onay = ihtiyacSahibiBAL.IhtiyacSahibiKaydet(yeniIhtiyacSahibi);
                if (onay.TamamlandiMi==true)
                {
                    TempData["uyari"] = "İhtiyaç sahibi ekleme işlemi başarı ile tamamlandı.";
                    return RedirectToAction("Liste");
                }
                else
                {
                    string hata = "";
                    foreach (var hataItem in onay.HataMesajlari)
                    {
                        hata = hata + "" + hataItem + "\n";
                    }

                    TempData["hata"] = hata;
                    Tanimla();
                    return View(yeniIhtiyacSahibi);
                }
            }
            else
            {
                Tanimla();
                return View(yeniIhtiyacSahibi);
            }
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