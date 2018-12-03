using System;
using System.Collections.Generic;
using BusinessLayer.Models.IhtiyacSahibiModelleri;
using BusinessLayer.Models.OrtakModeller;
using DataLayer;

namespace BusinessLayer.Siniflar
{
    public class IhtiyacSahibi
    {
        private DataLayer.Siniflar.IhtiyacSahibi ihtiyacSahibiDAL = new DataLayer.Siniflar.IhtiyacSahibi();

        public List<IhtiyacSahibiModel> TumIhtiyacSahipleriniGetir(int? KullaniciId)
        {
            var ihtiyacSahipleri = ihtiyacSahibiDAL.TumIhtiyacSahipleriniGetir(KullaniciId);
            var dondurulecekIhtiyacSahipleri = new List<IhtiyacSahibiModel>();
            for (int i = 0; i < ihtiyacSahipleri.Count; i++)
            {
                IhtiyacSahibiModel eklenecekIhtiyacSahibi = new IhtiyacSahibiModel()
                {
                    IhtiyacSahibiAciklama = ihtiyacSahipleri[i].IhtiyacSahibiAciklama,
                    IhtiyacSahibiAdi = ihtiyacSahipleri[i].IhtiyacSahibiAdi,
                    IhtiyacSahibiAdres = ihtiyacSahipleri[i].IhtiyacSahibiAdres,
                    IhtiyacSahibiId = ihtiyacSahipleri[i].IhtiyacSahibiId,
                    IhtiyacSahibiSoyadi = ihtiyacSahipleri[i].IhtiyacSahibiSoyadi,
                    IhtiyacSahibiTelNo = ihtiyacSahipleri[i].IhtiyacSahibiTelNo,
                    Sehir = new Models.OrtakModeller.SehirModel()
                    {
                        SehirAdi = ihtiyacSahipleri[i].SehirTablo.SehirAdi,
                        SehirId = ihtiyacSahipleri[i].SehirTablo.SehirId
                    }
                };
                dondurulecekIhtiyacSahipleri.Add(eklenecekIhtiyacSahibi);
            }

            return dondurulecekIhtiyacSahipleri;
        }

        public List<IhtiyacSahibiModel> FiltreliIhtiyacSahibiListesiniGetir(String aranan, int? sehirId, int? kullaniciId)
        {
            var ihtiyacSahipleri = ihtiyacSahibiDAL.FiltreliIhtiyacSahipleriListesiniGetir(aranan, sehirId, kullaniciId);
            var dondurulecekIhtiyacSahipleri = new List<IhtiyacSahibiModel>();
            for (int i = 0; i < ihtiyacSahipleri.Count; i++)
            {
                IhtiyacSahibiModel eklenecekIhtiyacSahibi = new IhtiyacSahibiModel()
                {
                    IhtiyacSahibiAciklama = ihtiyacSahipleri[i].IhtiyacSahibiAciklama,
                    IhtiyacSahibiAdi = ihtiyacSahipleri[i].IhtiyacSahibiAdi,
                    IhtiyacSahibiAdres = ihtiyacSahipleri[i].IhtiyacSahibiAdres,
                    IhtiyacSahibiId = ihtiyacSahipleri[i].IhtiyacSahibiId,
                    IhtiyacSahibiSoyadi = ihtiyacSahipleri[i].IhtiyacSahibiSoyadi,
                    IhtiyacSahibiTelNo = ihtiyacSahipleri[i].IhtiyacSahibiTelNo,
                    Sehir = new Models.OrtakModeller.SehirModel()
                    {
                        SehirAdi = ihtiyacSahipleri[i].SehirTablo.SehirAdi,
                        SehirId = ihtiyacSahipleri[i].SehirTablo.SehirId
                    }
                };
                dondurulecekIhtiyacSahipleri.Add(eklenecekIhtiyacSahibi);
            }

            return dondurulecekIhtiyacSahipleri;
        }

        public IslemOnayModel IhtiyacSahibiKaydet(IhtiyacSahibiModel yeniIhtiyacSahibi)
        {
            IslemOnayModel onay = new IslemOnayModel();
            if (ihtiyacSahibiDAL.IhtiyacSahibiVarMi(yeniIhtiyacSahibi.IhtiyacSahibiAdi,
                    yeniIhtiyacSahibi.IhtiyacSahibiSoyadi, yeniIhtiyacSahibi.IhtiyacSahibiTelNo) != null)
            {
                IhtiyacSahibiTablo eklenecekIhtiyacSahibi = new IhtiyacSahibiTablo();
                eklenecekIhtiyacSahibi.IhtiyacSahibiAdi = yeniIhtiyacSahibi.IhtiyacSahibiAdi;
                eklenecekIhtiyacSahibi.IhtiyacSahibiSoyadi = yeniIhtiyacSahibi.IhtiyacSahibiSoyadi;
                eklenecekIhtiyacSahibi.IhtiyacSahibiTelNo = yeniIhtiyacSahibi.IhtiyacSahibiTelNo;
                eklenecekIhtiyacSahibi.IhtiyacSahibiAdres = yeniIhtiyacSahibi.IhtiyacSahibiAdres;
                eklenecekIhtiyacSahibi.IhtiyacSahibiAciklama = yeniIhtiyacSahibi.IhtiyacSahibiAciklama;
                eklenecekIhtiyacSahibi.SehirTablo_SehirId = yeniIhtiyacSahibi.Sehir.SehirId;
                if (ihtiyacSahibiDAL.IhtiyacSahibiKaydet(eklenecekIhtiyacSahibi))
                {
                    onay.TamamlandiMi = true;
                }
                else
                {
                    onay.TamamlandiMi = false;
                    onay.HataMesajlari.Add("Bilinmeyen bir hata oluştu.");
                }
            }
            else
            {
                onay.TamamlandiMi = false;
                var sehir = ihtiyacSahibiDAL.IhtiyacSahibiVarMi(yeniIhtiyacSahibi.IhtiyacSahibiAdi,
                    yeniIhtiyacSahibi.IhtiyacSahibiSoyadi, yeniIhtiyacSahibi.IhtiyacSahibiTelNo).SehirTablo.SehirAdi;
                onay.HataMesajlari.Add($"Bu bilgilerde {sehir} için zaten bir ihtiyaç sahibi kayıt edilmiş");
            }

            return onay;
        }
    }
}
