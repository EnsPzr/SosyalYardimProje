using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using BusinessLayer;
using BusinessLayer.Models.AnaSayfaModelleri;
using BusinessLayer.Models.KasaModelleri;
using BusinessLayer.Siniflar;
using SosyalYardimProje.Filters;

namespace SosyalYardimProje.Controllers
{
    [HataFiltresi]
    public class KasaController : Controller
    {
        private Kasa kasaBAL = new Kasa();
        private Kullanici kullaniciBAL = new Kullanici();
        [KullaniciLoginFilter]
        public ActionResult Liste()
        {
            Tanimla();
            return View();
        }

        [KullaniciLoginFilter]
        [HttpGet]
        public JsonResult TumKasaGetir()
        {
            KasaJsModel model = new KasaJsModel()
            {
                BasariliMi = true,
                KasaList = kasaBAL.TumKasaGetir(KullaniciBilgileriDondur.KullaniciId())
            };
            model.KasaSayisi = model.KasaList.Count;
            Thread.Sleep(2000);
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        [KullaniciLoginFilter]
        [HttpGet]
        public JsonResult FiltreliKasaGetir(string aranan, string tarih, int? sehirId, int? gelirGider)
        {
            if (aranan.Equals(""))
            {
                aranan = null;
            }

            if (tarih.Equals(""))
            {
                tarih = null;
            }
            else
            {
                try
                {
                    Convert.ToDateTime(tarih);
                }
                catch (Exception)
                {
                    tarih = null;
                }
            }
            KasaJsModel model = new KasaJsModel()
            {
                BasariliMi = true,
                KasaList = kasaBAL.FiltreliKasaGetir(KullaniciBilgileriDondur.KullaniciId(), aranan, tarih, sehirId, gelirGider)
            };
            model.KasaSayisi = model.KasaList.Count;
            Thread.Sleep(2000);
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        [KullaniciLoginFilter]
        public ActionResult Ekle()
        {
            Tanimla();
            return View();
        }

        [KullaniciLoginFilter]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Ekle(KasaModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.Tarih.HasValue)
                {
                    try
                    {
                        Convert.ToDateTime(model.Tarih);
                    }
                    catch (Exception)
                    {
                        Tanimla();
                        ModelState.AddModelError("Tarih", "Tarih formatı geçerli değil.");
                        return View(model);
                    }
                }

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
                    Convert.ToDouble(model.Miktar);
                }
                catch (Exception)
                {
                    Tanimla();
                    ModelState.AddModelError("Miktar", "Lütfen geçerli bir miktar giriniz");
                    return View(model);
                }

                model.KullaniciId = KullaniciBilgileriDondur.KullaniciId();
                var onay = kasaBAL.KasaKaydet(KullaniciBilgileriDondur.KullaniciId(), model);
                if (onay.TamamlandiMi == true)
                {
                    TempData["uyari"] = "İşlem başarı ile tamamlandı.";
                    return RedirectToAction("Liste");
                }
                else
                {
                    string hatalar = KullaniciBilgileriDondur.HataMesajlariniOku(onay.HataMesajlari);
                    TempData["hata"] = hatalar;
                    return RedirectToAction("Liste");
                }
            }
            else
            {
                Tanimla();
                return View(model);
            }
        }

        [KullaniciLoginFilter]
        public ActionResult Duzenle(int? id)
        {
            if (id != null)
            {
                var kasa = kasaBAL.KasaGetir(id);
                if (kasa != null)
                {
                    if (kasaBAL.KullaniciIslemYapabilirMi(KullaniciBilgileriDondur.KullaniciId(), kasa.Sehir.SehirId))
                    {
                        Tanimla();
                        return View(kasa);
                    }
                    else
                    {
                        TempData["hata"] = "Sedece kendi bölgeniz için işlem yapabilirsiniz.";
                        return RedirectToAction("Liste");
                    }
                }
                else
                {
                    TempData["hata"] = "Düzenlemek istediğiniz kasa işlemi bulunamadı.";
                    return RedirectToAction("Liste");
                }
            }
            else
            {
                TempData["hata"] = "Lütfen düzenlemek istediğiniz kasa işlemini seçiniz";
                return RedirectToAction("Liste");
            }
        }

        [KullaniciLoginFilter]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Duzenle(KasaModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.Tarih.HasValue)
                {
                    try
                    {
                        Convert.ToDateTime(model.Tarih);
                    }
                    catch (Exception)
                    {
                        Tanimla();
                        ModelState.AddModelError("Tarih", "Tarih formatı geçerli değil.");
                        return View(model);
                    }
                }

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
                    Convert.ToDouble(model.Miktar);
                }
                catch (Exception)
                {
                    Tanimla();
                    ModelState.AddModelError("Miktar", "Lütfen geçerli bir miktar giriniz");
                    return View(model);
                }

                model.KullaniciId = KullaniciBilgileriDondur.KullaniciId();
                var onay = kasaBAL.KasaIslemGuncelle(KullaniciBilgileriDondur.KullaniciId(), model);
                if (onay.TamamlandiMi == true)
                {
                    TempData["uyari"] = "İşlem başarı ile tamamlandı.";
                    return RedirectToAction("Liste");
                }
                else
                {
                    string hatalar = KullaniciBilgileriDondur.HataMesajlariniOku(onay.HataMesajlari);
                    TempData["hata"] = hatalar;
                    return RedirectToAction("Liste");
                }
            }
            else
            {
                Tanimla();
                return View(model);
            }
        }

        [KullaniciLoginFilter]
        public ActionResult Sil(int? id)
        {
            if (id != null)
            {
                var kasa = kasaBAL.KasaGetir(id);
                if (kasa != null)
                {
                    if (kasaBAL.KullaniciIslemYapabilirMi(KullaniciBilgileriDondur.KullaniciId(), kasa.Sehir.SehirId))
                    {
                        Tanimla();
                        return View(kasa);
                    }
                    else
                    {
                        TempData["hata"] = "Sedece kendi bölgeniz için işlem yapabilirsiniz.";
                        return RedirectToAction("Liste");
                    }
                }
                else
                {
                    TempData["hata"] = "Düzenlemek istediğiniz kasa işlemi bulunamadı.";
                    return RedirectToAction("Liste");
                }
            }
            else
            {
                TempData["hata"] = "Lütfen düzenlemek istediğiniz kasa işlemini seçiniz";
                return RedirectToAction("Liste");
            }
        }

        [KullaniciLoginFilter]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult KasaSil(int? id)
        {
            if (id != null)
            {
                var kasa = kasaBAL.KasaGetir(id);
                if (kasa != null)
                {
                    if (kasaBAL.KullaniciIslemYapabilirMi(KullaniciBilgileriDondur.KullaniciId(), kasa.Sehir.SehirId))
                    {
                        var sonuc = kasaBAL.KasaSil(KullaniciBilgileriDondur.KullaniciId(), id);
                        if (sonuc.TamamlandiMi == true)
                        {
                            TempData["uyari"] = "İşlem başarı ile tamamlandı.";
                            return RedirectToAction("Liste");
                        }
                        else
                        {
                            string hatalar = KullaniciBilgileriDondur.HataMesajlariniOku(sonuc.HataMesajlari);
                            TempData["hata"] = hatalar;
                            return View(kasa);
                        }
                    }
                    else
                    {
                        TempData["hata"] = "Sedece kendi bölgeniz için işlem yapabilirsiniz.";
                        return RedirectToAction("Liste");
                    }
                }
                else
                {
                    TempData["hata"] = "Düzenlemek istediğiniz kasa işlemi bulunamadı.";
                    return RedirectToAction("Liste");
                }
            }
            else
            {
                TempData["hata"] = "Lütfen düzenlemek istediğiniz kasa işlemini seçiniz";
                return RedirectToAction("Liste");
            }
        }

        [KullaniciLoginFilter]
        public ActionResult KartIleEkle()
        {
            Tanimla();
            return View();
        }
        [KullaniciLoginFilter]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult KartIleEkle(KrediKartiKasaModel model)
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

                var sonuc = kasaBAL.KrediKartiEkleme(model);
                if (sonuc.TamamlandiMi == true)
                {
                    TempData["uyari"] = "Bağış ekleme işlemi başarı ile gerçekleşti.";
                    return RedirectToAction("Liste");
                }
                else
                {
                    String hatalar = KullaniciBilgileriDondur.HataMesajlariniOku(sonuc.HataMesajlari);
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
        public ActionResult BagisciKasaListe()
        {
            Tanimla();
            return View();
        }
        [BagisciLoginFilter]
        public ActionResult KartBagis()
        {
            var kullanici = kullaniciBAL.KullaniciGetir(KullaniciBilgileriDondur.KullaniciId());
            KrediKartiKasaModel model = new KrediKartiKasaModel();
            model.BagisciAdi = kullanici.KullaniciAdi;
            model.BagisciSoyadi = kullanici.KullaniciSoyadi;
            model.BagisciEPosta = kullanici.KullaniciEPosta;
            model.BagisciTelNo = kullanici.KullaniciTelNo;
            Tanimla();
            return View(model);
        }
        [BagisciLoginFilter]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult KartBagis(KrediKartiKasaModel model)
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

                var sonuc = kasaBAL.KrediKartiEkleme(model);
                if (sonuc.TamamlandiMi == true)
                {
                    TempData["uyari"] = "Bağışınız alındı. Teşekkür ederiz.";
                    return RedirectToAction("BagisciKasaListe");
                }
                else
                {
                    String hatalar = KullaniciBilgileriDondur.HataMesajlariniOku(sonuc.HataMesajlari);
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
            var sehirler = kullaniciBAL.SehirleriGetir(KullaniciBilgileriDondur.KullaniciId()).Select(p =>
                new SelectListItem()
                {
                    Text = p.SehirAdi,
                    Value = p.SehirId.ToString()
                }).ToList();
            ViewBag.sehirlerSelect = sehirler;
            ViewBag.sehirlerSelect2 = sehirler;
            var gelirGider = new List<SelectListItem>();
            gelirGider.Add(new SelectListItem() { Text = "Tümü", Value = "0" });
            gelirGider.Add(new SelectListItem() { Text = "Gelir", Value = "1" });
            gelirGider.Add(new SelectListItem() { Text = "Gider", Value = "2" });
            ViewBag.gelirGiderSelect = gelirGider;
            var gelirGider2 = new List<SelectListItem>();
            //gelirGider2.Add(new SelectListItem() { Text = "Tümü", Value = "0" });
            gelirGider2.Add(new SelectListItem() { Text = "Gelir", Value = "1" });
            gelirGider2.Add(new SelectListItem() { Text = "Gider", Value = "2" });
            ViewBag.gelirGiderSelect2 = gelirGider2;
            List<SelectListItem> ay= new List<SelectListItem>();
            for (int i = 1; i < 13; i++)
            {
                ay.Add(new SelectListItem(){Value = i.ToString(), Text = i.ToString()});
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