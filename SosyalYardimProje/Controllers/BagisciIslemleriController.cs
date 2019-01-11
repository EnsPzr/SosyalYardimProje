using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BusinessLayer;
using BusinessLayer.BagisciSiniflar;
using BusinessLayer.Models.BagisciGiris;
using BusinessLayer.Siniflar;
using SosyalYardimProje.Filters;

namespace SosyalYardimProje.Controllers
{
    [HataFiltresi]
    public class BagisciIslemleriController : Controller
    {
        private BagisciYonetimi bagisciBAL = new BagisciYonetimi();
        private Kullanici kullaniciBAL = new Kullanici();
        public ActionResult Giris()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Giris(BagisciGirisModel model)
        {
            if (ModelState.IsValid)
            {
                var bagisci = bagisciBAL.BagisciBul(model.Eposta, model.Sifre);
                if (bagisci != null)
                {
                    Session["KullaniciId"] = bagisci.KullaniciId;
                    Session["Bilgi"] = bagisci.KullaniciAdi + " " + bagisci.KullaniciSoyadi;
                    return RedirectToAction("AnaSayfa", "BagisciIslemleri");
                }
                else
                {
                    ModelState.AddModelError("","E Posta veya Şifre hatalı.");
                    return View(model);
                }
            }
            else
            {
                return View(model);
            }
        }

        [BagisciLoginFilter]
        public ActionResult AnaSayfa()
        {
            KullaniciBilgileriDondur.LogKaydet(6, "Bağışçı Giriş Yapıldı. Bağışçı Id=>"+KullaniciBilgileriDondur.KullaniciId());
            return View();
        }

        [ChildActionOnly]
        public PartialViewResult anaSayfaPartialGetir()
        {
            var anaSayfaModel = bagisciBAL.AnaSayfaBagislariGetir(KullaniciBilgileriDondur.KullaniciId());
            return PartialView(anaSayfaModel);
        }

        public ActionResult Kayit()
        {
            Tanimla();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Kayit(BagisciKayitModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (model.BagisciTelNo.Length > 8)
                    {
                        string tel1 = model.BagisciTelNo.Substring(0, 8);
                        string tel2 = model.BagisciTelNo.Substring(8, model.BagisciTelNo.Length - 8);
                        Convert.ToInt32(tel1);
                        Convert.ToInt32(tel2);
                    }
                    else
                    {
                        Convert.ToInt32(model.BagisciTelNo);
                    }
                }
                catch (Exception)
                {
                    ModelState.AddModelError("BagisciTelNo","Telefon numarası sadece rakamlardan oluşabilir.");
                    Tanimla();
                    return View(model);
                }

                var sonuc = bagisciBAL.BagisciKaydet(model);
                if (sonuc.TamamlandiMi == true)
                {
                    KullaniciBilgileriDondur.LogKaydet(5, "Bağışçı kayıt yapıldı. "+model.BagisciAdi+" "+model.BagisciSoyadi);
                    TempData["uyari"] = "Kayıt başarı ile tamamlandı.";
                    return RedirectToAction("Giris");
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

        [BagisciLoginFilter]
        public ActionResult ProfilDuzenle()
        {
            var bagisci = bagisciBAL.BagisciGetir(KullaniciBilgileriDondur.KullaniciId());
            if (bagisci != null)
            {
                Tanimla();
                return View(bagisci);
            }
            else
            {
                TempData["hata"] = "Hata oluştu";
                return RedirectToAction("AnaSayfa");
            }
        }

        [BagisciLoginFilter]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ProfilDuzenle(BagisciKayitModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.BagisciId == KullaniciBilgileriDondur.KullaniciId())
                {
                    try
                    {
                        if (model.BagisciTelNo.Length > 8)
                        {
                            string tel1 = model.BagisciTelNo.Substring(0, 8);
                            string tel2 = model.BagisciTelNo.Substring(8, model.BagisciTelNo.Length - 8);
                            Convert.ToInt32(tel1);
                            Convert.ToInt32(tel2);
                        }
                        else
                        {
                            Convert.ToInt32(model.BagisciTelNo);
                        }
                    }
                    catch (Exception)
                    {
                        ModelState.AddModelError("BagisciTelNo", "Telefon numarası sadece rakamlardan oluşabilir.");
                        Tanimla();
                        return View(model);
                    }

                    var sonuc = bagisciBAL.BagisciGuncelle(model);
                    if (sonuc.TamamlandiMi == true)
                    {
                        KullaniciBilgileriDondur.LogKaydet(3, "Bağışçı Kendi Profilini Güncelledi. "+model.BagisciAdi+" "+model.BagisciSoyadi);
                        TempData["uyari"] = "Profil Güncelleme başarı ile tamamlandı.";
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
                    TempData["hata"] = "Sadece kendi kullanıcınız için güncelleme yapabilirsiniz.";
                    return RedirectToAction("AnaSayfa");
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
            var sehirlerSelectList = kullaniciBAL.TumSehirleriGetir().Select(p=>new SelectListItem()
            {
                Text=p.SehirAdi,
                Value=p.SehirId.ToString()
            }).ToList();
            ViewBag.sehirlerSelectList = sehirlerSelectList;
        }
    }
}