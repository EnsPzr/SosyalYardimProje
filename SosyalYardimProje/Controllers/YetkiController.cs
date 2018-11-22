using SosyalYardimProje.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SosyalYardimProje.Controllers
{
    public class YetkiController : Controller
    {
        [KullaniciLoginFilter]
        [HttpGet]
        public ActionResult Liste()
        {
            return View();
        }
    }
}