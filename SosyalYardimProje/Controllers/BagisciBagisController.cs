using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using BusinessLayer.BagisciSiniflar;
using BusinessLayer.Models.TeslimAlinacakBagis;

namespace SosyalYardimProje.Controllers
{
    //full bağışçı
    public class BagisciBagisController : Controller
    {
        private BagisciBagis bagisBAL = new BagisciBagis();
        public ActionResult Liste()
        {
            return View();
        }

        [HttpGet]
        public JsonResult TumBagislariGetir()
        {
            TeslimAlinacakBagisJsModel model = new TeslimAlinacakBagisJsModel()
            {
                BasariliMi=true,
                BagisList= bagisBAL.TumBagislariGetir(BagisciBilgileriDondur.KullaniciId())
            };
            model.BagisSayisi = model.BagisList.Count;
            Thread.Sleep(2000);
            return Json(model, JsonRequestBehavior.AllowGet);
        }
    }
}