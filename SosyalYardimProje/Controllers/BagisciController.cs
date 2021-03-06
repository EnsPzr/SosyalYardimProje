﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using BusinessLayer.Models.BagisciModelleri;
using BusinessLayer.Siniflar;
using SosyalYardimProje.Filters;

namespace SosyalYardimProje.Controllers
{
    [HataFiltresi]
    public class BagisciController : Controller
    {
        private Bagisci bagisciBAL = new Bagisci();
        private Kullanici kullaniciBAL = new Kullanici();
        [KullaniciLoginFilter]
        public ActionResult Liste()
        {
            Tanimla();
            KullaniciBilgileriDondur.LogKaydet(0, "Bağışçı Listesi Görüntülendi.");
            return View();
        }

        [SadeceLoginFilter]
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
        [SadeceLoginFilter]
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
        [KullaniciLoginFilter]
        public ActionResult Duzenle(int? id)
        {
            if (id != null)
            {
                if (bagisciBAL.KullaniciIslemYapabilirMi(KullaniciBilgileriDondur.KullaniciId(),id))
                {
                    var bagisci = bagisciBAL.BagisciBul(id);
                    if (bagisci != null)
                    {
                        KullaniciBilgileriDondur.LogKaydet(3, "Bağışçı düzenlenmek için görüntülendi. "+bagisci.BagisciAdi+" "+bagisci.BagisciSoyadi);
                        Tanimla();
                        return View(bagisci);
                    }
                    else
                    {
                        TempData["hata"] = "Düzenlemek istediğiniz bağışçı bulunamadı.";
                        return RedirectToAction("Liste");
                    }
                }
                else
                {
                    TempData["hata"] = "Sadece kendi bölgenizdeki bağışçılar ile ilgili işlem yapabilirsiniz.";
                    return RedirectToAction("Liste");
                }
            }
            else
            {
                TempData["hata"] = "Düzenlemek istediğiniz bağışçıyı seçiniz";
                return RedirectToAction("Liste");
            }
        }
        [KullaniciLoginFilter]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Duzenle(BagisciModel model)
        {
            if (ModelState.IsValid)
            {
                if (bagisciBAL.KullaniciIslemYapabilirMi(KullaniciBilgileriDondur.KullaniciId(), model.BagisciId))
                {
                    var onay = bagisciBAL.BagisciKaydet(model);
                    if (onay.TamamlandiMi == true)
                    {
                        KullaniciBilgileriDondur.LogKaydet(3, "Bağışçı düzenlendi. "+model.BagisciAdi+" "+model.BagisciSoyadi);
                        TempData["uyari"] = "Bağışçı güncelleme işlemi başarı ile tamamlandı.";
                        return RedirectToAction("Liste");
                    }
                    else
                    {
                        String hatalar = KullaniciBilgileriDondur.HataMesajlariniOku(onay.HataMesajlari);
                        TempData["hata"] = hatalar;
                        Tanimla();
                        return View(model);
                    }
                }
                else
                {
                    TempData["hata"] = "Sadece kendi bölgenizdeki bağışçılar ile ilgili işlem yapabilirsiniz.";
                    return RedirectToAction("Liste");
                }
            }
            else
            {
                Tanimla();
                return View(model);
            }
        }
        [KullaniciLoginFilter]
        public ActionResult Sil(int? id)
        {
            if (id != null)
            {
                if (bagisciBAL.KullaniciIslemYapabilirMi(KullaniciBilgileriDondur.KullaniciId(), id))
                {
                    var bagisci = bagisciBAL.BagisciBul(id);
                    if (bagisci != null)
                    {
                        KullaniciBilgileriDondur.LogKaydet(2, "Bağışçı silinmek için görüntülendi. "+bagisci.BagisciAdi+" "+bagisci.BagisciSoyadi);
                        return View(bagisci);
                    }
                    else
                    {
                        TempData["hata"] = "Düzenlemek istediğiniz bağışçı bulunamadı.";
                        return RedirectToAction("Liste");
                    }
                }
                else
                {
                    TempData["hata"] = "Sadece kendi bölgenizdeki bağışçılar ile ilgili işlem yapabilirsiniz.";
                    return RedirectToAction("Liste");
                }
            }
            else
            {
                TempData["hata"] = "Düzenlemek istediğiniz bağışçıyı seçiniz";
                return RedirectToAction("Liste");
            }
        }
        [KullaniciLoginFilter]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult BagisciSil(int? id)
        {
            if (id != null)
            {
                var sonuc = bagisciBAL.BagisciSil(KullaniciBilgileriDondur.KullaniciId(), id);
                if (sonuc.TamamlandiMi == true)
                {
                    KullaniciBilgileriDondur.LogKaydet(2, "Bağışçı silindi. "+id);
                    TempData["uyari"] = "İşlem başarı ile tamamlandı.";
                    return RedirectToAction("Liste");
                }
                else
                {
                    String hatalar = KullaniciBilgileriDondur.HataMesajlariniOku(sonuc.HataMesajlari);
                    TempData["hata"] = hatalar;
                    return RedirectToAction("Liste");
                }
            }
            else
            {
                TempData["hata"] = "Düzenlemek istediğiniz bağışçıyı seçiniz";
                return RedirectToAction("Liste");
            }
        }
        [KullaniciLoginFilter]
        public ActionResult Detay(int? id)
        {
            if (id != null)
            {
                if (bagisciBAL.KullaniciIslemYapabilirMi(KullaniciBilgileriDondur.KullaniciId(), id))
                {
                    var bagisci = bagisciBAL.BagisciBul(id);
                    if (bagisci != null)
                    {
                        KullaniciBilgileriDondur.LogKaydet(4, "Bağışçı detay görüntülendi. "+bagisci.BagisciAdi+" "+bagisci.BagisciSoyadi);
                        Tanimla();
                        return View(bagisci);
                    }
                    else
                    {
                        TempData["hata"] = "Görüntülemek istediğiniz bağışçı bulunamadı.";
                        return RedirectToAction("Liste");
                    }
                }
                else
                {
                    TempData["hata"] = "Sadece kendi bölgenizdeki bağışçılar ile ilgili işlem yapabilirsiniz.";
                    return RedirectToAction("Liste");
                }
            }
            else
            {
                TempData["hata"] = "Görüntülemek istediğiniz bağışçıyı seçiniz";
                return RedirectToAction("Liste");
            }
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