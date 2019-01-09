using BusinessLayer.Siniflar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BusinessLayer.Models.MesajModelleri;
using System.Threading;
using SosyalYardimProje.Filters;

namespace SosyalYardimProje.Controllers
{
    [HataFiltresi]
    public class MesajController : Controller
    {
        private Mesaj mesajBAL = new Mesaj();
        private Kullanici kullaniciBAL = new Kullanici();
        [KullaniciLoginFilter]
        public ActionResult Liste()
        {
            KullaniciBilgileriDondur.LogKaydet(0, "Mesaj Listesi Görüntülendi.");
            Tanimla();
            return View();
        }

        [SadeceLoginFilter]
        [HttpGet]
        public JsonResult TumMesajlariGetir()
        {
            MesajJsModel model = new MesajJsModel()
            {
                BasariliMi = true,
                MesajList = mesajBAL.TumMesajlariGetir(KullaniciBilgileriDondur.KullaniciId())
            };
            model.MesajSayisi = model.MesajList.Count;
            Thread.Sleep(2000);
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        [SadeceLoginFilter]
        [HttpGet]
        public JsonResult FiltreliMesajlariGetir(int? arananKullaniciId, string aranan, string tarih, int? kimeGonderildi)
        {
            aranan = "";
            if (aranan.Equals(""))
            {
                aranan = null;
            }

            if (tarih.Equals(""))
            {
                tarih = null;
            }
            MesajJsModel model = new MesajJsModel()
            {
                BasariliMi = true,
                MesajList = mesajBAL.FiltreliMesajlariGetir(KullaniciBilgileriDondur.KullaniciId(), arananKullaniciId, aranan, tarih, kimeGonderildi)
            };
            model.MesajSayisi = model.MesajList.Count;
            Thread.Sleep(2000);
            return Json(model, JsonRequestBehavior.AllowGet);
        }
        [KullaniciLoginFilter]
        public ActionResult DetayListe(int? id)
        {
            KullaniciBilgileriDondur.LogKaydet(0, "Mesaj detay listesi görüntülendi. Mesaj Id=>"+id);
            ViewBag.MesajId = id;
            Tanimla();
            return View();
        }

        [SadeceLoginFilter]
        [HttpGet]
        public JsonResult TumDetaylariGetir(int? detayId)
        {
            Thread.Sleep(2000);
            if (mesajBAL.KullaniciIslemYapabilirMi(KullaniciBilgileriDondur.KullaniciId(), detayId))
            {
                MesajDetayJsModel model = new MesajDetayJsModel()
                {
                    BasariliMi = true,
                    MesajDetayList = mesajBAL.TumMesajlarDetaylariGetir(detayId)
                };
                model.MesajDetaySayisi = model.MesajDetayList.Count;
                return Json(model, JsonRequestBehavior.AllowGet);
            }
            else
            {
                MesajDetayJsModel model = new MesajDetayJsModel()
                {
                    BasariliMi = false
                };
                return Json(model, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public JsonResult FiltreliDetaylariGetir(int? detayId, string aranan)
        {
            Thread.Sleep(2000);
            if (mesajBAL.KullaniciIslemYapabilirMi(KullaniciBilgileriDondur.KullaniciId(), detayId))
            {
                if (aranan.Equals(""))
                {
                    aranan = null;
                }
                MesajDetayJsModel model = new MesajDetayJsModel()
                {
                    BasariliMi = true,
                    MesajDetayList = mesajBAL.FiltreliMesajlarDetaylariGetir(detayId, aranan)
                };
                model.MesajDetaySayisi = model.MesajDetayList.Count;
                return Json(model, JsonRequestBehavior.AllowGet);
            }
            else
            {
                MesajDetayJsModel model = new MesajDetayJsModel()
                {
                    BasariliMi = false
                };
                return Json(model, JsonRequestBehavior.AllowGet);
            }
        }

        [KullaniciLoginFilter]
        public ActionResult YeniMesaj()
        {
            Tanimla();
            return View();
        }

        [KullaniciLoginFilter]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult YeniMesaj(GonderilecekMesajModel model)
        {
            model.GonderenId = KullaniciBilgileriDondur.KullaniciId();
            if (ModelState.IsValid)
            {
                var sonuc = mesajBAL.MesajGonder(model);
                if (sonuc.TamamlandiMi == true)
                {
                    KullaniciBilgileriDondur.LogKaydet(1, "Yeni mesaj gönderimi başlatıldı. Mesaj metni=>"+model.MesajMetni);
                    TempData["uyari"] = "İşlem başarı ile gerçekleşti.";
                    return RedirectToAction("Liste");
                }
                else
                {
                    string hatalar = KullaniciBilgileriDondur.HataMesajlariniOku(sonuc.HataMesajlari);
                    if (hatalar != null)
                    {
                        TempData["hata"] = "Gönderim işlemi sırasında hata oluştu.";
                        Tanimla();
                        return View(model);
                    }
                    else
                    {
                        TempData["hata"] = hatalar;
                        Tanimla();
                        return View(model);
                    }
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
            var kullanicilar = kullaniciBAL.TumKullanicilariGetir(KullaniciBilgileriDondur.KullaniciId());
            var kullanicilarSelect = kullanicilar.Select(p => new SelectListItem()
            {
                Text = p.KullaniciAdi + " " + p.KullaniciSoyadi,
                Value = p.KullaniciId.ToString()
            }).ToList();
            ViewBag.kullanicilarSelect = kullanicilarSelect;

            var kimeGonderildi = new List<SelectListItem>
            {
                new SelectListItem() { Text = "Herkes", Value = "0" },
                new SelectListItem() { Text = "Koordinatörler", Value = "1" }
            };
            ViewBag.kimeGonderildi = kimeGonderildi;

            var sehirler = kullaniciBAL.SehirleriGetir(KullaniciBilgileriDondur.KullaniciId());
            var sehirlerSelect = sehirler.Select(p => new SelectListItem()
            {
                Text = p.SehirAdi,
                Value = p.SehirId.ToString()
            }).ToList();
            if (KullaniciBilgileriDondur.KullaniciMerkezdeMi()== true)
            {
                sehirlerSelect.Add(new SelectListItem()
                {
                    Text = "Her Yer",
                    Selected = true,
                    Value = "82"
                });
            }
            ViewBag.sehirlerSelect = sehirlerSelect;
        }
    }
}