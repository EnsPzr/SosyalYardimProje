using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using BusinessLayer;
using BusinessLayer.Models;
using BusinessLayer.Models.DepoModelleri;
using BusinessLayer.Siniflar;
using SosyalYardimProje.Filters;

namespace SosyalYardimProje.Controllers
{
    [HataFiltresi]
    public class DepoController : Controller
    {
        private Esya esyaBAL = new Esya();
        private Depo depoBAL = new Depo();
        private Kullanici kullaniciBAL = new Kullanici();
        [KullaniciLoginFilter]
        public ActionResult Liste()
        {
            Tanimla();
            return View();
        }
        [HttpGet]
        [SadeceLoginFilter]
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
        [HttpGet]
        [SadeceLoginFilter]
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
        [KullaniciLoginFilter]
        public ActionResult Ekle()
        {
            Tanimla();
            return View();
        }
        [KullaniciLoginFilter]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Ekle(DepoModel eklenecekEsya)
        {
            if (ModelState.IsValid)
            {
                var sonuc = depoBAL.DepoyaEsyaEkle(eklenecekEsya, KullaniciBilgileriDondur.KullaniciId());
                if (sonuc.TamamlandiMi == true)
                {
                    TempData["uyari"] = "Depoya eşya başarı ile eklendi.";
                    return RedirectToAction("Liste");
                }
                else
                {
                    Tanimla();
                    String hatalar = KullaniciBilgileriDondur.HataMesajlariniOku(sonuc.HataMesajlari);
                    TempData["hata"] = hatalar;
                    return View(eklenecekEsya);
                }
            }
            else
            {
                Tanimla();
                return View(eklenecekEsya);
            }
        }

        [KullaniciLoginFilter]
        public ActionResult Duzenle(int? id)
        {
            if (id != null)
            {
                var duzenlenecekEsya = depoBAL.DepoEsyaGetir(id,KullaniciBilgileriDondur.KullaniciId());
                if (duzenlenecekEsya != null)
                {
                    Tanimla();
                    return View(duzenlenecekEsya);
                }
                else
                {
                    TempData["hata"] = "Deponuzdan düzenlemek istediğiniz eşya bulunmadı.";
                    return RedirectToAction("Liste");
                }
            }
            else
            {
                TempData["hata"] = "Deponuzdan düzenlemek istediğiniz eşyayı seçiniz";
                return RedirectToAction("Liste");
            }
        }
        [KullaniciLoginFilter]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Duzenle(DepoModel duzenlenmisModel)
        {
            if (ModelState.IsValid)
            {
                var sonuc = depoBAL.DepoEsyaGuncelle(duzenlenmisModel,KullaniciBilgileriDondur.KullaniciId());
                if (sonuc.TamamlandiMi == true)
                {
                    TempData["uyari"] = "Deponuzdaki eşyayı güncelleme işlemi başarı ile tamamlandı.";
                    return RedirectToAction("Liste");
                }
                else
                {
                    String hatalar = KullaniciBilgileriDondur.HataMesajlariniOku(sonuc.HataMesajlari);
                    TempData["hata"] = hatalar;
                    Tanimla();
                    return View(duzenlenmisModel);
                }
            }
            else
            {
                Tanimla();
                return View(duzenlenmisModel);
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