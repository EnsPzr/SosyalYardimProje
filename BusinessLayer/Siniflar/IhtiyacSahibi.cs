using System;
using System.Collections.Generic;
using BusinessLayer.Models.IhtiyacSahibiModelleri;
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
    }
}
