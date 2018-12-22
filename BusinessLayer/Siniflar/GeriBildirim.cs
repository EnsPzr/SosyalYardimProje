using BusinessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer.Models.OrtakModeller;

namespace BusinessLayer.Siniflar
{
    public class GeriBildirim
    {
        DataLayer.Siniflar.GeriBildirim geriBildirimDAL = new DataLayer.Siniflar.GeriBildirim();

        public List<GeriBildirimModel> TumGeriBildirimleriGetir(int? kullaniciId)
        {
            var geriBildirimler = geriBildirimDAL.TumGeriBildirimleriGetir(kullaniciId);
            var donGeriBildirimler = geriBildirimler.Select(p => new GeriBildirimModel()
            {
                DurumInt = p.GeriBildirimDurumu,
                DurumStr = p.GeriBildirimDurumu == 0 ? "Okunmadı" : p.GeriBildirimDurumu == 1 ? "Okundu" : p.GeriBildirimDurumu == 2 ? "Geri Dönüş Yapıldı" : "Geri Dönüşe Gerek Görülmedi",
                GeriBildirimId = p.GeriBildirimId,
                Konu = p.GeriBildirimKonu,
                KullaniciAdiSoyadi = p.KullaniciBilgileriTablo.KullaniciAdi + " " + p.KullaniciBilgileriTablo.KullaniciSoyadi,
                KullaniciTel = p.KullaniciBilgileriTablo.KullaniciTelefonNumarasi,
                Mesaj = p.GeriBildirimMesaj,
                Tarih = p.Tarih,
                TarihStr = p.Tarih != null ? p.Tarih.Value.ToShortDateString() : ""
            }).ToList();
            return donGeriBildirimler;
        }

        public List<GeriBildirimModel> FiltreliGeriBildirimleriGetir(int? kullaniciId, string aranan, string tarih, int? sehirId)
        {
            var geriBildirimler = geriBildirimDAL.FiltreliGeriBildirimleriGetir(kullaniciId, aranan, tarih, sehirId);
            var donGeriBildirimler = geriBildirimler.Select(p => new GeriBildirimModel()
            {
                DurumInt = p.GeriBildirimDurumu,
                DurumStr = p.GeriBildirimDurumu == 0 ? "Okunmadı" : p.GeriBildirimDurumu == 1 ? "Okundu" : p.GeriBildirimDurumu == 2 ? "Geri Dönüş Yapıldı" : "Geri Dönüşe Gerek Görülmedi",
                GeriBildirimId = p.GeriBildirimId,
                Konu = p.GeriBildirimKonu,
                KullaniciAdiSoyadi = p.KullaniciBilgileriTablo.KullaniciAdi + " " + p.KullaniciBilgileriTablo.KullaniciSoyadi,
                KullaniciTel = p.KullaniciBilgileriTablo.KullaniciTelefonNumarasi,
                Mesaj = p.GeriBildirimMesaj,
                Tarih = p.Tarih,
                TarihStr = p.Tarih != null ? p.Tarih.Value.ToShortDateString() : ""
            }).ToList();
            return donGeriBildirimler;
        }

        public bool KullaniciIslemYapabilirMi(int? kullaniciId, int? geriBildirimId)
        {
            return geriBildirimDAL.KullaniciIslemYapabilirMi(kullaniciId, geriBildirimId);
        }

        public GeriBildirimModel GeriBildirimGetir(int? geriBildirimId)
        {
            var geriBildirim = geriBildirimDAL.GeriBildirimGetir(geriBildirimId);
            if (geriBildirim != null)
            {
                GeriBildirimModel model = new GeriBildirimModel();
                model.GeriBildirimId = geriBildirimId;
                model.KullaniciAdiSoyadi = geriBildirim.KullaniciBilgileriTablo.KullaniciAdi + " " +
                                           geriBildirim.KullaniciBilgileriTablo.KullaniciSoyadi;
                model.KullaniciTel = geriBildirim.KullaniciBilgileriTablo.KullaniciTelefonNumarasi;
                model.Konu = geriBildirim.GeriBildirimKonu;
                model.Mesaj = geriBildirim.GeriBildirimMesaj;
                model.DurumStr = geriBildirim.GeriBildirimDurumu == 0 ? "Okunmadı" :
                    geriBildirim.GeriBildirimDurumu == 1 ? "Okundu" :
                    geriBildirim.GeriBildirimDurumu == 2 ? "Geri Dönüş Yapıldı" : "Geri Dönüşe Gerek Görülmedi";
                model.DurumInt = geriBildirim.GeriBildirimDurumu;
                model.Tarih = geriBildirim.Tarih;
                model.TarihStr = geriBildirim.Tarih != null ? geriBildirim.Tarih.Value.ToShortDateString() : "";
                return model;
            }
            else
            {
                return null;
            }
        }

        public IslemOnayModel GeriBildirimKaydet(int? kullaniciId, int? geriBildirimId, int? durumId)
        {
            IslemOnayModel onay = new IslemOnayModel();
            if (KullaniciIslemYapabilirMi(kullaniciId, geriBildirimId))
            {
                if (geriBildirimDAL.GeriBildirimKaydet(geriBildirimId, durumId))
                {
                    onay.TamamlandiMi = true;
                }
                else
                {
                    onay.TamamlandiMi = false;
                    onay.HataMesajlari.Add("Düzenlenecek geri bildirim bulunamadı.");
                }
            }
            else
            {
                onay.TamamlandiMi = false;
                onay.HataMesajlari.Add("Sadece kendi bölgenize yapılan geri bildirimler ile ilgili işlem yapabilirsiniz.");
            }

            return onay;
        }
    }
}
