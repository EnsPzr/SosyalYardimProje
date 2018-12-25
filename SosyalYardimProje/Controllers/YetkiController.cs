using SosyalYardimProje.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BusinessLayer.Models.KullaniciModelleri;
using BusinessLayer.Siniflar;
using System.Threading;
using BusinessLayer.Models.YetkiModelleri;

namespace SosyalYardimProje.Controllers
{
    [HataFiltresi]
    public class YetkiController : Controller
    {
        private Yetki yetkiBAL = new Yetki();
        private Kullanici kullaniciBAL = new Kullanici();
        [KullaniciLoginFilter]
        [HttpGet]
        public ActionResult Liste()
        {
            Tanimla();
            return View();
        }

        [SadeceLoginFilter]
        [HttpGet]
        public JsonResult KullanicilariGetir()
        {
            KullaniciJSModel kullanicilar = new KullaniciJSModel();
            kullanicilar.KullaniciModelList = yetkiBAL.KullanicilariGetir(KullaniciBilgileriDondur.KullaniciId());
            kullanicilar.BasariliMi = true;
            kullanicilar.KullaniciSayisi = kullanicilar.KullaniciModelList.Count;
            Thread.Sleep(2000);
            return Json(kullanicilar, JsonRequestBehavior.AllowGet);
        }

        [SadeceLoginFilter]
        [HttpGet]
        public JsonResult FiltreliKullanicilariGetir(String aranan, int? sehirId)
        {
            KullaniciJSModel kullanicilar = new KullaniciJSModel();
            kullanicilar.KullaniciModelList =
                yetkiBAL.FiltreliKullanicilariGetir(aranan, sehirId, KullaniciBilgileriDondur.KullaniciId());
            kullanicilar.KullaniciSayisi = kullanicilar.KullaniciModelList.Count;
            kullanicilar.BasariliMi = true;
            Thread.Sleep(2000);
            return Json(kullanicilar, JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
        [KullaniciLoginFilter]
        public ActionResult YetkiListesi(int? id)
        {
            YetkiDurumlari();
            if (id != null)
            {
                var kullanici = kullaniciBAL.KullaniciGetir(id);
                if (kullanici != null)
                {
                    if (yetkiBAL.KullaniciAyniBolgedeMi(id, KullaniciBilgileriDondur.KullaniciId()))
                    {
                        var yetkiler = yetkiBAL.YetkileriGetir(id);
                        if (yetkiler != null)
                        {
                            return View(yetkiler);
                        }
                        else
                        {
                            TempData["hata"] = "Yetkiler bulunamadı.";
                            return RedirectToAction("Liste");
                        }
                    }
                    else
                    {
                        TempData["hata"] = "Yalnızca kendi görevli oolduğunuz bölgedeki kullanıcılar için işlem yapabilirsiniz.";
                        return RedirectToAction("Liste");
                    }
                }
                else
                {
                    TempData["hata"] = "Yetkilerini görüntülemek istediğiniz kullanıcı bulunamadı.";
                    return RedirectToAction("Liste");
                }

            }
            else
            {
                TempData["hata"] = "Yetkileri görüntülemek için lütfen kullanıcı seçiniz";
                return RedirectToAction("Liste");
            }
        }

        [HttpPost]
        [KullaniciLoginFilter]
        public ActionResult YetkileriKaydet(List<YetkiModel> yetkiler)
        {
            YetkiDurumlari();
            var sonuc = yetkiBAL.YetkileriKaydet(yetkiler);
            if (sonuc.TamamlandiMi == true)
            {
                TempData["uyari"] = "Yetki kaydetme işlemi başarı ile tamamlandı.";
                return RedirectToAction("Liste");
            }
            else
            {
                String hatalar = "";
                foreach (var hataItem in sonuc.HataMesajlari)
                {
                    hatalar = hatalar + " " + hataItem;
                }
                TempData["hata"] = hatalar;
                if (yetkiler[0].YetkiId != null)
                {
                    int? yetkiId = yetkiler[0].YetkiId;
                    yetkiler= yetkiBAL.YetkidenKullaniciBul(yetkiId);
                }
                return View("YetkiListesi",yetkiler);
            }
        }

        public void Tanimla()
        {
            var sehirler = kullaniciBAL.SehirleriGetir(KullaniciBilgileriDondur.KullaniciId()).Select(p =>
                new SelectListItem()
                {
                    Text = p.SehirAdi,
                    Value = p.SehirId.ToString()
                }).ToList();
            ViewBag.sehirler = sehirler;
        }

        public void YetkiDurumlari()
        {
            var yetkiDurumlari = new List<SelectListItem>();
            yetkiDurumlari.Add(new SelectListItem() { Value = "true", Text = "Girebilir" });
            yetkiDurumlari.Add(new SelectListItem() { Value = "false", Text = "Giremez" });
            ViewBag.yetkiDurumlari = new SelectList(yetkiDurumlari, "Value", "Text");
        }
    }
}