using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BusinessLayer;
using DataLayer;
namespace SosyalYardimProje.Filters
{
    public class KullaniciLoginFilter : FilterAttribute, IActionFilter
    {
        BusinessLayer.KullaniciYonetimi kullaniciYonetimi = new BusinessLayer.KullaniciYonetimi();
        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
            if (filterContext.HttpContext.Session["KullaniciGuId"] != null)
            {
                KullaniciBilgileriTablo kullanici = kullaniciYonetimi.LoginKullaniciBul(filterContext.HttpContext.Session["KullaniciGuId"].ToString());
                if (kullanici == null)
                {
                    filterContext.Controller.TempData["hata"] = "Oturum zaman aşımına uğradı.";
                    filterContext.Result = new RedirectToRouteResult(new System.Web.Routing.RouteValueDictionary { { "Controller", "Giris" }, { "Action", "Giris" } });
                }
            }
        }

        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.HttpContext.Session["KullaniciGuId"] != null)
            {
                KullaniciBilgileriTablo kullanici = kullaniciYonetimi.LoginKullaniciBul(filterContext.HttpContext.Session["KullaniciGuId"].ToString());
                if (kullanici == null)
                {
                    filterContext.Controller.TempData["hata"] = "Oturum zaman aşımına uğradı.";
                    filterContext.Result = new RedirectToRouteResult(new System.Web.Routing.RouteValueDictionary { { "Controller", "Giris" }, { "Action", "Giris" } });
                }
            }
        }
    }
}