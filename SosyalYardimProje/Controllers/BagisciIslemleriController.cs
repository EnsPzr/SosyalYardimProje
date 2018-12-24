﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BusinessLayer;
using BusinessLayer.BagisciSiniflar;
using BusinessLayer.Models.BagisciGiris;
using BusinessLayer.Siniflar;

namespace SosyalYardimProje.Controllers
{
    public class BagisciIslemleriController : Controller
    {
        private BagisciYonetimi bagisciBAL = new BagisciYonetimi();
        private Kullanici kullaniciBAL = new Kullanici();
        public ActionResult Giris()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Giris(BagisciGirisModel model)
        {
            if (ModelState.IsValid)
            {
                var bagisci = bagisciBAL.BagisciBul(model.Eposta, model.Sifre);
                if (bagisci != null)
                {
                    Session["KullaniciId"] = bagisci.KullaniciId;
                    Session["Bilgi"] = bagisci.KullaniciAdi + " " + bagisci.KullaniciSoyadi;
                    return RedirectToAction("AnaSayfa", "BagisciIslemleri");
                }
                else
                {
                    ModelState.AddModelError("","E Posta veya Şifre hatalı.");
                    return View(model);
                }
            }
            else
            {
                return View(model);
            }
        }

        public ActionResult AnaSayfa()
        {
            return View();
        }

        [ChildActionOnly]
        public PartialViewResult anaSayfaPartialGetir()
        {
            var anaSayfaModel = bagisciBAL.AnaSayfaBagislariGetir(BagisciBilgileriDondur.KullaniciId());
            return PartialView(anaSayfaModel);
        }

        public ActionResult Kayit()
        {
            Tanimla();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Kayit(BagisciKayitModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (model.BagisciTelNo.Length > 10)
                    {
                        string tel1 = model.BagisciTelNo.Substring(0, 8);
                        string tel2 = model.BagisciTelNo.Substring(8, model.BagisciTelNo.Length - 8);
                        Convert.ToInt32(tel1);
                        Convert.ToInt32(tel2);
                    }
                    else
                    {
                        Convert.ToInt32(model.BagisciTelNo);
                    }
                }
                catch (Exception)
                {
                    ModelState.AddModelError("BagisciTelNo","Telefon numarası sadece rakamlardan oluşabilir.");
                    Tanimla();
                    return View(model);
                }

                var sonuc = bagisciBAL.BagisciKaydet(model);
                if (sonuc.TamamlandiMi == true)
                {
                    TempData["uyari"] = "Kayıt başarı ile tamamlandı.";
                    return RedirectToAction("Giris");
                }
                else
                {
                    string hatalar = BagisciBilgileriDondur.HataMesajlariniOku(sonuc.HataMesajlari);
                    TempData["hata"] = hatalar;
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
            var sehirlerSelectList = kullaniciBAL.TumSehirleriGetir().Select(p=>new SelectListItem()
            {
                Text=p.SehirAdi,
                Value=p.SehirId.ToString()
            }).ToList();
            ViewBag.sehirlerSelectList = sehirlerSelectList;
        }
    }
}