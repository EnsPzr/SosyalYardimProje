﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using BusinessLayer;
using BusinessLayer.Models;
using BusinessLayer.Models.DepoModelleri;
using BusinessLayer.Siniflar;

namespace SosyalYardimProje.Controllers
{
    public class DepoController : Controller
    {
        private Esya esyaBAL = new Esya();
        private Depo depoBAL = new Depo();
        private Kullanici kullaniciBAL = new Kullanici();
        public ActionResult Liste()
        {
            Tanimla();
            return View();
        }

        public JsonResult TumunuGetir()
        {
            var depoEsyalari = depoBAL.DepoGetir(KullaniciBilgileriDondur.KullaniciId());
            DepoJsModel depoJs = new DepoJsModel()
            {
                BasariliMi = true,
                DepoList = depoEsyalari
            };
            depoJs.DepoEsyaSayisi = depoJs.DepoList.Count;
            Thread.Sleep(2000);
            return Json(depoJs, JsonRequestBehavior.AllowGet);
        }

        public JsonResult FiltreliDepoGetir(int? esyaId, int? sehirId, String aranan)
        {
            var depoEsyalari = depoBAL.FiltreliDepoGetir(KullaniciBilgileriDondur.KullaniciId(), esyaId, sehirId, aranan);
            DepoJsModel depoJs = new DepoJsModel()
            {
                BasariliMi = true,
                DepoList = depoEsyalari
            };
            depoJs.DepoEsyaSayisi = depoJs.DepoList.Count;
            Thread.Sleep(2000);
            return Json(depoJs, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Ekle()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Ekle(DepoModel eklenecekEsya)
        {
            var sonuc = depoBAL.DepoyaEsyaEkle(eklenecekEsya,KullaniciBilgileriDondur.KullaniciId());
            if (sonuc.TamamlandiMi == true)
            {
                TempData["uyari"] = "Depoya eşya başarı ile eklendi.";
                return RedirectToAction("Liste");
            }
            else
            {
                String hatalar = KullaniciBilgileriDondur.HataMesajlariniOku(sonuc.HataMesajlari);
                TempData["hata"] = hatalar;
                return View(eklenecekEsya);
            }
        }
        public void Tanimla()
        {
            var esyalarSelect = esyaBAL.TumEsyalariGetir().Select(p => new SelectListItem()
            {
                Text = p.EsyaAdi,
                Value = p.EsyaId.ToString()
            }).ToList();
            ViewBag.esyalarSelect = esyalarSelect;
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