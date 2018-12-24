using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer.Models.BagisciGiris;
using BusinessLayer.Models.KullaniciModelleri;
using BusinessLayer.Models.OrtakModeller;
using DataLayer;

namespace BusinessLayer.BagisciSiniflar
{
    public class BagisciYonetimi
    {
        private DataLayer.BagisciSiniflar.BagisciYonetimi bagisciDAL = new DataLayer.BagisciSiniflar.BagisciYonetimi();

        public KullaniciModel BagisciBul(String ePosta, String sifre)
        {
            var bagisci= bagisciDAL.BagisciBul(ePosta, sifre);
            if (bagisci != null)
            {
                var bagisModel=new KullaniciModel()
                {
                    KullaniciId=bagisci.KullaniciId,
                    KullaniciAdi=bagisci.KullaniciAdi,
                    KullaniciSoyadi = bagisci.KullaniciSoyadi
                };
                return bagisModel;
            }
            else
            {
                return null;
            }
        }

        public KullaniciBilgileriTablo LoginKullaniciBul(int? KullaniciId)
        {
            return bagisciDAL.KullaniciBul(KullaniciId);
        }

        public List<BagisciAnaSayfaModel> AnaSayfaBagislariGetir(int? kullaniciId)
        {
            var bagislar = bagisciDAL.BagislariGetir(kullaniciId);
            List<BagisciAnaSayfaModel> anaSayfaModelList = new List<BagisciAnaSayfaModel>();

            for (int i = 0; i < bagislar.Count; i++)
            {
                var bagisDetay=bagisciDAL.BagisDetayBul(bagislar[i].BagisId);
                for (int j = 0; j < bagisDetay.Count; j++)
                {
                    var eklenecek = new BagisciAnaSayfaModel();
                    eklenecek.TahminiAlinmaTarihi = bagislar[i].TahminiTeslimAlmaTarihi != null
                        ? bagislar[i].TahminiTeslimAlmaTarihi.Value.ToShortDateString()
                        : "";
                    eklenecek.Adet = bagisDetay[j].Adet;
                    eklenecek.AlinacakMi = bagisDetay[j].AlinacakMi != null
                        ? bagisDetay[j].AlinacakMi == true ? "Evet" : "Hayır"
                        : "Hayır";
                    eklenecek.EsyaAdi = bagisDetay[j].EsyaTablo.EsyaAdi;
                    eklenecek.OnaylandiMi = bagislar[i].OnaylandiMi != null
                        ? bagislar[i].OnaylandiMi == true ? "Evet" : "Hayır"
                        : "Hayır";
                    anaSayfaModelList.Add(eklenecek);
                }
            }

            return anaSayfaModelList;
        }

        public bool BagisciVarMi(string ePosta)
        {
            return bagisciDAL.BagisciVarMi(ePosta);
        }

        public IslemOnayModel BagisciKaydet(BagisciKayitModel bagisciModel)
        {
            IslemOnayModel onay = new IslemOnayModel();
            if (!(BagisciVarMi(bagisciModel.BagisciEPosta)))
            {
                KullaniciBilgileriTablo kullanici = new KullaniciBilgileriTablo();
                kullanici.KullaniciAdi = bagisciModel.BagisciAdi;
                kullanici.KullaniciSoyadi = bagisciModel.BagisciSoyadi;
                kullanici.SehirTablo_SehirId = bagisciModel.SehirId;
                kullanici.KullaniciTelefonNumarasi = bagisciModel.BagisciTelNo;
                kullanici.BagisciMi = true;
                kullanici.KullaniciEPosta = bagisciModel.BagisciEPosta;
                kullanici.KullaniciSifre = bagisciModel.BagisciSifre;
                kullanici.KullaniciAdres = bagisciModel.BagisciAdres;
                onay.TamamlandiMi= bagisciDAL.BagisciKaydet(kullanici);
            }
            else
            {
                onay.TamamlandiMi = false;
                onay.HataMesajlari.Add("Bu e posta hesabı kullanımdadır.");
            }
            return onay;
        }

        public BagisciKayitModel BagisciGetir(int? bagisciId)
        {
            var bagisci = bagisciDAL.BagisciGetir(bagisciId);
            if (bagisci != null)
            {
                var donBagisci = new BagisciKayitModel();
                donBagisci.SehirId = bagisci.SehirTablo_SehirId;
                donBagisci.BagisciAdi = bagisci.KullaniciAdi;
                donBagisci.BagisciAdres = bagisci.KullaniciAdres;
                donBagisci.BagisciEPosta = bagisci.KullaniciEPosta;
                donBagisci.BagisciSoyadi = bagisci.KullaniciSoyadi;
                donBagisci.BagisciTelNo = bagisci.KullaniciTelefonNumarasi;
                donBagisci.BagisciId = bagisci.KullaniciId;
                return donBagisci;
            }
            else
            {
                return null;
            }
        }

        public IslemOnayModel BagisciGuncelle(BagisciKayitModel model)
        {
            IslemOnayModel onay = new IslemOnayModel();
            KullaniciBilgileriTablo kulTablo = new KullaniciBilgileriTablo();
            kulTablo.KullaniciAdi = model.BagisciAdi;
            kulTablo.KullaniciSoyadi = model.BagisciSoyadi;
            kulTablo.KullaniciAdres = model.BagisciAdres;
            kulTablo.KullaniciEPosta = model.BagisciEPosta;
            kulTablo.SehirTablo_SehirId = model.SehirId;
            kulTablo.KullaniciSifre = model.BagisciSifre;
            kulTablo.KullaniciTelefonNumarasi = model.BagisciTelNo;
            kulTablo.KullaniciId = model.BagisciId;
            onay.TamamlandiMi = bagisciDAL.BagisciGuncelle(kulTablo);
            if (onay.TamamlandiMi == false)
            {
                onay.HataMesajlari.Add("Hata oluştu.");
            }
            return onay;
        }
    }
}
