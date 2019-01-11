using SosyalYardimProje.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BusinessLayer.Siniflar;
using System.Collections;

namespace SosyalYardimProje.Controllers
{
    [HataFiltresi]
    public class LogController : Controller
    {
        private Log logBAL = new Log();
        [KullaniciLoginFilter]
        public ActionResult Liste()
        {
            Tanimla();
            KullaniciBilgileriDondur.LogKaydet(0, "Log Listesi Görüntülendi.");
            return View(logBAL.TumLoglariGetir(KullaniciBilgileriDondur.KullaniciId()));
        }

        [HttpGet]
        [SadeceLoginFilter]
        public ActionResult FiltreliLoglariGetir(int? islemTipi, string aranan, string tarih)
        {
            if (tarih != null)
            {
                try
                {
                    DateTime.Parse(tarih);
                }
                catch (Exception)
                {
                    tarih = null;
                }
            }
            Tanimla();
            KullaniciBilgileriDondur.LogKaydet(0, "Filtreli Log Listesi Görüntülendi.");
            return View("Liste",logBAL.FiltreliLoglariGetir(KullaniciBilgileriDondur.KullaniciId(),islemTipi,aranan,tarih));
        }


        public void Tanimla()
        {
            SelectList islemTurleriSelectList = new SelectList((IEnumerable)logBAL.IslemTipleri(), "Key", "Value");
            ViewBag.islemTurleriSelectList = logBAL.IslemTipleri();
        }
    }
}