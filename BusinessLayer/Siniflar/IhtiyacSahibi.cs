using System;
using System.Collections.Generic;
using BusinessLayer.Models.IhtiyacSahibiModelleri;
using BusinessLayer.Models.OrtakModeller;
using DataLayer;

namespace BusinessLayer.Siniflar
{
    public class IhtiyacSahibi
    {
        private KullaniciYonetimi kullaniciBAL = new KullaniciYonetimi();
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
                    yeniIhtiyacSahibi.IhtiyacSahibiSoyadi, yeniIhtiyacSahibi.IhtiyacSahibiTelNo) == null)
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

        public IhtiyacSahibiModel IhtiyacSahibiGetir(int? ihtiyacSahibiId)
        {
            var ihtiyacSahibi = ihtiyacSahibiDAL.IhtiyacSahibiGetir(ihtiyacSahibiId);
            IhtiyacSahibiModel goruntulenecekIhtiyacSahibi = new IhtiyacSahibiModel();
            goruntulenecekIhtiyacSahibi.IhtiyacSahibiId = ihtiyacSahibi.IhtiyacSahibiId;
            goruntulenecekIhtiyacSahibi.IhtiyacSahibiAdi = ihtiyacSahibi.IhtiyacSahibiAdi;
            goruntulenecekIhtiyacSahibi.IhtiyacSahibiSoyadi = ihtiyacSahibi.IhtiyacSahibiSoyadi;
            goruntulenecekIhtiyacSahibi.IhtiyacSahibiAdres = ihtiyacSahibi.IhtiyacSahibiAdres;
            goruntulenecekIhtiyacSahibi.IhtiyacSahibiTelNo = ihtiyacSahibi.IhtiyacSahibiTelNo;
            goruntulenecekIhtiyacSahibi.IhtiyacSahibiAciklama = ihtiyacSahibi.IhtiyacSahibiAciklama;
            goruntulenecekIhtiyacSahibi.Sehir = new SehirModel
            {
                SehirAdi = ihtiyacSahibi.SehirTablo.SehirAdi,
                SehirId = ihtiyacSahibi.SehirTablo_SehirId
            };
            return goruntulenecekIhtiyacSahibi;
        }

        public bool IhtiyacSahibiGoruntulenebilirMi(int? ihtiyacSahibiId, int? kullaniciId)
        {
            var kullanici = kullaniciBAL.LoginKullaniciBul(kullaniciId);
            if (kullanici.KullaniciMerkezdeMi == true)
            {
                return true;
            }
            else
            {
                var ihtiyacSahibi = ihtiyacSahibiDAL.IhtiyacSahibiGetir(ihtiyacSahibiId);
                if (ihtiyacSahibi.SehirTablo_SehirId == kullanici.SehirTablo_SehirId)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public IslemOnayModel IhtiyacSahibiSil(int? ihtiyacSahibiId)
        {
            IslemOnayModel onay = new IslemOnayModel();
            var sonuc = ihtiyacSahibiDAL.IhtiyacSahibiSil(ihtiyacSahibiId);
            if (sonuc == true)
            {
                onay.TamamlandiMi = true;
            }
            else
            {
                onay.TamamlandiMi = false;
                onay.HataMesajlari.Add("İhtiyaç sahibi silinirken hata oluştu");
            }

            return onay;
        }

        public IslemOnayModel IhtiyacSahibiGuncelle(IhtiyacSahibiModel duzenlenmisIhtiyacSahibi)
        {
            IslemOnayModel onay = new IslemOnayModel();
            if (ihtiyacSahibiDAL.IhtiyacSahibiVarMi(duzenlenmisIhtiyacSahibi.IhtiyacSahibiId,
                duzenlenmisIhtiyacSahibi.IhtiyacSahibiAdi, duzenlenmisIhtiyacSahibi.IhtiyacSahibiSoyadi,
                duzenlenmisIhtiyacSahibi.IhtiyacSahibiTelNo) == null)
            {
                IhtiyacSahibiTablo eklenecekIhtiyacSahibi = new IhtiyacSahibiTablo();
                eklenecekIhtiyacSahibi.IhtiyacSahibiId = Convert.ToInt32(duzenlenmisIhtiyacSahibi.IhtiyacSahibiId);
                eklenecekIhtiyacSahibi.IhtiyacSahibiAdi = duzenlenmisIhtiyacSahibi.IhtiyacSahibiAdi;
                eklenecekIhtiyacSahibi.IhtiyacSahibiSoyadi = duzenlenmisIhtiyacSahibi.IhtiyacSahibiSoyadi;
                eklenecekIhtiyacSahibi.IhtiyacSahibiTelNo = duzenlenmisIhtiyacSahibi.IhtiyacSahibiTelNo;
                eklenecekIhtiyacSahibi.IhtiyacSahibiAdres = duzenlenmisIhtiyacSahibi.IhtiyacSahibiAdres;
                eklenecekIhtiyacSahibi.IhtiyacSahibiAciklama = duzenlenmisIhtiyacSahibi.IhtiyacSahibiAciklama;
                eklenecekIhtiyacSahibi.SehirTablo_SehirId = duzenlenmisIhtiyacSahibi.Sehir.SehirId;
                if (ihtiyacSahibiDAL.IhtiyacSahibiGuncelle(eklenecekIhtiyacSahibi))
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
                var sehir = ihtiyacSahibiDAL.IhtiyacSahibiVarMi(duzenlenmisIhtiyacSahibi.IhtiyacSahibiAdi,
                    duzenlenmisIhtiyacSahibi.IhtiyacSahibiSoyadi, duzenlenmisIhtiyacSahibi.IhtiyacSahibiTelNo).SehirTablo.SehirAdi;
                onay.HataMesajlari.Add($"Bu bilgilerde {sehir} için zaten bir ihtiyaç sahibi kayıt edilmiş");
            }
            return onay;
        }

        public List<IhtiyacSahibiKontrolModel> KontrolEdilecekIhtiyacSahipleriniGetir(int? kullaniciId)
        {
            var kontrolEdilecekIhtiyacSahipleri = ihtiyacSahibiDAL.KontrolEdilecekIhtiyacSahipleriniGetir(kullaniciId);
            List<IhtiyacSahibiKontrolModel> kontrolListeModel = new List<IhtiyacSahibiKontrolModel>();
            for (int i = 0; i < kontrolEdilecekIhtiyacSahipleri.Count; i++)
            {
                IhtiyacSahibiKontrolModel eklenecekModel = new IhtiyacSahibiKontrolModel();
                eklenecekModel.IhtiyacSahibiAdiSoyadi =
                    kontrolEdilecekIhtiyacSahipleri[i].IhtiyacSahibiTablo.IhtiyacSahibiAdi + " " +
                    kontrolEdilecekIhtiyacSahipleri[i].IhtiyacSahibiTablo.IhtiyacSahibiSoyadi;
                eklenecekModel.IhtiyacSahibiTelNo =
                    kontrolEdilecekIhtiyacSahipleri[i].IhtiyacSahibiTablo.IhtiyacSahibiTelNo;
                eklenecekModel.IhtiyacSahibiAdres =
                    kontrolEdilecekIhtiyacSahipleri[i].IhtiyacSahibiTablo.IhtiyacSahibiAdres;
                eklenecekModel.IhtiyacSahibiKontrolId = kontrolEdilecekIhtiyacSahipleri[i].IhtiyacSahibiKontrolId;
                if (kontrolEdilecekIhtiyacSahipleri[i].MuhtacMi != null)
                {
                    if (kontrolEdilecekIhtiyacSahipleri[i].MuhtacMi == true)
                    {
                        eklenecekModel.MuhtacMi = "Evet";
                    }
                    else
                    {
                        eklenecekModel.MuhtacMi = "Hayır";
                    }
                }
                else
                {
                    eklenecekModel.MuhtacMi = "Hayır";
                }

                eklenecekModel.EklenmeTarih = kontrolEdilecekIhtiyacSahipleri[i].Tarih;
                if (kontrolEdilecekIhtiyacSahipleri[i].KontrolYapilmaTarihi != null)
                {
                    eklenecekModel.KontrolTarih = kontrolEdilecekIhtiyacSahipleri[i].KontrolYapilmaTarihi;
                }

                if (kontrolEdilecekIhtiyacSahipleri[i].TahminiTeslimTarihi != null)
                {
                    eklenecekModel.TahminiTeslimTarihi = kontrolEdilecekIhtiyacSahipleri[i].TahminiTeslimTarihi;
                }

                if (kontrolEdilecekIhtiyacSahipleri[i].TeslimTamamlandiMi != null)
                {
                    if (kontrolEdilecekIhtiyacSahipleri[i].TeslimTamamlandiMi == true)
                    {
                        eklenecekModel.TeslimTamamlandiMi = "Evet";
                    }
                    else
                    {
                        eklenecekModel.TeslimTamamlandiMi = "Hayır";
                    }
                }
                else
                {
                    eklenecekModel.TeslimTamamlandiMi = "Hayır";
                }
            }

            return kontrolListeModel;
        }

        public List<IhtiyacSahibiKontrolModel> KontrolEdilecekFiltreliIhtiyacSahipleriniGetir(int? kullaniciId,
            string aranan, int? sehirId, DateTime? tarih)
        {
            var kontrolEdilecekIhtiyacSahipleri = ihtiyacSahibiDAL.KontrolEdilecekFiltreliIhtiyacSahipleriniGetir(kullaniciId, aranan, sehirId, tarih);
            List<IhtiyacSahibiKontrolModel> kontrolListeModel = new List<IhtiyacSahibiKontrolModel>();
            for (int i = 0; i < kontrolEdilecekIhtiyacSahipleri.Count; i++)
            {
                IhtiyacSahibiKontrolModel eklenecekModel = new IhtiyacSahibiKontrolModel();
                eklenecekModel.IhtiyacSahibiAdiSoyadi =
                    kontrolEdilecekIhtiyacSahipleri[i].IhtiyacSahibiTablo.IhtiyacSahibiAdi + " " +
                    kontrolEdilecekIhtiyacSahipleri[i].IhtiyacSahibiTablo.IhtiyacSahibiSoyadi;
                eklenecekModel.IhtiyacSahibiTelNo =
                    kontrolEdilecekIhtiyacSahipleri[i].IhtiyacSahibiTablo.IhtiyacSahibiTelNo;
                eklenecekModel.IhtiyacSahibiAdres =
                    kontrolEdilecekIhtiyacSahipleri[i].IhtiyacSahibiTablo.IhtiyacSahibiAdres;
                eklenecekModel.IhtiyacSahibiKontrolId = kontrolEdilecekIhtiyacSahipleri[i].IhtiyacSahibiKontrolId;
                if (kontrolEdilecekIhtiyacSahipleri[i].MuhtacMi != null)
                {
                    if (kontrolEdilecekIhtiyacSahipleri[i].MuhtacMi == true)
                    {
                        eklenecekModel.MuhtacMi = "Evet";
                    }
                    else
                    {
                        eklenecekModel.MuhtacMi = "Hayır";
                    }
                }
                else
                {
                    eklenecekModel.MuhtacMi = "Hayır";
                }

                eklenecekModel.EklenmeTarih = kontrolEdilecekIhtiyacSahipleri[i].Tarih;
                if (kontrolEdilecekIhtiyacSahipleri[i].KontrolYapilmaTarihi != null)
                {
                    eklenecekModel.KontrolTarih = kontrolEdilecekIhtiyacSahipleri[i].KontrolYapilmaTarihi;
                }

                if (kontrolEdilecekIhtiyacSahipleri[i].TahminiTeslimTarihi != null)
                {
                    eklenecekModel.TahminiTeslimTarihi = kontrolEdilecekIhtiyacSahipleri[i].TahminiTeslimTarihi;
                }

                if (kontrolEdilecekIhtiyacSahipleri[i].TeslimTamamlandiMi != null)
                {
                    if (kontrolEdilecekIhtiyacSahipleri[i].TeslimTamamlandiMi == true)
                    {
                        eklenecekModel.TeslimTamamlandiMi = "Evet";
                    }
                    else
                    {
                        eklenecekModel.TeslimTamamlandiMi = "Hayır";
                    }
                }
                else
                {
                    eklenecekModel.TeslimTamamlandiMi = "Hayır";
                }
            }

            return kontrolListeModel;
        }
    }
}
