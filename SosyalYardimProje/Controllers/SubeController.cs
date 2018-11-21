using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using BusinessLayer.Models.SubeModelleri;
using BusinessLayer.Siniflar;
using SosyalYardimProje.Filters;

namespace SosyalYardimProje.Controllers
{
    public class SubeController : Controller
    {
        Sube subeBusinessLayer = new Sube();
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
    }
}