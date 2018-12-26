using BusinessLayer;
using BusinessLayer.Models.DisardanIhtiyacSahibiModelleri;
using BusinessLayer.Siniflar;
using SosyalYardimProje.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SosyalYardimProje.Controllers
{
    [HataFiltresi]
    public class DisardanController : Controller
    {
        private Kullanici kullaniciBAL = new Kullanici();
        private IhtiyacSahibi ihtiyacSahibiBAL = new IhtiyacSahibi();
        private GeriBildirim geriBildirimBAL = new GeriBildirim();
        private Kasa kasaBAL = new Kasa();
        public ActionResult AnaSayfa()
        {
            return View();
        }


        public ActionResult denemesayfa()
        {
            return View();
        }

        public ActionResult IhtiyacSahibiEkle()
        {
            Tanimla();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult IhtiyacSahibiEkle(DisardanIhtiyacSahibiModel model)
        {
            if (ModelState.IsValid)
            {
                var sonuc = ihtiyacSahibiBAL.DisardanIhtiyacSahibiKaydet(model);
                if (sonuc.TamamlandiMi == true)
                {
                    TempData["uyari"] = "Teşekkür ederiz. Ekiplerimiz tarafında ihtiyaç sahibi ziyaret edilecektir. Girmiş olduğunuz bilgiler doğrultusunda sizin için hesap açılmıştır. Hesabınıza girip eşya bağışı yapabilir ve eklediğiniz ihtiyaç sahiplerini görebilirsiniz.";
                    return RedirectToAction("AnaSayfa");
                }
                else
                {
                    String hatalar = KullaniciBilgileriDondur.HataMesajlariniOku(sonuc.HataMesajlari);
                    Tanimla();
                    return View(model);
                }
            }
            else
            {
                Tanimla();
                return View(model);
            }
        }

        public ActionResult GeriBildirimYap()
        {
            Tanimla();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult GeriBildirimYap(DisardanGeriBildirimModel model)
        {
            if (ModelState.IsValid)
            {
                var sonuc = geriBildirimBAL.DisardanGeriBildirimEkle(model);
                if (sonuc.TamamlandiMi == true)
                {
                    TempData["uyari"] =
                        "Geri bildiriminiz için teşekkür ederiz. Verdiğiniz bilgiler doğrultusunda sistemimizde sizin için bir hesap oluşturuldu. Dilerseniz hesabınıza girip eşya bağışı ve geri bildiriminizin durumunu takip edebilme gibi işlemler yapabilirsiniz.";
                    return RedirectToAction("AnaSayfa");
                }
                else
                {
                    String hatalar = KullaniciBilgileriDondur.HataMesajlariniOku(sonuc.HataMesajlari);
                    Tanimla();
                    return View(model);
                }
            }
            else
            {
                Tanimla();
                return View();
            }
        }

        public ActionResult NakdiBagisYap()
        {
            Tanimla();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult NakdiBagisYap(DisardanNakdiBagisModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.Miktar < 0)
                {
                    Tanimla();
                    ModelState.AddModelError("Miktar", "Pozitif bir miktar giriniz");
                    return View(model);
                }
                else if (model.Miktar == 0)
                {
                    Tanimla();
                    ModelState.AddModelError("Miktar", "Pozitif bir miktar giriniz");
                    return View(model);
                }

                try
                {
                    String ilkYari = model.KartNo.Substring(0, 8);
                    String ikinciYari = model.KartNo.Substring(8, 8);
                    Convert.ToInt32(ilkYari);
                    Convert.ToInt32(ikinciYari);
                }
                catch (Exception)
                {
                    Tanimla();
                    ModelState.AddModelError("KartNo", "Kart No sadece rakamlardan oluşabilir");
                    return View(model);
                }
                if (model.GuvenlikKodu != null)
                {
                    try
                    {
                        Convert.ToInt32(model.GuvenlikKodu);
                    }
                    catch (Exception)
                    {
                        Tanimla();
                        ModelState.AddModelError("GuvenlikKodu", "Güvenlik kodu sadece sayılardan oluşabilir");
                        return View(model);
                    }
                }

                var sonuc = kasaBAL.DisardanKartIleBagis(model);
                if (sonuc.TamamlandiMi == true)
                {
                    TempData["uyari"] =
                        "Teşekkür ederiz. Nakdi bağışınız alındı. Verdiğiniz bilgiler doğrultusunda sistemimizde hesabınız oluşturuldu. Dilerseniz giriş yapabilir ve bugüne kadar ne kadar nakdi bağış yaptığınızı öğrenebilirsiniz.";
                    return RedirectToAction("AnaSayfa");
                }
                else
                {
                    string hatalar = KullaniciBilgileriDondur.HataMesajlariniOku(sonuc.HataMesajlari);
                    TempData["hata"] = hatalar;
                    Tanimla();
                    return View(model);
                }
            }
            else
            {
                Tanimla();
                return View(model);
            }
        }

        public void Tanimla()
        {
            var sehirlerSelectList = kullaniciBAL.TumSehirleriGetir().Select(p => new SelectListItem()
            {
                Text=p.SehirAdi,
                Value=p.SehirId.ToString()
            }).ToList();
            ViewBag.sehirlerSelectList = sehirlerSelectList;
            List<SelectListItem> ay = new List<SelectListItem>();
            for (int i = 1; i < 13; i++)
            {
                ay.Add(new SelectListItem() { Value = i.ToString(), Text = i.ToString() });
            }

            ViewBag.aySelectList = ay;
            List<SelectListItem> yil = new List<SelectListItem>();
            for (int i = 19; i < 28; i++)
            {
                yil.Add(new SelectListItem() { Value = i.ToString(), Text = i.ToString() });
            }

            ViewBag.yilSelectList = yil;
        }
    }
}