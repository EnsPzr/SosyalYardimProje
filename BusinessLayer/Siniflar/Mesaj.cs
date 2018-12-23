using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer.Models.MesajModelleri;

namespace BusinessLayer.Siniflar
{
    public class Mesaj
    {
        private DataLayer.Siniflar.Mesaj mesajDAL = new DataLayer.Siniflar.Mesaj();

        public List<MesajModel> TumMesajlariGetir(int? kullaniciId)
        {
            var mesajlar = mesajDAL.TumMesajlariGetir(kullaniciId);
            var gonMesajlar = mesajlar.Select(p => new MesajModel()
            {
                AliciInt=p.KimeAtildi,
                AliciStr = p.KimeAtildi!=null?p.KimeAtildi==0?"Herkes":"Koordinatörler":"Hiç Kimse",
                KullaniciAdiSoyadi=p.KullaniciBilgileriTablo.KullaniciAdi+" "+p.KullaniciBilgileriTablo.KullaniciSoyadi,
                KullaniciId=p.KullaniciBilgleriTablo_KullaniciId,
                MesajId=p.MesajId,
                Tarih=p.Tarih
            }).ToList();
            return gonMesajlar;
        }

        public List<MesajModel> FiltreliMesajlariGetir(int? kullaniciId, int? arananKullaniciId, string aranan,
            string tarih, int? kimeGonderildi)
        {
            var mesajlar = mesajDAL.FiltreliMesajlariGetir(kullaniciId, arananKullaniciId, aranan, tarih, kimeGonderildi);
            var gonMesajlar = mesajlar.Select(p => new MesajModel()
            {
                AliciInt = p.KimeAtildi,
                AliciStr = p.KimeAtildi != null ? p.KimeAtildi == 0 ? "Herkes" : "Koordinatörler" : "Hiç Kimse",
                KullaniciAdiSoyadi = p.KullaniciBilgileriTablo.KullaniciAdi + " " + p.KullaniciBilgileriTablo.KullaniciSoyadi,
                KullaniciId = p.KullaniciBilgleriTablo_KullaniciId,
                MesajId = p.MesajId,
                Tarih = p.Tarih
            }).ToList();
            return gonMesajlar;
        }
    }
}
