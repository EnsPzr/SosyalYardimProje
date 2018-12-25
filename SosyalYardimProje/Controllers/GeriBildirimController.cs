using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using BusinessLayer;
using BusinessLayer.Models;
using BusinessLayer.Models.AnaSayfaModelleri;
using BusinessLayer.Models.GeriBildirimModelleri;
using BusinessLayer.Siniflar;
using SosyalYardimProje.Filters;

namespace SosyalYardimProje.Controllers
{
    public class GeriBildirimController : Controller
    {
        private Kullanici kullaniciBAL = new Kullanici();
        private GeriBildirim geriBildirimBAL = new GeriBildirim();

        [KullaniciLoginFilter]
        public ActionResult Liste()
        {
            Tanimla();
            return View();
        }

        [SadeceLoginFilter]
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

        [SadeceLoginFilter]
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
                GeriBildirimList = geriBildirimBAL.FiltreliGeriBildirimleriGetir(KullaniciBilgileriDondur.KullaniciId(),
                    aranan, tarih, sehirId)
            };
            model.GeriBildirimSayisi = model.GeriBildirimList.Count;
            Thread.Sleep(2000);
            return Json(model, JsonRequestBehavior.AllowGet);
        }
        [KullaniciLoginFilter]
        public ActionResult Duzenle(int? id)
        {
            if (id != null)
            {
                var geriBildirim = geriBildirimBAL.GeriBildirimGetir(id, null);
                if (geriBildirim != null)
                {
                    if (geriBildirimBAL.KullaniciIslemYapabilirMi(KullaniciBilgileriDondur.KullaniciId(), id))
                    {
                        Tanimla();
                        return View(geriBildirim);
                    }
                    else
                    {
                        TempData["hata"] =
                            "Sadece kendi bölgenize gelen geri bildirimler ile ilgili işlem yapabilirsiniz.";
                        return RedirectToAction("Liste");
                    }
                }
                else
                {
                    TempData["hata"] = "Düzenlemek istediğiniz geri bildirimi bulunamadı.";
                    return RedirectToAction("Liste");
                }
            }
            else
            {
                TempData["hata"] = "Düzenlemek istediğiniz geri bildirimi seçiniz.";
                return RedirectToAction("Liste");
            }
        }
        [KullaniciLoginFilter]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Duzenle(GeriBildirimModel model)
        {
            if (ModelState.IsValid)
            {
                var sonuc = geriBildirimBAL.GeriBildirimKaydet(KullaniciBilgileriDondur.KullaniciId(),
                    model.GeriBildirimId, model.DurumInt);
                if (sonuc.TamamlandiMi == true)
                {
                    TempData["uyari"] = "İşlem başarı ile gerçekleşti";
                    return RedirectToAction("Liste");
                }
                else
                {
                    TempData["hata"] = KullaniciBilgileriDondur.HataMesajlariniOku(sonuc.HataMesajlari);
                    return RedirectToAction("Liste");
                }
            }
            else
            {
                Tanimla();
                return View(model);
            }
        }


        [BagisciLoginFilter]
        public ActionResult GeriBildirimListesi()
        {
            var geriBildirimler = geriBildirimBAL.TumGeriBildirimleriGetir(KullaniciBilgileriDondur.KullaniciId());
            return View(geriBildirimler);
        }

        [BagisciLoginFilter]
        public ActionResult YeniGeriBildirim()
        {
            GeriBildirimModel model = new GeriBildirimModel();
            model.KullaniciId = BagisciBilgileriDondur.KullaniciId();
            return View(model);
        }
        [BagisciLoginFilter]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult YeniGeriBildirim(GeriBildirimModel model)
        {
            model.Tarih = DateTime.Now;
            if (ModelState.IsValid)
            {
                var sonuc = geriBildirimBAL.YeniGeriBildirimKaydet(model);
                if (sonuc.TamamlandiMi == true)
                {
                    TempData["uyari"] = "Geri bildiriminiz alınmıştır. Teşekkür ederiz.";
                    return RedirectToAction("GeriBildirimListesi");
                }
                else
                {
                    TempData["hata"] = KullaniciBilgileriDondur.HataMesajlariniOku(sonuc.HataMesajlari);
                    return View(model);
                }
            }
            else
            {
                return View(model);
            }
        }

        [BagisciLoginFilter]
        public ActionResult GeriBildirimGuncelle(int? id)
        {
            if (id != null)
            {
                var geriBildirim = geriBildirimBAL.GeriBildirimGetir(id, 1);
                if (geriBildirim != null)
                {
                    if (geriBildirimBAL.BagisciGeriBildirimGuncelleyebilirMi(BagisciBilgileriDondur.KullaniciId(), id))
                    {
                        return View(geriBildirim);
                    }
                    else
                    {
                        TempData["hata"] = "Geri bildirim okunduğundan dolayı güncelleme yapamazsınız.";
                        return RedirectToAction("GeriBildirimListesi");
                    }
                }
                else
                {
                    TempData["hata"] = "Düzenlemek istediğiniz geri bildirimi bulunamadı.";
                    return RedirectToAction("GeriBildirimListesi");
                }
            }
            else
            {
                TempData["hata"] = "Düzenlemek istediğiniz geri bildirimi seçiniz.";
                return RedirectToAction("GeriBildirimListesi");
            }
        }

        [BagisciLoginFilter]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult GeriBildirimGuncelle(GeriBildirimModel model)
        {
            if (ModelState.IsValid)
            {
                var sonuc = geriBildirimBAL.GeriBildirimGuncelle(model, BagisciBilgileriDondur.KullaniciId());
                if (sonuc.TamamlandiMi == true)
                {
                    TempData["uyari"] = "Geri bildiriminiz alınmıştır. Teşekkür ederiz.";
                    return RedirectToAction("GeriBildirimListesi");
                }
                else
                {
                    TempData["hata"] = KullaniciBilgileriDondur.HataMesajlariniOku(sonuc.HataMesajlari);
                    return View(model);
                }
            }
            else
            {
                return View(model);
            }
        }

        [BagisciLoginFilter]
        public ActionResult Sil(int? id)
        {
            if (id != null)
            {
                var geriBildirim = geriBildirimBAL.GeriBildirimGetir(id, 1);
                if (geriBildirim != null)
                {
                    if (geriBildirimBAL.BagisciGeriBildirimGuncelleyebilirMi(BagisciBilgileriDondur.KullaniciId(), id))
                    {
                        return View(geriBildirim);
                    }
                    else
                    {
                        TempData["hata"] = "Geri bildirim okunduğundan dolayı işlem yapamazsınız.";
                        return RedirectToAction("GeriBildirimListesi");
                    }
                }
                else
                {
                    TempData["hata"] = "Düzenlemek istediğiniz geri bildirimi bulunamadı.";
                    return RedirectToAction("GeriBildirimListesi");
                }
            }
            else
            {
                TempData["hata"] = "Düzenlemek istediğiniz geri bildirimi seçiniz.";
                return RedirectToAction("GeriBildirimListesi");
            }
        }

        [BagisciLoginFilter]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult GeriBildirimSil(int? id)
        {
            if (id != null)
            {
                var geriBildirim = geriBildirimBAL.GeriBildirimGetir(id, 1);
                if (geriBildirim != null)
                {
                    if (geriBildirimBAL.BagisciGeriBildirimGuncelleyebilirMi(BagisciBilgileriDondur.KullaniciId(), id))
                    {
                        var sonuc = geriBildirimBAL.GeriBildirimSil(id, BagisciBilgileriDondur.KullaniciId());
                        if (sonuc.TamamlandiMi == true)
                        {
                            TempData["uyari"] = "Geri bildirim başarıyla silme işlemi tamamlandı.";
                            return RedirectToAction("GeriBildirimListesi");
                        }
                        else
                        {
                            TempData["hata"] = KullaniciBilgileriDondur.HataMesajlariniOku(sonuc.HataMesajlari);
                            return RedirectToAction("GeriBildirimListesi");
                        }
                    }
                    else
                    {
                        TempData["hata"] = "Geri bildirim okunduğundan dolayı işlem yapamazsınız.";
                        return RedirectToAction("GeriBildirimListesi");
                    }
                }
                else
                {
                    TempData["hata"] = "İşlem yapmak istediğiniz geri bildirimi bulunamadı.";
                    return RedirectToAction("GeriBildirimListesi");
                }
            }
            else
            {
                TempData["hata"] = "İşlem yapmak istediğiniz geri bildirimi seçiniz.";
                return RedirectToAction("GeriBildirimListesi");
            }
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

            var durumlar = new List<SelectListItem>();
            durumlar.Add(new SelectListItem() { Text = "Okunmadı", Value = "0" });
            durumlar.Add(new SelectListItem() { Text = "Okundu", Value = "1" });
            durumlar.Add(new SelectListItem() { Text = "Geri Dönüş Yapıldı", Value = "2" });
            durumlar.Add(new SelectListItem() { Text = "Geri Dönüşe Gerek Görülmedi", Value = "3" });
            ViewBag.durumlarSelectList = durumlar;
        }

    }
}