using BusinessLayer.Siniflar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BusinessLayer.Models.MesajModelleri;
using System.Threading;

namespace SosyalYardimProje.Controllers
{
    public class MesajController : Controller
    {
        private Mesaj mesajBAL = new Mesaj();
        private Kullanici kullaniciBAL = new Kullanici();
        public ActionResult Liste()
        {
            return View();
        }

        [HttpGet]
        public JsonResult TumMesajlariGetir()
        {
            MesajJsModel model = new MesajJsModel()
            {
                BasariliMi=true,
                MesajList=mesajBAL.TumMesajlariGetir(KullaniciBilgileriDondur.KullaniciId())
            };
            model.MesajSayisi = model.MesajList.Count;
            Thread.Sleep(2000);
            return Json(model, JsonRequestBehavior.AllowGet);
        }
    }
}