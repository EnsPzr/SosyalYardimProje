using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using BusinessLayer;
using BusinessLayer.Models.AnaSayfaModelleri;
using BusinessLayer.Models.IhtiyacSahibiModelleri;
using BusinessLayer.Siniflar;
using SosyalYardimProje.Filters;
using Exception = System.Exception;

namespace SosyalYardimProje.Controllers
{
    [HataFiltresi]
    public class IhtiyacSahibiController : Controller
    {
        private IhtiyacSahibi ihtiyacSahibiBAL = new IhtiyacSahibi();
        private Kullanici kullaniciBusinessLayer = new Kullanici();
        [KullaniciLoginFilter]
        public ActionResult Liste()
        {
            Tanimla();
            return View(new IhtiyacSahibiModel());
        }
        [SadeceLoginFilter]
        public JsonResult IhtiyacSahipleriniGetir()
        {
            IhtiyacSahibiJSModel ihtiyacSahibiModel = new IhtiyacSahibiJSModel();
            ihtiyacSahibiModel.IhtiyacSahipleri =
                ihtiyacSahibiBAL.TumIhtiyacSahipleriniGetir(KullaniciBilgileriDondur.KullaniciId());
            ihtiyacSahibiModel.BasariliMi = true;
            ihtiyacSahibiModel.IhtiyacSahibiSayisi = ihtiyacSahibiModel.IhtiyacSahipleri.Count;
            Thread.Sleep(2000);
            return Json(ihtiyacSahibiModel, JsonRequestBehavior.AllowGet);
        }
        [SadeceLoginFilter]
        public JsonResult FiltreliIhtiyacSahipleriniGetir(String IhtiyacSahibiAranan, int? SehirId)
        {
            IhtiyacSahibiJSModel ihtiyacSahibiModel = new IhtiyacSahibiJSModel();
            ihtiyacSahibiModel.IhtiyacSahipleri =
                ihtiyacSahibiBAL.FiltreliIhtiyacSahibiListesiniGetir(IhtiyacSahibiAranan, SehirId, KullaniciBilgileriDondur.KullaniciId());
            ihtiyacSahibiModel.BasariliMi = true;
            ihtiyacSahibiModel.IhtiyacSahibiSayisi = ihtiyacSahibiModel.IhtiyacSahipleri.Count;
            Thread.Sleep(2000);
            return Json(ihtiyacSahibiModel, JsonRequestBehavior.AllowGet);
        }
        [KullaniciLoginFilter]
        public ActionResult Ekle()
        {
            Tanimla();
            return View();
        }

        [KullaniciLoginFilter]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Ekle(IhtiyacSahibiModel yeniIhtiyacSahibi)
        {
            if (ModelState.IsValid)
            {
                var onay = ihtiyacSahibiBAL.IhtiyacSahibiKaydet(yeniIhtiyacSahibi);
                if (onay.TamamlandiMi == true)
                {
                    TempData["uyari"] = "İhtiyaç sahibi ekleme işlemi başarı ile tamamlandı.";
                    return RedirectToAction("Liste");
                }
                else
                {
                    string hata = "";
                    foreach (var hataItem in onay.HataMesajlari)
                    {
                        hata = hata + "" + hataItem + "\n";
                    }

                    TempData["hata"] = hata;
                    Tanimla();
                    return View(yeniIhtiyacSahibi);
                }
            }
            else
            {
                Tanimla();
                return View(yeniIhtiyacSahibi);
            }
        }
        [KullaniciLoginFilter]
        public ActionResult Sil(int? id)
        {
            if (id != null)
            {
                if (ihtiyacSahibiBAL.IhtiyacSahibiGoruntulenebilirMi(id, KullaniciBilgileriDondur.KullaniciId()))
                {
                    var ihtiyacSahibi = ihtiyacSahibiBAL.IhtiyacSahibiGetir(id);
                    if (ihtiyacSahibi != null)
                    {
                        return View(ihtiyacSahibi);
                    }
                    else
                    {
                        TempData["hata"] =
                            "Görüntülemek istediğiniz ihtiyaç sahibi bulunamadı.";
                        return RedirectToAction("Liste");
                    }
                }
                else
                {
                    TempData["hata"] =
                        "Görüntelemeye çalıştığınız ihtiyaç sahibi sizin bölgenizde bulunmayan bir ihtiyaç sahibidir.";
                    return RedirectToAction("Liste");
                }
            }
            else
            {
                TempData["hata"] =
                    "Görüntülemek istediğiniz ihtiyaç sahibini seçiniz.";
                return RedirectToAction("Liste");
            }
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        [KullaniciLoginFilter]
        public ActionResult IhtiyacSahibiSil(int? id)
        {
            if (id != null)
            {
                if (ihtiyacSahibiBAL.IhtiyacSahibiGoruntulenebilirMi(id, KullaniciBilgileriDondur.KullaniciId()))
                {
                    var islemSonucu = ihtiyacSahibiBAL.IhtiyacSahibiSil(id);
                    if (islemSonucu.TamamlandiMi == true)
                    {
                        TempData["uyari"] =
                            "Silme işlemi başarı ile sonuçlandı.";
                        return RedirectToAction("Liste");
                    }
                    else
                    {
                        string hata = "";
                        foreach (var hataItem in islemSonucu.HataMesajlari)
                        {
                            hata += hataItem + "\n";
                        }

                        TempData["hata"] = hata+" İhtiyaç sahibine eşya verildiyse veya maddi bağış yapıldıysa sistemden silemezsiniz.";
                        return RedirectToAction("Liste");
                    }
                }
                else
                {
                    TempData["hata"] =
                        "Silmeye çalıştığınız ihtiyaç sahibi sizin bölgenizde bulunmayan bir ihtiyaç sahibidir.";
                    return RedirectToAction("Liste");
                }
            }
            else
            {
                TempData["hata"] =
                    "Silmek istediğiniz ihtiyaç sahibini seçiniz.";
                return RedirectToAction("Liste");
            }
        }
        [KullaniciLoginFilter]
        public ActionResult Duzenle(int? id)
        {
            if (id != null)
            {
                if (ihtiyacSahibiBAL.IhtiyacSahibiGoruntulenebilirMi(id, KullaniciBilgileriDondur.KullaniciId()))
                {
                    if (ihtiyacSahibiBAL.IhtiyacSahibiGetir(id) != null)
                    {
                        Tanimla();
                        return View(ihtiyacSahibiBAL.IhtiyacSahibiGetir(id));
                    }
                    else
                    {
                        TempData["hata"] = "Düzenlemek istediğiniz ihtiyaç sahibi bulunamadı.";
                        return RedirectToAction("Liste");
                    }
                }
                else
                {
                    TempData["hata"] = "Düzenlemek istediğiniz ihtiyaç sahibi sizin bölgenizde bulunmuyor";
                    return RedirectToAction("Liste");
                }
            }
            else
            {
                TempData["hata"] = "Düzenlemek istediğiniz ihtiyaç sahibini seçiniz";
                return RedirectToAction("Liste");
            }
        }

        [KullaniciLoginFilter]
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
                            TempData["uyari"] = "İhtiyaç sahibi güncelleme işlemi başarı ile sonuçlandı";
                            return RedirectToAction("Liste");
                        }
                        else
                        {
                            string hata = "";
                            foreach (var hataItem in onay.HataMesajlari)
                            {
                                hata += hataItem + "\n";
                            }

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
                        "Güncellemeye çalıştığınız ihtiyaç sahibi sizin bölgenizde bulunmayan bir ihtiyaç sahibidir.";
                    return RedirectToAction("Liste");
                }
            }
            else
            {
                Tanimla();
                return View(duzenlenmisIhtiyacSahibi);
            }
        }

        [KullaniciLoginFilter]
        public ActionResult Detay(int? id)
        {
            if (id != null)
            {
                if (ihtiyacSahibiBAL.IhtiyacSahibiGoruntulenebilirMi(id, KullaniciBilgileriDondur.KullaniciId()))
                {
                    if (ihtiyacSahibiBAL.IhtiyacSahibiGetir(id) != null)
                    {
                        return View(ihtiyacSahibiBAL.IhtiyacSahibiGetir(id));
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
        [KullaniciLoginFilter]
        public ActionResult IhtiyacSahibiKontrolListesi()
        {
            Tanimla();
            return View();
        }
        [SadeceLoginFilter]
        public JsonResult TumIhtiyacSahipleriniGetir()
        {
            IhtiyacSahibiKontrolJSModel model = new IhtiyacSahibiKontrolJSModel()
            {
                BasariliMi = true,
                IhtiyacSahibiKontrolListe = ihtiyacSahibiBAL.KontrolEdilecekIhtiyacSahipleriniGetir(KullaniciBilgileriDondur.KullaniciId())
            };
            model.IhtiyacSahibiSayisi = model.IhtiyacSahibiKontrolListe.Count();
            Thread.Sleep(2000);
            return Json(model, JsonRequestBehavior.AllowGet);
        }
        [SadeceLoginFilter]
        public JsonResult FiltreliKontrolIhtiyacSahipleriniGetir(String aranan, int? sehirId, String tarih)
        {
            IhtiyacSahibiKontrolJSModel model = new IhtiyacSahibiKontrolJSModel()
            {
                BasariliMi = true,
                IhtiyacSahibiKontrolListe = ihtiyacSahibiBAL.KontrolEdilecekFiltreliIhtiyacSahipleriniGetir(KullaniciBilgileriDondur.KullaniciId(), aranan, sehirId, tarih)
            };
            model.IhtiyacSahibiSayisi = model.IhtiyacSahibiKontrolListe.Count();
            Thread.Sleep(2000);
            return Json(model, JsonRequestBehavior.AllowGet);
        }
        [KullaniciLoginFilter]
        public ActionResult Kontrol(int? id)
        {
            if (id != null)
            {
                if (ihtiyacSahibiBAL.KullaniciIslemYapabilirMi(KullaniciBilgileriDondur.KullaniciId(), id))
                {
                    var ihtiyacSahibi = ihtiyacSahibiBAL.IhtiyacSahibiVerileceklerGetir(id);
                    if (ihtiyacSahibi != null)
                    {
                        return View(ihtiyacSahibi);
                    }
                    else
                    {
                        TempData["hata"] = "İşlem yapmak istediğiniz ihtiyaç sahibi bulunamadı.";
                        return RedirectToAction("IhtiyacSahibiKontrolListesi");
                    }
                }
                else
                {
                    TempData["hata"] = "Sadece kendi bölgenizde bulunan ihtiyaç sahipleri ile ilgili işlem yapabilirsiniz";
                    return RedirectToAction("IhtiyacSahibiKontrolListesi");
                }
            }
            else
            {
                TempData["hata"] = "Lütfen işlem yapmak istediğiniz ihtiyaç sahibini seçiniz";
                return RedirectToAction("IhtiyacSahibiKontrolListesi");
            }
        }
        [KullaniciLoginFilter]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Kontrol(IhtiyacSahibiKontrolSayfaModel model)
        {
            if (model.MuhtacMi == true)
            {
                if (ModelState.IsValid)
                {
                    int sayac = 0;
                    for (int i = 0; i < model.verileceklerList.Count; i++)
                    {
                        try
                        {
                            Convert.ToInt32(model.verileceklerList[i].Adet);
                        }
                        catch (Exception)
                        {
                            sayac++;
                        }
                    }

                    if (sayac > 0)
                    {
                        TempData["hata"] = "Verilecek eşya adetleri sadece sayıdan oluşmalıdır.";
                        return View(model);
                    }

                    bool d = false;
                    try
                    {
                        Convert.ToDouble(model.NakdiBagisMiktari);
                        d = true;
                    }
                    catch (Exception)
                    {
                        
                    }

                    if (d == false)
                    {
                        TempData["hata"] = "Verilen nakdi bağış sadece virgül içeren bir sayıdan oluşabilir.";
                        return View(model);
                    }

                    d = true;


                    if (model.TahminiTeslim.HasValue)
                    {
                        try
                        {
                            Convert.ToDateTime(model.TahminiTeslim);
                            d = false;
                        }
                        catch (Exception)
                        {

                        }
                    }

                    if (d == true)
                    {
                        TempData["hata"] = "Tahmini teslim tarihi doğru formatta değil.";
                        return View(model);
                    }

                    sayac = 0;
                    int sayac2 = 0;
                    for (int i = 0; i < model.verileceklerList.Count; i++)
                    {
                        if (model.verileceklerList[i].Adet > 0)
                        {
                            sayac++;
                        }
                        else if (model.verileceklerList[i].Adet < 0)
                        {
                            sayac2++;
                        }
                    }
                    if (sayac2 > 0)
                    {
                        TempData["hata"] = "Verilecek eşya sayısı 0'dan küçük olamaz";
                        return View(model);
                    }
                    if (sayac > 0)
                    {
                        if (!(model.TahminiTeslim.HasValue))
                        {
                            ModelState.AddModelError("TahiminTeslimTarihi","Verilecek eşya seçildiğinden dolayı tahmini teslim tarihi seçilmedilir.");
                            return View(model);
                        }
                    }

                    if (ihtiyacSahibiBAL.KullaniciIslemYapabilirMi(KullaniciBilgileriDondur.KullaniciId(),
                        model.IhtiyacSahibiKontrolId))
                    {
                        if (!(ihtiyacSahibiBAL.TeslimTamamlandiMi(model.IhtiyacSahibiKontrolId)))
                        {
                            if (ihtiyacSahibiBAL.ihtiyacSahibiKontrolKaydet(model))
                            {
                                TempData["uyari"] = "İşlem başarı ile tamamlandı.";
                                return RedirectToAction("IhtiyacSahibiKontrolListesi");
                            }
                            else
                            {
                                TempData["hata"] = "İşlem sırasında hata oluştu.";
                                return View(model);
                            }
                        }
                        else
                        {
                            TempData["hata"] = "Teslim tamamlandıktan sonra güncelleme yapılamaz.";
                            return View(model);
                        }
                    }
                    else
                    {
                        TempData["hata"] = "Sadece kendi bölgenizdeki ihtiyaç sahipleri için işlem yapabilirsiniz.";
                        return RedirectToAction("IhtiyacSahibiKontrolListesi");
                    }
                }
                else
                {
                    return View(model);
                }
            }
            else
            {
                ModelState.AddModelError("MuhtacMi","Muhtaç durumunu true yapmanız gerekmektedir.");
                return View(model);
            }
        }

        [KullaniciLoginFilter]
        public ActionResult Teslim(int? id)
        {
            if (id != null)
            {
                if (ihtiyacSahibiBAL.KullaniciIslemYapabilirMi(KullaniciBilgileriDondur.KullaniciId(),id))
                {
                    if (ihtiyacSahibiBAL.IhtiyacSahibiMuhtacMi(id))
                    {
                        var teslimModel = ihtiyacSahibiBAL.teslimEdilecekBilgileriGetir(id);
                        return View(teslimModel);
                    }
                    else
                    {
                        TempData["hata"] = "İhtiyaç sahibinin durumu muhtaç olmadığından işlem yapılamamaktadır.";
                        return RedirectToAction("IhtiyacSahibiKontrolListesi");
                    }
                }
                else
                {
                    TempData["hata"] = "Sadece kendi bölgenizdeki ihtiyaç sahipleri ile ilgili işlem yapabilirsiniz";
                    return RedirectToAction("IhtiyacSahibiKontrolListesi");
                }
            }
            else
            {
                TempData["hata"] = "Lütfen işlem yapmak istediğiniz ihtiyaç sahibini seçiniz";
                return RedirectToAction("IhtiyacSahibiKontrolListesi");
            }
        }
        [KullaniciLoginFilter]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Teslim(IhtiyacSahibiTeslimModel model)
        {
            if (ModelState.IsValid)
            {
                if (ihtiyacSahibiBAL.IhtiyacSahibiKontrolVarMi(model.IhtiyacSahibiKontrolId))
                {
                    if (ihtiyacSahibiBAL.KullaniciIslemYapabilirMi(KullaniciBilgileriDondur.KullaniciId(),
                        model.IhtiyacSahibiKontrolId))
                    {
                        if (ihtiyacSahibiBAL.IhtiyacSahibiMuhtacMi(model.IhtiyacSahibiKontrolId))
                        {
                            if (ihtiyacSahibiBAL.ihtiyacSahibiTeslimKaydet(model))
                            {
                                TempData["uyari"] = "Teslim işlemi başarı ile tamamlandı.";
                                return RedirectToAction("IhtiyacSahibiKontrolListesi");
                            }
                            else
                            {
                                TempData["hata"] = "Teslim edilen eşyaları kaydetme işleminde hata oluştu.";
                                return View(model);
                            }
                        }
                        else
                        {
                            TempData["hata"] = "İhtiyaç sahibinin durumu muhtaç olarak ayarlanmadığından işlem yapılamamaktadır.";
                            return View(model);
                        }
                    }
                    else
                    {
                        TempData["hata"] = "Sadece kendi bölgenizdeki ihtiyaç sahipleri için işlem yapabilirsiniz.";
                        return RedirectToAction("IhtiyacSahibiKontrolListesi");
                    }
                }
                else
                {
                    TempData["hata"] = "İşlem yapmak istediğiniz ihtiyac sahibi bulunamadı.";
                    return RedirectToAction("IhtiyacSahibiKontrolListesi");
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
    }
}