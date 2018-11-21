using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using BusinessLayer;
using BusinessLayer.Models.KullaniciModelleri;
using BusinessLayer.Siniflar;
using SosyalYardimProje.Filters;

namespace SosyalYardimProje.Controllers
{

    public class KullaniciController : Controller
    {
        Kullanici kullaniciBusinessLayer = new Kullanici();
        [KullaniciLoginFilter]
        public ActionResult Liste()
        {
            Tanimla();
            return View(new KullaniciModel());
        }
        [SadeceLoginFilter]
        [HttpGet]
        public JsonResult KullanicilariGetir()
        {
            var jsKullaniciModel = new KullaniciJSModel()
            {
                KullaniciModelList =
                    kullaniciBusinessLayer.TumKullanicilariGetir(KullaniciBilgileriDondur.KullaniciId()),
                BasariliMi = true
            };
            jsKullaniciModel.KullaniciSayisi = jsKullaniciModel.KullaniciModelList.Count;
            Thread.Sleep(2000);
            return Json(jsKullaniciModel, JsonRequestBehavior.AllowGet);
        }

        [SadeceLoginFilter]
        [HttpGet]
        public JsonResult FiltreliKullanicilariGetir(KullaniciFiltrelemeModel filtreliKullaniciModel)
        {
            var jsKullaniciModel = new KullaniciJSModel()
            {
                KullaniciModelList =
                    kullaniciBusinessLayer.TumKullanicilariGetir(KullaniciBilgileriDondur.KullaniciId()),
                BasariliMi = true
            };
            jsKullaniciModel.KullaniciSayisi = jsKullaniciModel.KullaniciModelList.Count;
            Thread.Sleep(2000);
            return Json(jsKullaniciModel, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult Ekle()
        {
            MerkezdeGosterilecekMi();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Ekle(KullaniciModel yeniKullanici)
        {
            yeniKullanici.KullaniciSifre = "123456";
            yeniKullanici.KullaniciSifreTekrar = "123456";
            if (ModelState.IsValid)
            {
                if (KullaniciBilgileriDondur.KullaniciBilgileriGetir().KullaniciMerkezdeMi == true)
                {
                    if (!kullaniciBusinessLayer.KullaniciVarMi(yeniKullanici.KullaniciEPosta))
                    {
                        if (kullaniciBusinessLayer.KullaniciEkle(yeniKullanici))
                        {
                            yeniKullanici.KullaniciSifre = "123456";
                            yeniKullanici.KullaniciSifreTekrar = "123456";
                            TempData["uyari"] = yeniKullanici.KullaniciAdi + " " + yeniKullanici.KullaniciSoyadi +
                                                " kullanıcısı başarı ile kayıt edildi";
                            return RedirectToAction("Liste", "Kullanici");
                        }
                        else
                        {
                            TempData["hata"] = "Ekleme işlemi sırasında hata oluştu.";
                            MerkezdeGosterilecekMi();
                            return View(yeniKullanici);
                        }

                    }
                    else
                    {
                        ModelState.AddModelError("KullaniciEPosta", "E Posta adresi kullanımda.");
                        MerkezdeGosterilecekMi();
                        return View(yeniKullanici);
                    }
                }
                else
                {
                    if (!kullaniciBusinessLayer.KullaniciVarMi(yeniKullanici.KullaniciEPosta))
                    {
                        if (yeniKullanici.Sehir.SehirId ==
                            KullaniciBilgileriDondur.KullaniciBilgileriGetir().SehirTablo.SehirId)
                        {
                            if (kullaniciBusinessLayer.KullaniciEkle(yeniKullanici))
                            {
                                TempData["uyari"] = yeniKullanici.KullaniciAdi + " " + yeniKullanici.KullaniciSoyadi +
                                                    " kullanıcısı başarı ile kayıt edildi";
                                return RedirectToAction("Liste", "Kullanici");
                            }
                            else
                            {
                                TempData["hata"] = "Ekleme işlemi sırasında hata oluştu.";
                                MerkezdeGosterilecekMi();
                                return View(yeniKullanici);
                            }
                        }
                        else
                        {
                            TempData["hata"] = "Kullanıcıyı sadece görevli olduğunuz şehire ekleyebilirsiniz";
                            MerkezdeGosterilecekMi();
                            return View(yeniKullanici);
                        }

                    }
                    else
                    {
                        ModelState.AddModelError("KullaniciEPosta", "E Posta adresi kullanımda.");
                        MerkezdeGosterilecekMi();
                        return View(yeniKullanici);
                    }
                }
            }
            else
            {
                MerkezdeGosterilecekMi();
                return View(yeniKullanici);
            }
        }

        public void MerkezdeGosterilecekMi()
        {
            var kullaniciMerkezdeMi = KullaniciBilgileriDondur.KullaniciBilgileriGetir().KullaniciMerkezdeMi;
            ViewBag.GosterilecekMi = kullaniciMerkezdeMi == null ? false : kullaniciMerkezdeMi == true ? true : false;
            Tanimla();
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