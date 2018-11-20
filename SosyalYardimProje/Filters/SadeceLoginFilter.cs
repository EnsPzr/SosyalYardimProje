using DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SosyalYardimProje.Filters
{
    public class SadeceLoginFilter : FilterAttribute, IActionFilter
    {
        BusinessLayer.KullaniciYonetimi kullaniciYonetimi = new BusinessLayer.KullaniciYonetimi();
        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
            if (filterContext.HttpContext.Session["KullaniciId"] != null)
            {
                var KullaniciIdVar = filterContext.HttpContext.Session["KullaniciId"];
                if (KullaniciIdVar != null)
                {
                    int? KullaniciId = Convert.ToInt32(KullaniciIdVar);
                    KullaniciBilgileriTablo kullanici = kullaniciYonetimi.LoginKullaniciBul(KullaniciId);
                    if (kullanici == null)
                    {
                        filterContext.Controller.TempData["hata"] = "Oturum zaman aşımına uğradı.";
                        filterContext.Result = new RedirectToRouteResult(new System.Web.Routing.RouteValueDictionary { { "Controller", "Giris" }, { "Action", "Giris" } });
                    }
                    else
                    {
                        if (!kullaniciYonetimi.KullaniciAktifMi(KullaniciId))
                        {
                            filterContext.Controller.TempData["hata"] = "Kullanıcı Aktif Değil. İl görevliniz ile iletişime geçiniz.";
                            filterContext.Result = new RedirectToRouteResult(new System.Web.Routing.RouteValueDictionary { { "Controller", "Giris" }, { "Action", "Giris" } });
                        }

                    }
                }
                else
                {
                    filterContext.Controller.TempData["hata"] = "Oturum zaman aşımına uğradı.";
                    filterContext.Result = new RedirectToRouteResult(new System.Web.Routing.RouteValueDictionary { { "Controller", "Giris" }, { "Action", "Giris" } });
                }
            }
            else
            {
                filterContext.Controller.TempData["hata"] = "Oturum zaman aşımına uğradı.";
                filterContext.Result = new RedirectToRouteResult(new System.Web.Routing.RouteValueDictionary { { "Controller", "Giris" }, { "Action", "Giris" } });
            }
        }

        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.HttpContext.Session["KullaniciId"] != null)
            {
                var KullaniciIdVar = filterContext.HttpContext.Session["KullaniciId"];
                if (KullaniciIdVar != null)
                {
                    int? KullaniciId = Convert.ToInt32(KullaniciIdVar);
                    KullaniciBilgileriTablo kullanici = kullaniciYonetimi.LoginKullaniciBul(KullaniciId);
                    if (kullanici == null)
                    {
                        filterContext.Controller.TempData["hata"] = "Oturum zaman aşımına uğradı.";
                        filterContext.Result = new RedirectToRouteResult(new System.Web.Routing.RouteValueDictionary { { "Controller", "Giris" }, { "Action", "Giris" } });
                    }
                    else
                    {
                        if (!kullaniciYonetimi.KullaniciAktifMi(KullaniciId))
                        {
                            filterContext.Controller.TempData["hata"] = "Kullanıcı Aktif Değil. İl görevliniz ile iletişime geçiniz.";
                            filterContext.Result = new RedirectToRouteResult(new System.Web.Routing.RouteValueDictionary { { "Controller", "Giris" }, { "Action", "Giris" } });
                        }

                    }
                }
                else
                {
                    filterContext.Controller.TempData["hata"] = "Oturum zaman aşımına uğradı.";
                    filterContext.Result = new RedirectToRouteResult(new System.Web.Routing.RouteValueDictionary { { "Controller", "Giris" }, { "Action", "Giris" } });
                }
            }
            else
            {
                filterContext.Controller.TempData["hata"] = "Oturum zaman aşımına uğradı.";
                filterContext.Result = new RedirectToRouteResult(new System.Web.Routing.RouteValueDictionary { { "Controller", "Giris" }, { "Action", "Giris" } });
            }
        }
    }
}