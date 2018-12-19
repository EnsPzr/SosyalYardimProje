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
    }
}