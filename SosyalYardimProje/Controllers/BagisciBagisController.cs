using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BusinessLayer.BagisciSiniflar;

namespace SosyalYardimProje.Controllers
{
    public class BagisciBagisController : Controller
    {
        private BagisciBagis bagisBAL = new BagisciBagis();
        public ActionResult Liste()
        {
            return View();
        }
    }
}