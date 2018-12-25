using System;
using System.Linq;
using System.Threading;
using System.Web.Mvc;
using BusinessLayer.Models.SubeModelleri;
using BusinessLayer.Siniflar;
using SosyalYardimProje.Filters;

namespace SosyalYardimProje.Controllers
{
    [HataFiltresi]
    public class SubeController : Controller
    {
        Sube subeBusinessLayer = new Sube();
        private Kullanici kullaniciBL = new Kullanici();
        [KullaniciLoginFilter]
        [HttpGet]
        public ActionResult Liste()
        {
            return View();
        }

        [HttpGet]
        [SadeceLoginFilter]
        public JsonResult SubeleriGetir(string aranan)
        {
            SubeJSModel jsModel = new SubeJSModel();
            var subeler = subeBusinessLayer.TumSubeleriGetir();
            if (aranan != null)
            {
                subeler = subeBusinessLayer.FiltreliSubeleriGetir(aranan);
            }
            else
            {
                subeler = subeBusinessLayer.TumSubeleriGetir();
            }
            jsModel.SubeModelList = subeler;
            jsModel.BasariliMi = true;
            jsModel.SubeSayisi = subeler.Count;
            Thread.Sleep(2000);
            return Json(jsModel, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [KullaniciLoginFilter]
        public ActionResult Ekle()
        {
            Tanimla();
            return View();
        }

        [HttpPost]
        [KullaniciLoginFilter]
        [ValidateAntiForgeryToken]
        public ActionResult Ekle(SubeModel yeniSube)
        {
            if (ModelState.IsValid)
            {
                bool sehirGorevlisiVarMi = subeBusinessLayer.sehirGorevlisiVarMi(yeniSube.Sehir.SehirId);
                if (!sehirGorevlisiVarMi)
                {
                    if (subeBusinessLayer.SubeEkle(yeniSube))
                    {
                        TempData["uyari"] = "Şube ekleme işlemi başarı ile tamamlandı.";
                        return RedirectToAction("Liste");
                    }
                    else
                    {
                        TempData["hata"] = "Bilinmeyen bir hata oluştu.";
                        Tanimla();
                        return View(yeniSube);
                    }
                }
                else
                {
                    TempData["hata"] = "Şehir için zaten bir görevli seçimi yapılmış.";
                    Tanimla();
                    return View(yeniSube);
                }
            }
            else
            {
                Tanimla();
                return View(yeniSube);
            }
        }

        [HttpGet]
        [KullaniciLoginFilter]
        public ActionResult Sil(int? id)
        {
            if (id != null)
            {
                var sube = subeBusinessLayer.SubeBul(id);
                if (sube != null)
                {
                    return View(sube);
                }
                else
                {
                    TempData["hata"] = "Silinecek şube bulunamadı";
                    return RedirectToAction("Liste");
                }
            }
            else
            {
                TempData["hata"] = "Lütfen silmek istediğiniz şubeyi seçiniz";
                return RedirectToAction("Liste");
            }
        }

        [HttpPost]
        [KullaniciLoginFilter]
        [ValidateAntiForgeryToken]
        public ActionResult SubeSil(int? id)
        {
            if (id != null)
            {
                var sube = subeBusinessLayer.SubeBul(id);
                if (sube != null)
                {
                    if (subeBusinessLayer.SubeSil(id))
                    {
                        TempData["uyari"] = "Şube silme işlemi başarı ile tamamlandı";
                        return RedirectToAction("Liste");
                    }
                    else
                    {
                        TempData["hata"] = "Bilinmeyen bir hata oluştu";
                        return RedirectToAction("Sil", "Sube", new { id });
                    }
                }
                else
                {
                    TempData["hata"] = "Silinecek şube bulunamadı";
                    return RedirectToAction("Liste");
                }
            }
            else
            {
                TempData["hata"] = "Lütfen silmek istediğiniz şubeyi seçiniz";
                return RedirectToAction("Liste");
            }
        }


        [HttpGet]
        [KullaniciLoginFilter]
        public ActionResult Duzenle(int? id)
        {
            if (id != null)
            {
                var sube = subeBusinessLayer.SubeBul(id);
                if (sube != null)
                {
                    Tanimla();
                    return View(sube);
                }
                else
                {
                    TempData["hata"] = "Düzenlemek istediğiniz şube bulunamadı.";
                    return RedirectToAction("Liste");
                }
            }
            else
            {
                Tanimla();
                TempData["hata"] = "Lütfen düzenlemek istediğiniz şubeyi bulunuz.";
                return RedirectToAction("Liste");
            }
        }

        [HttpPost]
        [KullaniciLoginFilter]
        [ValidateAntiForgeryToken]
        public ActionResult Duzenle(SubeModel duzenlenmisSube)
        {
            if (ModelState.IsValid)
            {
                var sonuc = subeBusinessLayer.SubeGuncelle(duzenlenmisSube);
                if (sonuc.TamamlandiMi==true)
                {
                    TempData["uyari"] = "Şube güncelleme işlemi başarı ile tamamlandı.";
                    return RedirectToAction("Liste");
                }
                else
                {
                    String hatalar = "";
                    foreach (var hata in sonuc.HataMesajlari)
                    {
                        hatalar = hatalar + " " + hata;
                    }

                    TempData["hata"] = hatalar;
                    Tanimla();
                    return View(duzenlenmisSube);
                }
            }
            else
            {
                Tanimla();
                return View(duzenlenmisSube);
            }
        }

        [HttpGet]
        [KullaniciLoginFilter]
        public ActionResult Detay(int? id)
        {
            if (id != null)
            {
                var sube = subeBusinessLayer.SubeBul(id);
                if (sube != null)
                {
                    return View(sube);
                }
                else
                {
                    TempData["hata"] = "Görüntülenecek şube bulunamadı.";
                    return RedirectToAction("Liste");
                }
            }
            else
            {
                TempData["hata"] = "Lütfen görüntülemek istediğiniz şubeyi seçiniz";
                return RedirectToAction("Liste");
            }
        }
        public void Tanimla()
        {
            var sehirler = kullaniciBL.SehirleriGetir(KullaniciBilgileriDondur.KullaniciId()).Select(p => new SelectListItem()
            {
                Text = p.SehirAdi,
                Value = p.SehirId.ToString()
            }).ToList();
            ViewBag.sehirler = sehirler;
            var kullanicilar = kullaniciBL.TumKullanicilariGetir(KullaniciBilgileriDondur.KullaniciId()).Select(p => new SelectListItem()
            {
                Value = p.KullaniciId.ToString(),
                Text = p.KullaniciAdi + " " + p.KullaniciSoyadi
            }).ToList();
            ViewBag.kullanicilar = kullanicilar;
        }
    }
}