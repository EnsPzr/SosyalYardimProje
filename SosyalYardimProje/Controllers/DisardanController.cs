﻿using BusinessLayer;
using BusinessLayer.Models.DisardanIhtiyacSahibiModelleri;
using BusinessLayer.Siniflar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SosyalYardimProje.Controllers
{
    public class DisardanController : Controller
    {
        private Kullanici kullaniciBAL = new Kullanici();
        private IhtiyacSahibi ihtiyacSahibiBAL = new IhtiyacSahibi();
        public ActionResult AnaSayfa()
        {
            return View();
        }

        public ActionResult IhtiyacSahibiEkle()
        {
            Tanimla();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult IhtiyacSahibiEkle(DisardanIhtiyacSahibiModel model)
        {
            if (ModelState.IsValid)
            {
                var sonuc = ihtiyacSahibiBAL.DisardanIhtiyacSahibiKaydet(model);
                if (sonuc.TamamlandiMi == true)
                {
                    TempData["uyari"] = "Teşekkür ederiz. Ekiplerimiz tarafında ihtiyaç sahibi ziyaret edilecektir. Girmiş olduğunuz bilgiler doğrultusunda sizin için hesap açılmıştır. Hesabınıza girip eşya bağışı yapabilir ve eklediğiniz ihtiyaç sahiplerini görebilirsiniz.";
                    return RedirectToAction("AnaSayfa");
                }
                else
                {
                    String hatalar = KullaniciBilgileriDondur.HataMesajlariniOku(sonuc.HataMesajlari);
                    Tanimla();
                    return View(model);
                }
            }
            else
            {
                Tanimla();
                return View(model);
            }
        }

        public void Tanimla()
        {
            var sehirlerSelectList = kullaniciBAL.TumSehirleriGetir().Select(p => new SelectListItem()
            {
                Text=p.SehirAdi,
                Value=p.SehirId.ToString()
            }).ToList();
            ViewBag.sehirlerSelectList = sehirlerSelectList;
        }
    }
}