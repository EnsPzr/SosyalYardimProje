using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using BusinessLayer;
using BusinessLayer.Models.DepoModelleri;
using BusinessLayer.Siniflar;

namespace SosyalYardimProje.Controllers
{
    public class DepoController : Controller
    {
        private Depo depoBAL = new Depo();
        public ActionResult Liste()
        {
            return View();
        }

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
    }
}