using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer.Models.BagisciModelleri;
using BusinessLayer.Models.OrtakModeller;

namespace BusinessLayer.Siniflar
{
    public class Bagisci
    {
        private DataLayer.Siniflar.Bagisci bagisciDAL = new DataLayer.Siniflar.Bagisci();
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
    }
}
