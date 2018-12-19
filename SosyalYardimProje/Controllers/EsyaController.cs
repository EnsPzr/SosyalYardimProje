using BusinessLayer.Models.EsyaModelleri;
using System;
using System.Collections.Generic;
using System.Linq;
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
    }
}