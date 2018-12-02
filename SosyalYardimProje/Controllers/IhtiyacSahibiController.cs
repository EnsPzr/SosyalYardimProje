using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BusinessLayer.Models.IhtiyacSahibiModelleri;
using BusinessLayer.Siniflar;
using SosyalYardimProje.Filters;

namespace SosyalYardimProje.Controllers
{
    public class IhtiyacSahibiController : Controller
    {
        private IhtiyacSahibi ihtiyacSahibiBAL= new IhtiyacSahibi();
        [KullaniciLoginFilter]
        public ActionResult Liste()
        {
            return View();
        }
        [SadeceLoginFilter]
        public JsonResult IhtiyacSahipleriniGetir()
        {
            IhtiyacSahibiJSModel ihtiyacSahibiModel = new IhtiyacSahibiJSModel();
            ihtiyacSahibiModel.IhtiyacSahipleri =
                ihtiyacSahibiBAL.TumIhtiyacSahipleriniGetir(KullaniciBilgileriDondur.KullaniciId());
            ihtiyacSahibiModel.BasariliMi = true;
            ihtiyacSahibiModel.IhtiyacSahibiSayisi = ihtiyacSahibiModel.IhtiyacSahipleri.Count;
            return Json(ihtiyacSahibiModel, JsonRequestBehavior.AllowGet);
        }
    }
}