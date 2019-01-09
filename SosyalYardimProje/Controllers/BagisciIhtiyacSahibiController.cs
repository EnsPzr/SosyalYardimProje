using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BusinessLayer.Models.IhtiyacSahibiModelleri;
using BusinessLayer.Siniflar;
using SosyalYardimProje.Filters;

namespace SosyalYardimProje.Controllers
{
    [HataFiltresi]
    public class BagisciIhtiyacSahibiController : Controller
    {
        private IhtiyacSahibi ihtiyacSahibiBAL = new IhtiyacSahibi();
        private Kullanici kullaniciBAL = new Kullanici();
        [BagisciLoginFilter]
        public ActionResult Liste()
        {
            KullaniciBilgileriDondur.LogKaydet(0, "Bağışçı tarafından eklenen ihiyaç sahipleri görüntülendi. Bağışçı Id="+KullaniciBilgileriDondur.KullaniciId());
            var ihtiyacSahipleri = ihtiyacSahibiBAL.TumIhtiyacSahipleriniGetir(KullaniciBilgileriDondur.KullaniciId());
            return View(ihtiyacSahipleri);
        }
        [BagisciLoginFilter]
        public ActionResult Ekle()
        {
            Tanimla();
            return View();
        }
        [BagisciLoginFilter]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Ekle(IhtiyacSahibiModel model)
        {
            if (ModelState.IsValid)
            {
                var onay = ihtiyacSahibiBAL.IhtiyacSahibiKaydet(model,KullaniciBilgileriDondur.KullaniciId());
                if (onay.TamamlandiMi == true)
                {
                    KullaniciBilgileriDondur.LogKaydet(1, "Bağışçı tarafından ihtiyaç sahibi eklendi. "+model.IhtiyacSahibiAdi+" "+model.IhtiyacSahibiSoyadi);
                    TempData["uyari"] = "İhtiyaç sahibi ekleme işlemi başarı ile tamamlandı.";
                    return RedirectToAction("Liste");
                }
                else
                {
                    string hatalar = KullaniciBilgileriDondur.HataMesajlariniOku(onay.HataMesajlari);
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
        public ActionResult Duzenle(int? id)
        {
            if (id != null)
            {
                if (ihtiyacSahibiBAL.IhtiyacSahibiGoruntulenebilirMi(id, KullaniciBilgileriDondur.KullaniciId()))
                {
                    var ihtiyacSahibi = ihtiyacSahibiBAL.IhtiyacSahibiGetir(id);
                    if (ihtiyacSahibi != null)
                    {
                        KullaniciBilgileriDondur.LogKaydet(3, "Bağışçı tarafından ihtiyaç sahibi düzenlenmek üzere görüntülendi. "+ihtiyacSahibi.IhtiyacSahibiAdi+" "+ihtiyacSahibi.IhtiyacSahibiSoyadi);
                        Tanimla();
                        return View(ihtiyacSahibi);
                    }
                    else
                    {
                        TempData["hata"] = "İhtiyaç sahibi bulunamadı.";
                        return RedirectToAction("Liste");
                    }
                }
                else
                {
                    TempData["hata"] = "Sadece kendi eklediğiniz ihtiyaç sahipleri ile ilgili işlem yapabilirisiniz.";
                    return RedirectToAction("Liste");
                }
            }
            else
            {
                TempData["hata"] = "Lütfen güncellemek istediğiniz ihtiyaç sahibini seçiniz.";
                return RedirectToAction("Liste");
            }
        }
        [BagisciLoginFilter]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Duzenle(IhtiyacSahibiModel duzenlenmisIhtiyacSahibi)
        {
            if (ModelState.IsValid)
            {
                if (ihtiyacSahibiBAL.IhtiyacSahibiGoruntulenebilirMi(duzenlenmisIhtiyacSahibi.IhtiyacSahibiId, KullaniciBilgileriDondur.KullaniciId()))
                {
                    var ihtiyacSahibi = ihtiyacSahibiBAL.IhtiyacSahibiGetir(duzenlenmisIhtiyacSahibi.IhtiyacSahibiId);
                    if (ihtiyacSahibi != null)
                    {
                        var onay = ihtiyacSahibiBAL.IhtiyacSahibiGuncelle(duzenlenmisIhtiyacSahibi);
                        if (onay.TamamlandiMi == true)
                        {
                            KullaniciBilgileriDondur.LogKaydet(3, "Bağışçı tarafından ihtiyaç sahibi düzenlendi. "+ihtiyacSahibi.IhtiyacSahibiAdi+" "+ihtiyacSahibi.IhtiyacSahibiSoyadi);
                            TempData["uyari"] = "İhtiyaç sahibi güncelleme işlemi başarı ile sonuçlandı";
                            return RedirectToAction("Liste");
                        }
                        else
                        {
                            string hata = KullaniciBilgileriDondur.HataMesajlariniOku(onay.HataMesajlari);
                            TempData["hata"] = hata;
                            Tanimla();
                            return View(duzenlenmisIhtiyacSahibi);
                        }
                    }
                    else
                    {
                        TempData["hata"] =
                            "Güncellemek istediğiniz ihtiyaç sahibi bulunamadı.";
                        return RedirectToAction("Liste");
                    }
                }
                else
                {
                    TempData["hata"] =
                        "Sadece kendi eklediğiniz ihtiyaç sahipleri ile ilgili işlem yapabilirsiniz.";
                    return RedirectToAction("Liste");
                }
            }
            else
            {
                Tanimla();
                return View(duzenlenmisIhtiyacSahibi);
            }
        }
        [BagisciLoginFilter]
        public ActionResult Detay(int? id)
        {
            if (id != null)
            {
                if (ihtiyacSahibiBAL.IhtiyacSahibiGoruntulenebilirMi(id, KullaniciBilgileriDondur.KullaniciId()))
                {
                    if (ihtiyacSahibiBAL.IhtiyacSahibiGetir(id) != null)
                    {
                        var ihtiyacSahibi = ihtiyacSahibiBAL.IhtiyacSahibiGetir(id);
                        KullaniciBilgileriDondur.LogKaydet(4, "Bağışçı tarafından ihtiyaç sahibi detay görüntülendi." + ihtiyacSahibi.IhtiyacSahibiAdi+" "+ ihtiyacSahibi.IhtiyacSahibiSoyadi);
                        return View(ihtiyacSahibi);
                    }
                    else
                    {
                        TempData["hata"] = "Görüntülemek istediğiniz ihtiyaç sahibi bulunamadı";
                        return RedirectToAction("Liste", "IhtiyacSahibi");
                    }
                }
                else
                {
                    TempData["hata"] = "Görüntülemek istediğiniz ihtiyaç sahibi sizin bölgenizde bulunmuyor.";
                    return RedirectToAction("Liste", "IhtiyacSahibi");
                }
            }
            else
            {
                TempData["hata"] = "Görüntülek istediğiniz ihtiyaç sahibini seçiniz.";
                return RedirectToAction("Liste");
            }
        }

        public void Tanimla()
        {
            var sehirlerSelectList = kullaniciBAL.TumSehirleriGetir().Select(p => new SelectListItem()
            {
                Text = p.SehirAdi,
                Value = p.SehirId.ToString()
            }).ToList();
            ViewBag.sehirlerSelectList = sehirlerSelectList;
        }
    }
}