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
    }
}
