using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer.Models.BagisciModelleri;
using BusinessLayer.Models.OrtakModeller;
using DataLayer;

namespace BusinessLayer.Siniflar
{
    public class Bagisci
    {
        private DataLayer.Siniflar.Bagisci bagisciDAL = new DataLayer.Siniflar.Bagisci();
        private DataLayer.Siniflar.Kullanici kullaniciDAL = new DataLayer.Siniflar.Kullanici();
        public List<BagisciModel> TumBagiscilariGetir(int? KullaniciId)
        {
            var bagiscilar = bagisciDAL.TumBagiscilariGetir(KullaniciId);
            var bagisciList = bagiscilar.Select(p => new BagisciModel()
            {
                BagisciAdi = p.KullaniciAdi,
                BagisciEPosta = p.KullaniciEPosta,
                BagisciId = p.KullaniciId,
                BagisciSifre = p.KullaniciSifre,
                BagisciSoyadi = p.KullaniciSoyadi,
                TelNo = p.KullaniciTelefonNumarasi,
                Sehir = new SehirModel()
                {
                    SehirAdi = p.SehirTablo.SehirAdi,
                    SehirId = p.SehirTablo_SehirId
                },
                Adres=p.KullaniciAdres
            }).ToList();
            return bagisciList;
        }

        public List<BagisciModel> FiltreliBagiscilariGetir(int? KullaniciId, int? SehirId, String aranan)
        {
            var bagiscilar = bagisciDAL.FiltreliBagiscilariGetir(KullaniciId,SehirId,aranan);
            if (bagiscilar.Count == 0)
            {
                return new List<BagisciModel>();
            }
            else
            {
                var BagisciList = new List<BagisciModel>();
                for (int i = 0; i < bagiscilar.Count; i++)
                {
                    var bagisci = new BagisciModel();
                    bagisci.BagisciAdi = bagiscilar[i].KullaniciAdi;
                    bagisci.BagisciEPosta = bagiscilar[i].KullaniciEPosta;
                    bagisci.BagisciId = bagiscilar[i].KullaniciId;
                    bagisci.BagisciSifre = bagiscilar[i].KullaniciSifre;
                    bagisci.BagisciSoyadi = bagiscilar[i].KullaniciSoyadi;
                    bagisci.TelNo = bagiscilar[i].KullaniciTelefonNumarasi;
                    bagisci.Sehir = new SehirModel()
                    {
                        SehirAdi = bagiscilar[i].SehirTablo.SehirAdi,
                        SehirId = bagiscilar[i].SehirTablo_SehirId
                    };
                    bagisci.Adres = bagiscilar[i].KullaniciAdres;
                    BagisciList.Add(bagisci);
                }
                //var bagisciList = bagiscilar.Select(p => new BagisciModel()
                //{
                //    BagisciAdi = p.KullaniciAdi,
                //    BagisciEPosta = p.KullaniciEPosta,
                //    BagisciId = p.KullaniciId,
                //    BagisciSifre = p.KullaniciSifre,
                //    BagisciSoyadi = p.KullaniciSoyadi,
                //    TelNo = p.KullaniciTelefonNumarasi,
                //    Sehir = new SehirModel()
                //    {
                //        SehirAdi = p.SehirTablo.SehirAdi,
                //        SehirId = p.SehirTablo_SehirId
                //    },
                //    Adres = p.KullaniciAdres
                //}).ToList();
                return BagisciList;
            }
        }

        public bool KullaniciIslemYapabilirMi(int? kullaniciId, int? bagisciId)
        {
            return bagisciDAL.KullaniciIslemYapabilirMi(kullaniciId, bagisciId);
        }

        public BagisciModel BagisciBul(int? id)
        {
            var kullanici = kullaniciDAL.KullaniciGetir(id);
            if (kullanici != null)
            {
                BagisciModel model = new BagisciModel();
                model.Adres = kullanici.KullaniciAdres;
                model.BagisciAdi = kullanici.KullaniciAdi;
                model.BagisciSoyadi = kullanici.KullaniciSoyadi;
                model.TelNo = kullanici.KullaniciTelefonNumarasi;
                model.BagisciId = kullanici.KullaniciId;
                model.Sehir.SehirAdi = kullanici.SehirTablo.SehirAdi;
                model.Sehir.SehirId = kullanici.SehirTablo_SehirId;
                model.BagisciEPosta = kullanici.KullaniciEPosta;
                model.BagisciSifre = kullanici.KullaniciSifre;
                return model;
            }
            else
            {
                return null;
            }
        }

        public IslemOnayModel BagisciKaydet(BagisciModel bagisci)
        {
            IslemOnayModel onay = new IslemOnayModel();
            if (bagisciDAL.BagisciIdVarMi(bagisci.BagisciId))
            {
                KullaniciBilgileriTablo kullanici = new KullaniciBilgileriTablo();
                kullanici.KullaniciId = Convert.ToInt32(bagisci.BagisciId);
                kullanici.KullaniciAdi = bagisci.BagisciAdi;
                kullanici.KullaniciSoyadi = bagisci.BagisciSoyadi;
                kullanici.SehirTablo_SehirId = bagisci.Sehir.SehirId;
                kullanici.KullaniciTelefonNumarasi = bagisci.TelNo;
                kullanici.KullaniciEPosta = bagisci.BagisciEPosta;
                kullanici.KullaniciAdres = bagisci.Adres;
                if (!(bagisciDAL.BagiscidanVarMi(kullanici)))
                {
                    onay.TamamlandiMi = false;
                    onay.HataMesajlari.Add("Aynı eposta adresine sahip başka bir bağışçı var.");
                }
                else
                {
                    bagisciDAL.BagisciKaydet(kullanici);
                    onay.TamamlandiMi = true;
                }
            }
            else
            {
                onay.TamamlandiMi = false;
                onay.HataMesajlari.Add("Aranan bağışçı bulunamadı.");
            }

            return onay;
        }
    }
}
