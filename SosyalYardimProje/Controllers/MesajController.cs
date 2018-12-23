using BusinessLayer.Siniflar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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
    }
}