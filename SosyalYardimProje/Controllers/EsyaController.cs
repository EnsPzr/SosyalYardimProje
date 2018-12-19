using BusinessLayer.Models.EsyaModelleri;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using BusinessLayer.Siniflar;

namespace SosyalYardimProje.Controllers
{
    public class EsyaController : Controller
    {
        private Esya esyaBAL = new Esya();
        public ActionResult Liste()
        {
            return View();
        }

        [HttpGet]
        public JsonResult TumEsyalariGetir()
        {
            EsyaJsModel model = new EsyaJsModel()
            {
                BasariliMi = true,
                EsyaList= esyaBAL.TumEsyalariGetir()
            };
            model.EsyaSayisi = model.EsyaList.Count();
            Thread.Sleep(2000);
            return Json(model, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult FiltreliEsyalariGetir(String aranan)
        {
            EsyaJsModel model = new EsyaJsModel()
            {
                BasariliMi = true,
                EsyaList = esyaBAL.FiltreliEsyalariGetir(aranan)
            };
            model.EsyaSayisi = model.EsyaList.Count();
            Thread.Sleep(2000);
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Ekle()
        {
            return View();
        }
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Ekle(EsyaModel eklenecekEsya)
        {
            if (ModelState.IsValid)
            {
                var sonuc = esyaBAL.Ekle(eklenecekEsya);
                if (sonuc.TamamlandiMi == true)
                {
                    TempData["uyari"] = "Eşya ekleme işlemi başarı ile gerçekleşti";
                    return RedirectToAction("Liste", "Esya");
                }
                else
                {
                    String hatalar = "";
                    foreach (var hata in sonuc.HataMesajlari)
                    {
                        hatalar += hata + "\n";
                    }

                    TempData["hata"] = hatalar;
                    return View(eklenecekEsya);
                }
            }
            else
            {
                return View(eklenecekEsya);
            }
        }

        public ActionResult Duzenle(int? id)
        {
            if (id != null)
            {
                var esya = esyaBAL.EsyaGetir(id);
                if (esya != null)
                {
                    return View(esya);
                }
                else
                {
                    TempData["hata"] = "Aradığınız eşya sistemde bulunmamaktadır";
                    return RedirectToAction("Liste");
                }
            }
            else
            {
                TempData["hata"] = "Lütfen düzenlemek istediğiniz eşyayı seçiniz";
                return RedirectToAction("Liste");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Duzenle(EsyaModel esya)
        {
            var onay = esyaBAL.EsyaDuzenle(esya);
            if (onay.TamamlandiMi==true)
            {
                TempData["uyari"] = "Eşya düzenleme işlemi başarı ile tamamlandı.";
                return RedirectToAction("Liste");
            }
            else
            {
                String hatalar = "";
                foreach (var hata in onay.HataMesajlari)
                {
                    hatalar += hata + "\n";
                }

                TempData["hata"] = hatalar;
                return View(esya);
            }
        }


        public ActionResult Sil(int? id)
        {
            if (id != null)
            {
                var esya = esyaBAL.EsyaGetir(id);
                if (esya != null)
                {
                    return View(esya);
                }
                else
                {
                    TempData["hata"] = "Silmek istediğiniz eşya bulunamadı.";
                    return RedirectToAction("Liste");
                }
            }
            else
            {
                TempData["hata"] = "Silmek istediğiniz eşyayı seçiniz";
                return RedirectToAction("Liste");
            }
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        [Route("Sil/{id}")]
        public ActionResult EsyaSil(int? id)
        {
            if (id != null)
            {
                var esya = esyaBAL.EsyaGetir(id);
                if (esya != null)
                {
                    var onay = esyaBAL.EsyaSil(id);
                    if (onay.TamamlandiMi == true)
                    {
                        TempData["uyari"] = "Eşya silme işlemi başarı ile tamamlandı.";
                        return RedirectToAction("Liste");
                    }
                    else
                    {
                        String hatalar = KullaniciBilgileriDondur.HataMesajlariniOku(onay.HataMesajlari);
                        TempData["hata"] = hatalar;
                        return RedirectToAction("Liste");
                    }
                }
                else
                {
                    TempData["hata"] = "Silmek istediğiniz eşya bulunamadı.";
                    return RedirectToAction("Liste");
                }
            }
            else
            {
                TempData["hata"] = "Silmek istediğiniz eşyayı seçiniz.";
                return RedirectToAction("Liste");
            }
        }
    }
}