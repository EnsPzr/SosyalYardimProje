using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using BusinessLayer;
using BusinessLayer.Models.TeslimAlinacakBagis;
using BusinessLayer.Siniflar;
using SosyalYardimProje.Filters;

namespace SosyalYardimProje.Controllers
{
    [HataFiltresi]
    public class TeslimAlinacakBagisController : Controller
    {
        private Kullanici kullaniciBAL = new Kullanici();
        private TeslimAlinacakBagis bagisBAL = new TeslimAlinacakBagis();
        [KullaniciLoginFilter]
        public ActionResult Liste()
        {
            Tanimla();
            return View();
        }

        [SadeceLoginFilter]
        [HttpGet]
        public JsonResult TumBagiscilariGetir()
        {
            TeslimAlinacakBagisJsModel model = new TeslimAlinacakBagisJsModel()
            {
                BagisList = bagisBAL.TumBagislariGetir(KullaniciBilgileriDondur.KullaniciId()),
                BasariliMi = true,
                BagisSayisi = bagisBAL.TumBagislariGetir(KullaniciBilgileriDondur.KullaniciId()).Count
            };
            Thread.Sleep(2000);
            return Json(model, JsonRequestBehavior.AllowGet);
        }
        [SadeceLoginFilter]
        [HttpGet]
        public JsonResult FiltreliBagiscilariGetir(int? sehirId, string aranan, string tarih)
        {
            if (!(tarih.Equals("")))
            {
                try
                {
                    Convert.ToDateTime(tarih);
                }
                catch (Exception e)
                {
                    tarih = null;
                }
            }
            else
            {
                tarih = null;
            }

            if ((aranan.Equals("")))
            {
                aranan = null;
            }
            TeslimAlinacakBagisJsModel model = new TeslimAlinacakBagisJsModel()
            {
                BagisList = bagisBAL.FiltreliBagislariGetir(KullaniciBilgileriDondur.KullaniciId(),sehirId,aranan,tarih),
                BasariliMi = true,
                BagisSayisi = bagisBAL.FiltreliBagislariGetir(KullaniciBilgileriDondur.KullaniciId(), sehirId, aranan, tarih).Count
            };
            Thread.Sleep(2000);
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        [KullaniciLoginFilter]
        public ActionResult Duzenle(int? id)
        {
            if (id != null)
            {
                if (bagisBAL.KullaniciBagisDetayiGorebilirMi(KullaniciBilgileriDondur.KullaniciId(), id))
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
                    TempData["hata"] = "Bu bağışı düzenlemek için yetjniz bulunmamaktadır.";
                    return RedirectToAction("Liste");
                }
            }
            else
            {
                TempData["hata"] = "Düzenlemek için bağış seçiniz";
                return RedirectToAction("Liste");
            }
        }

        [KullaniciLoginFilter]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Duzenle(TeslimAlinacakBagisModel model)
        {
            if (ModelState.IsValid)
            {
                int sayac = 0;
                for (int i = 0; i < model.esyaModel.Count; i++)
                {
                    if (model.esyaModel[i].AlinacakMi == true)
                    {
                        sayac++;
                    }
                }

                if (!(sayac > 0 && model.TahminiTeslimAlma.HasValue))
                {
                    ModelState.AddModelError("TahminiTeslimAlma", "Tahmini Teslim Alma Tarihi Seçilmelidir.");
                    return View(model);
                }

                if (model.TahminiTeslimAlma.HasValue)
                {
                    try
                    {
                        Convert.ToDateTime(model.TahminiTeslimAlma);
                    }
                    catch (Exception)
                    {
                        ModelState.AddModelError("TahminiTeslimAlma","Tahmini teslim alma formatı uygun değil");
                        return View(model);
                    }
                }

                int sayac2 = 0;
                for (int i = 0; i < model.esyaModel.Count; i++)
                {
                    if (model.esyaModel[i].AlinacakMi == false && model.esyaModel[i].AlindiMi == true)
                    {
                        sayac2++;
                    }
                }

                if (sayac2 > 0)
                {
                    TempData["hata"] = "Teslim alınmayacak olarak işaretlenen eşya için teslim alındı işaretlenmiş.";
                    return View(model);
                }

                if (bagisBAL.TeslimBagisKaydet(model))
                {
                    TempData["uyari"] = "Kayıt başarı ile tamamlandı.";
                    return RedirectToAction("Liste");
                }
                else
                {
                    TempData["hata"] = "Kayıt sırasında hata oluştu.";
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
            var sehirler = kullaniciBAL.SehirleriGetir(KullaniciBilgileriDondur.KullaniciId()).Select(p =>
                new SelectListItem()
                {
                    Text = p.SehirAdi,
                    Value = p.SehirId.ToString()
                }).ToList();
            ViewBag.sehirler = sehirler;
        }
    }
}