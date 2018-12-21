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

        public ActionResult Duzenle(int? id)
        {
            if (id != null)
            {
                if (bagisciBAL.KullaniciIslemYapabilirMi(KullaniciBilgileriDondur.KullaniciId(),id))
                {
                    var bagisci = bagisciBAL.BagisciBul(id);
                    if (bagisci != null)
                    {
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

        public ActionResult Sil(int? id=1004)
        {
            if (id != null)
            {
                if (bagisciBAL.KullaniciIslemYapabilirMi(KullaniciBilgileriDondur.KullaniciId(), id))
                {
                    var bagisci = bagisciBAL.BagisciBul(id);
                    if (bagisci != null)
                    {
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

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult BagisciSil(int? id)
        {
            if (id != null)
            {
                var sonuc = bagisciBAL.BagisciSil(KullaniciBilgileriDondur.KullaniciId(), id);
                if (sonuc.TamamlandiMi == true)
                {
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

        public ActionResult Detay(int? id)
        {
            if (id != null)
            {
                if (bagisciBAL.KullaniciIslemYapabilirMi(KullaniciBilgileriDondur.KullaniciId(), id))
                {
                    var bagisci = bagisciBAL.BagisciBul(id);
                    if (bagisci != null)
                    {
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