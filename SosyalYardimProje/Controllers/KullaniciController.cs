using System;
using System.Linq;
using System.Threading;
using System.Web.Mvc;
using BusinessLayer.Models.KullaniciModelleri;
using BusinessLayer.Siniflar;
using SosyalYardimProje.Filters;

namespace SosyalYardimProje.Controllers
{
    [HataFiltresi]
    public class KullaniciController : Controller
    {
        private readonly string sadeceGorevli =
            "Sadece görevli olduğunuz bölgedeki kullanıcılar ile ilgili işlem yapabilirsiniz.";
        Kullanici kullaniciBusinessLayer = new Kullanici();
        [KullaniciLoginFilter]
        public ActionResult Liste()
        {
            KullaniciBilgileriDondur.LogKaydet(0, "Kullanıcı Listesi Görüntülendi.");
            Tanimla();
            return View(new KullaniciModel());
        }
        [SadeceLoginFilter]
        [HttpGet]
        public JsonResult KullanicilariGetir()
        {
            var jsKullaniciModel = new KullaniciJSModel()
            {
                KullaniciModelList =
                    kullaniciBusinessLayer.TumKullanicilariGetir(KullaniciBilgileriDondur.KullaniciId()),
                BasariliMi = true
            };
            jsKullaniciModel.KullaniciSayisi = jsKullaniciModel.KullaniciModelList.Count;
            Thread.Sleep(2000);
            return Json(jsKullaniciModel, JsonRequestBehavior.AllowGet);
        }

        [SadeceLoginFilter]
        [HttpGet]
        public JsonResult FiltreliKullanicilariGetir(KullaniciFiltrelemeModel filtreliKullaniciModel)
        {

            var jsKullaniciModel = new KullaniciJSModel()
            {
                KullaniciModelList =
                    kullaniciBusinessLayer.FiltreliKullanicilariGetir(filtreliKullaniciModel.AraTxt, filtreliKullaniciModel.SehirId, KullaniciBilgileriDondur.KullaniciId(), filtreliKullaniciModel.OnayliMi, filtreliKullaniciModel.MerkezdeMi, filtreliKullaniciModel.OnayliMi),
                BasariliMi = true
            };
            jsKullaniciModel.KullaniciSayisi = jsKullaniciModel.KullaniciModelList.Count;
            Thread.Sleep(2000);
            return Json(jsKullaniciModel, JsonRequestBehavior.AllowGet);
        }

        [KullaniciLoginFilter]
        [HttpGet]
        public ActionResult Ekle()
        {
            MerkezdeGosterilecekMi();
            return View(new KullaniciModel());
        }
        [KullaniciLoginFilter]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Ekle(KullaniciModel yeniKullanici)
        {
            yeniKullanici.KullaniciSifre = "123456";
            yeniKullanici.KullaniciSifreTekrar = "123456";
            yeniKullanici.AktifMi = true;
            yeniKullanici.KullaniciOnayliMi = true;
            if (ModelState.IsValid)
            {
                if (KullaniciBilgileriDondur.KullaniciMerkezdeMi()==true)
                {
                    if (!kullaniciBusinessLayer.KullaniciVarMi(yeniKullanici.KullaniciEPosta))
                    {
                        if (ValidateIdentityNumber(yeniKullanici.KullaniciTCKimlik))
                        {
                            if (kullaniciBusinessLayer.KullaniciEkle(yeniKullanici))
                            {
                                KullaniciBilgileriDondur.LogKaydet(1, "Kullanıcı Eklendi. Adı Soyadı=>"+yeniKullanici.KullaniciAdi+" "+yeniKullanici.KullaniciSoyadi);
                                TempData["uyari"] = yeniKullanici.KullaniciAdi + " " + yeniKullanici.KullaniciSoyadi +
                                                    " kullanıcısı başarı ile kayıt edildi";
                                return RedirectToAction("Liste", "Kullanici");
                            }
                            else
                            {
                                TempData["hata"] = "Ekleme işlemi sırasında hata oluştu.";
                                MerkezdeGosterilecekMi();
                                return View(yeniKullanici);
                            }
                        }
                        else
                        {
                            ModelState.AddModelError("KullaniciTCKimlik", "Lütfen geçerli bir TC Kimlik numarası giriniz.");
                            MerkezdeGosterilecekMi();
                            return View(yeniKullanici);
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("KullaniciEPosta", "E Posta adresi kullanımda.");
                        MerkezdeGosterilecekMi();
                        return View(yeniKullanici);
                    }
                }
                else
                {
                    if (!kullaniciBusinessLayer.KullaniciVarMi(yeniKullanici.KullaniciEPosta))
                    {
                        if (yeniKullanici.Sehir.SehirId ==
                            KullaniciBilgileriDondur.KullaniciSehir())
                        {
                            if (ValidateIdentityNumber(yeniKullanici.KullaniciTCKimlik))
                            {
                                if (kullaniciBusinessLayer.KullaniciEkle(yeniKullanici))
                                {
                                    TempData["uyari"] = yeniKullanici.KullaniciAdi + " " + yeniKullanici.KullaniciSoyadi +
                                                        " kullanıcısı başarı ile kayıt edildi";
                                    return RedirectToAction("Liste", "Kullanici");
                                }
                                else
                                {
                                    TempData["hata"] = "Ekleme işlemi sırasında hata oluştu.";
                                    MerkezdeGosterilecekMi();
                                    return View(yeniKullanici);
                                }
                            }
                            else
                            {
                                ModelState.AddModelError("KullaniciTCKimlik", "Lütfen geçerli bir TC Kimlik numarası giriniz.");
                                MerkezdeGosterilecekMi();
                                return View(yeniKullanici);
                            }
                        }
                        else
                        {
                            TempData["hata"] = "Kullanıcıyı sadece görevli olduğunuz şehire ekleyebilirsiniz";
                            MerkezdeGosterilecekMi();
                            return View(yeniKullanici);
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("KullaniciEPosta", "E Posta adresi kullanımda.");
                        MerkezdeGosterilecekMi();
                        return View(yeniKullanici);
                    }
                }
            }
            else
            {
                MerkezdeGosterilecekMi();
                return View(yeniKullanici);
            }
        }

        [HttpGet]
        [KullaniciLoginFilter]
        public ActionResult Sil(int? id)
        {
            if (id != null)
            {
                var kullanici = kullaniciBusinessLayer.KullaniciGetir(id);
                if (kullanici != null)
                {
                    if (Convert.ToBoolean(KullaniciBilgileriDondur.KullaniciMerkezdeMi()))
                    {
                        return View(kullanici);
                    }
                    else
                    {
                        if (kullanici.Sehir.SehirId ==
                            KullaniciBilgileriDondur.KullaniciSehir())
                        {
                            KullaniciBilgileriDondur.LogKaydet(2, "Kullanıcı Silmek için Görüntülendi. Adı Soyadı=>"+kullanici.KullaniciAdi+" "+kullanici.KullaniciSoyadi);
                            return View(kullanici);
                        }
                        else
                        {
                            TempData["hata"] = sadeceGorevli;
                            return RedirectToAction("Liste", "Kullanici");
                        }
                    }
                }
                else
                {
                    TempData["hata"] = "Lütfen silmek istediğiniz kullanıcıyı seçiniz";
                    return RedirectToAction("Liste");
                }
            }
            else
            {
                TempData["hata"] = "Lütfen silmek istediğiniz kullanıcıyı seçiniz";
                return RedirectToAction("Liste");
            }
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        [KullaniciLoginFilter]
        [Route("Sil/{id}")]
        public ActionResult KullaniciSil(int? id)
        {
            if (id != null)
            {
                var kullanici = kullaniciBusinessLayer.KullaniciGetir(id);
                if (kullanici != null)
                {
                    if (Convert.ToBoolean(KullaniciBilgileriDondur.KullaniciMerkezdeMi()))
                    {
                        if (kullaniciBusinessLayer.KullaniciSil(id))
                        {
                            KullaniciBilgileriDondur.LogKaydet(2, "Kullanıcı Silindi. Adı Soyadı=>" + kullanici.KullaniciAdi + " " + kullanici.KullaniciSoyadi);
                            TempData["uyari"] = "Kullanıcı silme işlemi başarı ile tamamlandı";
                            return RedirectToAction("Liste");
                        }
                        else
                        {
                            TempData["hata"] = "Bilinmeyen bir hata oluştu";
                            return RedirectToAction("Sil", "Kullanici", new { id });
                        }
                    }
                    else
                    {
                        if (kullanici.Sehir.SehirId ==
                            KullaniciBilgileriDondur.KullaniciSehir())
                        {
                            if (kullaniciBusinessLayer.KullaniciSil(id))
                            {
                                TempData["uyari"] = "Kullanıcı silme işlemi başarı ile tamamlandı";
                                return RedirectToAction("Liste");
                            }
                            else
                            {
                                TempData["hata"] = "Bilinmeyen bir hata oluştu";
                                return RedirectToAction("Sil", "Kullanici", new { id });
                            }
                        }
                        else
                        {
                            TempData["hata"] = sadeceGorevli;
                            return RedirectToAction("Liste", "Kullanici");
                        }
                    }
                }
                else
                {
                    TempData["hata"] = "Lütfen silmek istediğiniz kullanıcıyı seçiniz";
                    return RedirectToAction("Liste");
                }
            }
            else
            {
                TempData["hata"] = "Lütfen silmek istediğiniz kullanıcıyı seçiniz";
                return RedirectToAction("Liste");
            }
        }

        [KullaniciLoginFilter]
        [HttpGet]
        public ActionResult Duzenle(int? id)
        {
            MerkezdeGosterilecekMi();
            if (id != null)
            {
                var kullanici = kullaniciBusinessLayer.KullaniciGetir(id);
                if (kullanici != null)
                {
                    if (Convert.ToBoolean(KullaniciBilgileriDondur.KullaniciMerkezdeMi()))
                    {
                        return View(kullanici);
                    }
                    else
                    {
                        if (kullanici.Sehir.SehirId ==
                            KullaniciBilgileriDondur.KullaniciSehir())
                        {
                            return View(kullanici);
                        }
                        else
                        {
                            TempData["hata"] = sadeceGorevli;
                            return RedirectToAction("Liste", "Kullanici");
                        }
                    }
                }
                else
                {
                    TempData["hata"] = "Lütfen silmek istediğiniz kullanıcıyı seçiniz";
                    return RedirectToAction("Liste");
                }
            }
            else
            {
                TempData["hata"] = "Lütfen silmek istediğiniz kullanıcıyı seçiniz";
                return RedirectToAction("Liste");
            }
        }

        [ValidateAntiForgeryToken]
        [KullaniciLoginFilter]
        [HttpPost]
        public ActionResult Duzenle(KullaniciModel duzenlenmisKullanici)
        {
            if (ModelState.IsValid)
            {
                var kullanici = kullaniciBusinessLayer.KullaniciGetir(duzenlenmisKullanici.KullaniciId);
                if (kullanici != null)
                {
                    if (KullaniciBilgileriDondur.KullaniciMerkezdeMi() == true)
                    {
                        if (kullaniciBusinessLayer.KullaniciVarMi(duzenlenmisKullanici.KullaniciEPosta, duzenlenmisKullanici.KullaniciId))
                        {
                            if (ValidateIdentityNumber(duzenlenmisKullanici.KullaniciTCKimlik))
                            {
                                duzenlenmisKullanici.AktifMi = true;
                                if (kullaniciBusinessLayer.KullaniciGuncelle(duzenlenmisKullanici))
                                {
                                    KullaniciBilgileriDondur.LogKaydet(3, "Kullanıcı Düzenlendi. Adı Soyadı=>" + kullanici.KullaniciAdi + " " + kullanici.KullaniciSoyadi+" Kullanıcı Id=>"+kullanici.KullaniciId);
                                    TempData["uyari"] = duzenlenmisKullanici.KullaniciAdi + " " + duzenlenmisKullanici.KullaniciSoyadi +
                                                        " kullanıcısı başarı ile düzenlendi.";
                                    return RedirectToAction("Liste", "Kullanici");
                                }
                                else
                                {
                                    TempData["hata"] = "Güncelleme işlemi sırasında hata oluştu. Aynı E Posta hesabına ait başka bir hesap olabilir. Lütfen teyit ediniz.";
                                    MerkezdeGosterilecekMi();
                                    return View(duzenlenmisKullanici);
                                }
                            }
                            else
                            {
                                ModelState.AddModelError("KullaniciTCKimlik", "Lütfen geçerli bir TC Kimlik numarası giriniz.");
                                MerkezdeGosterilecekMi();
                                return View(duzenlenmisKullanici);
                            }
                        }
                        else
                        {
                            ModelState.AddModelError("KullaniciEPosta", "E Posta adresi kullanımda.");
                            MerkezdeGosterilecekMi();
                            return View(duzenlenmisKullanici);
                        }
                    }
                    else
                    {
                        if (!kullaniciBusinessLayer.KullaniciVarMi(duzenlenmisKullanici.KullaniciEPosta))
                        {
                            if (duzenlenmisKullanici.Sehir.SehirId ==
                                KullaniciBilgileriDondur.KullaniciSehir())
                            {
                                if (ValidateIdentityNumber(duzenlenmisKullanici.KullaniciTCKimlik))
                                {
                                    if (kullaniciBusinessLayer.KullaniciGuncelle(duzenlenmisKullanici))
                                    {
                                        TempData["uyari"] = duzenlenmisKullanici.KullaniciAdi + " " + duzenlenmisKullanici.KullaniciSoyadi +
                                                            " kullanıcısı başarı ile güncellendi.";
                                        return RedirectToAction("Liste", "Kullanici");
                                    }
                                    else
                                    {
                                        TempData["hata"] = "Güncelleme işlemi sırasında hata oluştu.";
                                        MerkezdeGosterilecekMi();
                                        return View(duzenlenmisKullanici);
                                    }
                                }
                                else
                                {
                                    ModelState.AddModelError("KullaniciTCKimlik", "Lütfen geçerli bir TC Kimlik numarası giriniz.");
                                    MerkezdeGosterilecekMi();
                                    return View(duzenlenmisKullanici);
                                }
                            }
                            else
                            {
                                TempData["hata"] = "Sadece görevli olduğunuz bölgelerdeki kullanıcıları düzenleyebilirsiniz.";
                                MerkezdeGosterilecekMi();
                                return View(duzenlenmisKullanici);
                            }
                        }
                        else
                        {
                            ModelState.AddModelError("KullaniciEPosta", "E Posta adresi kullanımda.");
                            MerkezdeGosterilecekMi();
                            return View(duzenlenmisKullanici);
                        }
                    }
                }
                else
                {
                    TempData["hata"] = "Düzenlenecek kullanıcıyı seçiniz";
                    return RedirectToAction("Liste", "Kullanici");
                }
            }
            else
            {
                MerkezdeGosterilecekMi();
                return View(duzenlenmisKullanici);
            }
        }

        [KullaniciLoginFilter]
        [HttpGet]
        public ActionResult Detay(int? id)
        {
            if (id != null)
            {
                var kullanici = kullaniciBusinessLayer.KullaniciGetir(id);
                if (kullanici != null)
                {
                    if (Convert.ToBoolean(KullaniciBilgileriDondur.KullaniciMerkezdeMi()))
                    {
                        return View(kullanici);
                    }
                    else
                    {
                        if (kullanici.Sehir.SehirId ==
                            KullaniciBilgileriDondur.KullaniciSehir())
                        {
                            KullaniciBilgileriDondur.LogKaydet(4, "Kullanıcı Detay Görüntülendi. Adı Soyadı=>" + kullanici.KullaniciAdi + " " + kullanici.KullaniciSoyadi);
                            return View(kullanici);
                        }
                        else
                        {
                            TempData["hata"] = sadeceGorevli;
                            return RedirectToAction("Liste", "Kullanici");
                        }
                    }
                }
                else
                {
                    TempData["hata"] = "Lütfen görüntülemek istediğiniz kullanıcıyı seçiniz";
                    return RedirectToAction("Liste");
                }
            }
            else
            {
                TempData["hata"] = "Lütfen görüntülemek istediğiniz kullanıcıyı seçiniz";
                return RedirectToAction("Liste");
            }
        }
        public void MerkezdeGosterilecekMi()
        {
            var kullaniciMerkezdeMi = KullaniciBilgileriDondur.KullaniciMerkezdeMi();
            ViewBag.GosterilecekMi = kullaniciMerkezdeMi == null ? false : kullaniciMerkezdeMi == true ? true : false;
            Tanimla();
        }

        [KullaniciLoginFilter]
        [HttpGet]
        public ActionResult BilgilerimiGuncelle()
        {
            var kullanici = kullaniciBusinessLayer.KullaniciGetir(KullaniciBilgileriDondur.KullaniciId());
            return View(kullanici);
        }

        [KullaniciLoginFilter]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult BilgilerimiGuncelle(KullaniciModel model)
        {
            if (model.KullaniciSifre == null)
            {
                model.KullaniciSifre = "123456";
                model.KullaniciSifreTekrar = "123456";
            }
            if (ModelState.IsValid)
            {
                var sonuc = kullaniciBusinessLayer.BilgilerimiGuncelle(model);
                if (sonuc == true)
                {
                    TempData["uyari"] = "İşlem başarı ile tamamlandı.";
                    return View(model);
                }
                else
                {
                    TempData["hata"] = "Düzenleme işlemi sırasında hata oluştu.";
                    return View(model);
                }
            }
            else
            {
                return View(model);
            }
        }

        public void Tanimla()
        {
            var sehirler = kullaniciBusinessLayer.SehirleriGetir(KullaniciBilgileriDondur.KullaniciId()).Select(p =>
                new SelectListItem()
                {
                    Text = p.SehirAdi,
                    Value = p.SehirId.ToString()
                }).ToList();
            ViewBag.sehirler = sehirler;
        }
        public bool ValidateIdentityNumber(string identityNo)
        {
            bool result = false;
            try
            {
                if (!string.IsNullOrEmpty(identityNo) && identityNo.Length == 11 && identityNo[0] != '0')
                {
                    Int64 ATCNO = 0, BTCNO = 0, identityNoInt = 0;
                    long C1 = 0, C2 = 0, C3 = 0, C4 = 0, C5 = 0, C6 = 0, C7 = 0, C8 = 0, C9 = 0, Q1 = 0, Q2 = 0;
                    identityNoInt = Int64.Parse(identityNo);
                    ATCNO = identityNoInt / 100;
                    BTCNO = identityNoInt / 100;
                    C1 = ATCNO % 10; ATCNO = ATCNO / 10;
                    C2 = ATCNO % 10; ATCNO = ATCNO / 10;
                    C3 = ATCNO % 10; ATCNO = ATCNO / 10;
                    C4 = ATCNO % 10; ATCNO = ATCNO / 10;
                    C5 = ATCNO % 10; ATCNO = ATCNO / 10;
                    C6 = ATCNO % 10; ATCNO = ATCNO / 10;
                    C7 = ATCNO % 10; ATCNO = ATCNO / 10;
                    C8 = ATCNO % 10; ATCNO = ATCNO / 10;
                    C9 = ATCNO % 10; ATCNO = ATCNO / 10;
                    Q1 = ((10 - ((((C1 + C3 + C5 + C7 + C9) * 3) + (C2 + C4 + C6 + C8)) % 10)) % 10);
                    Q2 = ((10 - (((((C2 + C4 + C6 + C8) + Q1) * 3) + (C1 + C3 + C5 + C7 + C9)) % 10)) % 10);

                    result = ((BTCNO * 100) + (Q1 * 10) + Q2 == identityNoInt);
                }
            }
            catch (Exception)
            {
            }
            return result;
        }
    }
}