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
        public JsonResult FiltreliKasaGetir(string aranan, string tarih, int? sehirId, int? gelirGider)
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
                KasaList = kasaBAL.FiltreliKasaGetir(KullaniciBilgileriDondur.KullaniciId(), aranan, tarih, sehirId, gelirGider)
            };
            model.KasaSayisi = model.KasaList.Count;
            Thread.Sleep(2000);
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Ekle()
        {
            Tanimla();
            return View();
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Ekle(KasaModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.Tarih.HasValue)
                {
                    try
                    {
                        Convert.ToDateTime(model.Tarih);
                    }
                    catch (Exception)
                    {
                        Tanimla();
                        ModelState.AddModelError("Tarih", "Tarih formatı geçerli değil.");
                        return View(model);
                    }
                }

                if (model.Miktar < 0)
                {
                    Tanimla();
                    ModelState.AddModelError("Miktar", "Pozitif bir miktar giriniz");
                    return View(model);
                }
                else if (model.Miktar == 0)
                {
                    Tanimla();
                    ModelState.AddModelError("Miktar", "Pozitif bir miktar giriniz");
                    return View(model);
                }

                try
                {
                    Convert.ToDouble(model.Miktar);
                }
                catch (Exception)
                {
                    Tanimla();
                    ModelState.AddModelError("Miktar", "Lütfen geçerli bir miktar giriniz");
                    return View(model);
                }

                model.KullaniciId = KullaniciBilgileriDondur.KullaniciId();
                var onay = kasaBAL.KasaKaydet(KullaniciBilgileriDondur.KullaniciId(), model);
                if (onay.TamamlandiMi == true)
                {
                    TempData["uyari"]="İşlem başarı ile tamamlandı.";
                    return RedirectToAction("Liste");
                }
                else
                {
                    string hatalar = KullaniciBilgileriDondur.HataMesajlariniOku(onay.HataMesajlari);
                    TempData["hata"] = hatalar;
                    return RedirectToAction("Liste");
                }
            }
            else
            {
                Tanimla();
                return View(model);
            }
        }

        public ActionResult Duzenle(int? id)
        {
            if (id != null)
            {
                var kasa = kasaBAL.KasaGetir(id);
                if (kasa != null)
                {
                    if (kasaBAL.KullaniciIslemYapabilirMi(KullaniciBilgileriDondur.KullaniciId(),kasa.Sehir.SehirId))
                    {
                        Tanimla();
                        return View(kasa);
                    }
                    else
                    {
                        TempData["hata"] = "Sedece kendi bölgeniz için işlem yapabilirsiniz.";
                        return RedirectToAction("Liste");
                    }
                }
                else
                {
                    TempData["hata"] = "Düzenlemek istediğiniz kasa işlemi bulunamadı.";
                    return RedirectToAction("Liste");
                }
            }
            else
            {
                TempData["hata"] = "Lütfen düzenlemek istediğiniz kasa işlemini seçiniz";
                return RedirectToAction("Liste");
            }
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Duzenle(KasaModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.Tarih.HasValue)
                {
                    try
                    {
                        Convert.ToDateTime(model.Tarih);
                    }
                    catch (Exception)
                    {
                        Tanimla();
                        ModelState.AddModelError("Tarih", "Tarih formatı geçerli değil.");
                        return View(model);
                    }
                }

                if (model.Miktar < 0)
                {
                    Tanimla();
                    ModelState.AddModelError("Miktar", "Pozitif bir miktar giriniz");
                    return View(model);
                }
                else if (model.Miktar == 0)
                {
                    Tanimla();
                    ModelState.AddModelError("Miktar", "Pozitif bir miktar giriniz");
                    return View(model);
                }

                try
                {
                    Convert.ToDouble(model.Miktar);
                }
                catch (Exception)
                {
                    Tanimla();
                    ModelState.AddModelError("Miktar", "Lütfen geçerli bir miktar giriniz");
                    return View(model);
                }

                model.KullaniciId = KullaniciBilgileriDondur.KullaniciId();
                var onay = kasaBAL.KasaIslemGuncelle(KullaniciBilgileriDondur.KullaniciId(), model);
                if (onay.TamamlandiMi == true)
                {
                    TempData["uyari"] = "İşlem başarı ile tamamlandı.";
                    return RedirectToAction("Liste");
                }
                else
                {
                    string hatalar = KullaniciBilgileriDondur.HataMesajlariniOku(onay.HataMesajlari);
                    TempData["hata"] = hatalar;
                    return RedirectToAction("Liste");
                }
            }
            else
            {
                Tanimla();
                return View(model);
            }
        }

        public ActionResult Sil(int? id=1)
        {
            if (id != null)
            {
                var kasa = kasaBAL.KasaGetir(id);
                if (kasa != null)
                {
                    if (kasaBAL.KullaniciIslemYapabilirMi(KullaniciBilgileriDondur.KullaniciId(), kasa.Sehir.SehirId))
                    {
                        Tanimla();
                        return View(kasa);
                    }
                    else
                    {
                        TempData["hata"] = "Sedece kendi bölgeniz için işlem yapabilirsiniz.";
                        return RedirectToAction("Liste");
                    }
                }
                else
                {
                    TempData["hata"] = "Düzenlemek istediğiniz kasa işlemi bulunamadı.";
                    return RedirectToAction("Liste");
                }
            }
            else
            {
                TempData["hata"] = "Lütfen düzenlemek istediğiniz kasa işlemini seçiniz";
                return RedirectToAction("Liste");
            }
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult KasaSil(int? id)
        {
            if (id != null)
            {
                var kasa = kasaBAL.KasaGetir(id);
                if (kasa != null)
                {
                    if (kasaBAL.KullaniciIslemYapabilirMi(KullaniciBilgileriDondur.KullaniciId(), kasa.Sehir.SehirId))
                    {
                        var sonuc = kasaBAL.KasaSil(KullaniciBilgileriDondur.KullaniciId(), id);
                        if (sonuc.TamamlandiMi == true)
                        {
                            TempData["uyari"] = "İşlem başarı ile tamamlandı.";
                            return RedirectToAction("Liste");
                        }
                        else
                        {
                            string hatalar = KullaniciBilgileriDondur.HataMesajlariniOku(sonuc.HataMesajlari);
                            TempData["hata"] = hatalar;
                            return View(kasa);
                        }
                    }
                    else
                    {
                        TempData["hata"] = "Sedece kendi bölgeniz için işlem yapabilirsiniz.";
                        return RedirectToAction("Liste");
                    }
                }
                else
                {
                    TempData["hata"] = "Düzenlemek istediğiniz kasa işlemi bulunamadı.";
                    return RedirectToAction("Liste");
                }
            }
            else
            {
                TempData["hata"] = "Lütfen düzenlemek istediğiniz kasa işlemini seçiniz";
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
            ViewBag.sehirlerSelect = sehirler;
            var gelirGider = new List<SelectListItem>();
            gelirGider.Add(new SelectListItem() { Text = "Tümü", Value = "0" });
            gelirGider.Add(new SelectListItem() { Text = "Gelir", Value = "1" });
            gelirGider.Add(new SelectListItem() { Text = "Gider", Value = "2" });
            ViewBag.gelirGiderSelect = gelirGider;
            var gelirGider2 = new List<SelectListItem>();
            //gelirGider2.Add(new SelectListItem() { Text = "Tümü", Value = "0" });
            gelirGider2.Add(new SelectListItem() { Text = "Gelir", Value = "1" });
            gelirGider2.Add(new SelectListItem() { Text = "Gider", Value = "2" });
            ViewBag.gelirGiderSelect2 = gelirGider2;
        }
    }
}