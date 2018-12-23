using BusinessLayer.Siniflar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BusinessLayer.Models.MesajModelleri;
using System.Threading;

namespace SosyalYardimProje.Controllers
{
    public class MesajController : Controller
    {
        private Mesaj mesajBAL = new Mesaj();
        private Kullanici kullaniciBAL = new Kullanici();
        public ActionResult Liste()
        {
            Tanimla();
            return View();
        }

        [HttpGet]
        public JsonResult TumMesajlariGetir()
        {
            MesajJsModel model = new MesajJsModel()
            {
                BasariliMi = true,
                MesajList = mesajBAL.TumMesajlariGetir(KullaniciBilgileriDondur.KullaniciId())
            };
            model.MesajSayisi = model.MesajList.Count;
            Thread.Sleep(2000);
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult FiltreliMesajlariGetir(int? arananKullaniciId, string aranan, string tarih,int? kimeGonderildi)
        {
            aranan = "";
            if (aranan.Equals(""))
            {
                aranan = null;
            }

            if (tarih.Equals(""))
            {
                tarih = null;
            }
            MesajJsModel model = new MesajJsModel()
            {
                BasariliMi = true,
                MesajList = mesajBAL.FiltreliMesajlariGetir(KullaniciBilgileriDondur.KullaniciId(), arananKullaniciId, aranan, tarih, kimeGonderildi)
            };
            model.MesajSayisi = model.MesajList.Count;
            Thread.Sleep(2000);
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        public ActionResult DetayListe(int? id)
        {
            ViewBag.MesajId = id;
            Tanimla();
            return View();
        }

        [HttpGet]
        public JsonResult TumDetaylariGetir(int? detayId)
        {
            if (mesajBAL.KullaniciIslemYapabilirMi(KullaniciBilgileriDondur.KullaniciId(), detayId))
            {
                MesajDetayJsModel model = new MesajDetayJsModel()
                {
                    BasariliMi = true,
                    MesajDetayList=mesajBAL.TumMesajlarDetaylariGetir(detayId)
                };
                model.MesajDetaySayisi = model.MesajDetayList.Count;
                return Json(model, JsonRequestBehavior.AllowGet);
            }
            else
            {
                MesajDetayJsModel model = new MesajDetayJsModel()
                {
                    BasariliMi = false
                };
                return Json(model, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public JsonResult FiltreliDetaylariGetir(int? detayId, string aranan)
        {
            if (mesajBAL.KullaniciIslemYapabilirMi(KullaniciBilgileriDondur.KullaniciId(), detayId))
            {
                if (aranan.Equals(""))
                {
                    aranan = null;
                }
                MesajDetayJsModel model = new MesajDetayJsModel()
                {
                    BasariliMi = true,
                    MesajDetayList = mesajBAL.FiltreliMesajlarDetaylariGetir(detayId,aranan)
                };
                model.MesajDetaySayisi = model.MesajDetayList.Count;
                return Json(model, JsonRequestBehavior.AllowGet);
            }
            else
            {
                MesajDetayJsModel model = new MesajDetayJsModel()
                {
                    BasariliMi = false
                };
                return Json(model, JsonRequestBehavior.AllowGet);
            }
        }

        public void Tanimla()
        {
            var kullanicilar = kullaniciBAL.TumKullanicilariGetir(KullaniciBilgileriDondur.KullaniciId());
            var kullanicilarSelect = kullanicilar.Select(p => new SelectListItem()
            {
                Text = p.KullaniciAdi + " " + p.KullaniciSoyadi,
                Value = p.KullaniciId.ToString()
            }).ToList();
            ViewBag.kullanicilarSelect = kullanicilarSelect;

            var kimeGonderildi = new List<SelectListItem>();
            kimeGonderildi.Add(new SelectListItem() { Text = "Herkes", Value = "0" });
            kimeGonderildi.Add(new SelectListItem() { Text = "Koordinatörler", Value = "1" });
            kimeGonderildi.Add(new SelectListItem() { Text = "Hiç Kimse", Value = "3" });
            ViewBag.kimeGonderildi = kimeGonderildi;
        }
    }
}