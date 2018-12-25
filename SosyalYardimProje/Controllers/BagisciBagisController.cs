using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Configuration;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using BusinessLayer.BagisciSiniflar;
using BusinessLayer.Models.BagisciBagisModelleri;
using BusinessLayer.Models.TeslimAlinacakBagis;
using BusinessLayer.Siniflar;

namespace SosyalYardimProje.Controllers
{
    //full bağışçı
    public class BagisciBagisController : Controller
    {
        private BagisciBagis bagisBAL = new BagisciBagis();
        private Esya esyaBAL = new Esya();
        private Kullanici kullaniciBAL = new Kullanici();
        private Bagisci bagisciBAL = new Bagisci();
        public ActionResult Liste()
        {
            return View();
        }

        [HttpGet]
        public JsonResult TumBagislariGetir()
        {
            TeslimAlinacakBagisJsModel model = new TeslimAlinacakBagisJsModel()
            {
                BasariliMi = true,
                BagisList = bagisBAL.TumBagislariGetir(BagisciBilgileriDondur.KullaniciId())
            };
            model.BagisSayisi = model.BagisList.Count;
            Thread.Sleep(2000);
            return Json(model, JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
        public JsonResult FiltreliBagislariGetir(string tarih)
        {
            if (tarih != null)
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
            TeslimAlinacakBagisJsModel model = new TeslimAlinacakBagisJsModel()
            {
                BasariliMi = true,
                BagisList = bagisBAL.FiltreliBagislariGetir(BagisciBilgileriDondur.KullaniciId(), tarih)
            };
            model.BagisSayisi = model.BagisList.Count;
            Thread.Sleep(2000);
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        public ActionResult YeniBagis()
        {
            Tanimla();
            List<BagisciBagisModel> bagislar = new List<BagisciBagisModel>();
            for (int i = 0; i < 5; i++)
            {
                bagislar.Add(new BagisciBagisModel());
            }
            return View(bagislar);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult YeniBagis(List<BagisciBagisModel> model)
        {
            TeslimAlinacakBagisModel bagisTablo = new TeslimAlinacakBagisModel();
            var kullanici = kullaniciBAL.KullaniciGetir(BagisciBilgileriDondur.KullaniciId());
            var bagisci = bagisciBAL.BagisciBul(BagisciBilgileriDondur.KullaniciId());
            bagisTablo.BagisciAdiSoyadi = bagisci.BagisciAdi + " " + bagisci.BagisciSoyadi;
            bagisTablo.BagisciAdres = bagisci.Adres;
            bagisTablo.BagisciTelNo = bagisci.TelNo;
            bagisTablo.esyaModel = new List<TeslimAlinacakBagisEsyaModel>();
            for (int i = 0; i < model.Count; i++)
            {
                if (model[i].Adet != null)
                {
                    bagisTablo.esyaModel.Add(new TeslimAlinacakBagisEsyaModel()
                    {
                        Adet = model[i].Adet,
                        EsyaId = model[i].EsyaId
                    });
                    bagisTablo.esyaModel[i].resimModel = new List<TeslimAlinacakBagisResimModel>();
                    var eklenecekResim = new TeslimAlinacakBagisResimModel();
                    if (model[i].Resim1_data != null)
                    {
                        int a = model[i].Resim1_data.FileName.LastIndexOf(".");
                        int b = model[i].Resim1_data.FileName.Length - a;
                        Guid gu = Guid.NewGuid();
                        String guId = Guid.NewGuid().ToString("N") + model[i].Resim1_data.FileName
                                          .Substring(a,
                                              (b));
                        model[i].Resim1_data.SaveAs(Server.MapPath("~/Picture") + "/" + guId);
                        eklenecekResim.ResimYol = "/Picture/" + guId;
                    }
                    if (model[i].Resim2_data != null)
                    {
                        int a = model[i].Resim2_data.FileName.LastIndexOf(".");
                        int b = model[i].Resim2_data.FileName.Length - a;
                        String guId = Guid.NewGuid().ToString("N") + model[i].Resim2_data.FileName
                                          .Substring(a,
                                              (b));
                        model[i].Resim2_data.SaveAs(Server.MapPath("~/Picture") + "/" + guId);
                        eklenecekResim.ResimYol2 = "/Picture/" + guId;
                    }
                    if (model[i].Resim3_data != null)
                    {
                        int a = model[i].Resim2_data.FileName.LastIndexOf(".");
                        int b = model[i].Resim2_data.FileName.Length - a;
                        String guId = Guid.NewGuid().ToString("N") + model[i].Resim3_data.FileName
                                          .Substring(a,
                                              (b));
                        model[i].Resim3_data.SaveAs(Server.MapPath("~/Picture") + "/" + guId);
                        eklenecekResim.ResimYol3 = "/Picture/" + guId;
                    }

                    if (eklenecekResim.ResimYol != null)
                    {
                        bagisTablo.esyaModel[i].resimModel.Add(eklenecekResim);
                    }
                }
            }

            var sonuc = bagisBAL.BagisKaydet(bagisTablo, Convert.ToInt32(BagisciBilgileriDondur.KullaniciId()));
            if (sonuc == true)
            {
                TempData["uyari"] = "Teşekkür ederiz. En yakın zamanda birimlerimiz sizinle irtibata geçecekler.";
            }
            else
            {
                TempData["hata"] = "Bilinmeyen bir hata oluştu.";
            }
            return RedirectToAction("Liste");
        }


        public ActionResult Sil(int? id)
        {
            if (id != null)
            {
                if (bagisBAL.KullaniciIslemYapabilirMi(BagisciBilgileriDondur.KullaniciId(), id))
                {
                    var bagis = bagisBAL.Detay(id);
                    if (bagis != null)
                    {
                        return View(bagis);
                    }
                    else
                    {
                        TempData["hata"] = "Bağış bulunamadı.";
                        return RedirectToAction("Liste");
                    }
                }
                else
                {
                    TempData["hata"] = "Sadece kendi bağışınız için işlem yapabilirsiniz.";
                    return RedirectToAction("Liste");
                }
            }
            else
            {
                TempData["hata"] = "Lütfen silmek istediğiniz bağışı seçiniz.";
                return RedirectToAction("Liste");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult BagisSil(int? id)
        {
            if (id != null)
            {
                if (bagisBAL.KullaniciIslemYapabilirMi(BagisciBilgileriDondur.KullaniciId(), id))
                {
                    var sonuc = bagisBAL.BagisSil(BagisciBilgileriDondur.KullaniciId(),id);
                    if (sonuc.TamamlandiMi == true)
                    {
                        TempData["uyari"] = "İşlem başarı ile gerçekleşti.";
                        return RedirectToAction("Liste");
                    }
                    else
                    {
                        String hatalar = KullaniciBilgileriDondur.HataMesajlariniOku(sonuc.HataMesajlari);
                        TempData["hata"] = hatalar;
                        return RedirectToAction("Liste");
                    }
                }
                else
                {
                    TempData["hata"] = "Sadece kendi bağışınız için işlem yapabilirsiniz.";
                    return RedirectToAction("Liste");
                }
            }
            else
            {
                TempData["hata"] = "Lütfen silmek istediğiniz bağışı seçiniz.";
                return RedirectToAction("Liste");
            }
        }
        public void Tanimla()
        {
            var esyalar = esyaBAL.TumEsyalariGetir().Select(p => new SelectListItem()
            {
                Value = p.EsyaId.ToString(),
                Text = p.EsyaAdi
            }).ToList();
            ViewBag.esyalarSelectList = esyalar;
        }
    }
}