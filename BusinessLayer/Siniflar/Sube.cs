using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer.Models.OrtakModeller;
using BusinessLayer.Models.SubeModelleri;
using DataLayer;

namespace BusinessLayer.Siniflar
{
    public class Sube
    {
        DataLayer.Siniflar.Sube subeDataLayer = new DataLayer.Siniflar.Sube();
        Kullanici kullaniciBusinessLayer = new Kullanici();
        public List<SubeModel> TumSubeleriGetir()
        {
            var subeler = subeDataLayer.TumSubeleriGetir();
            var goruntulenecekSubeler=new List<SubeModel>();
            for (int i = 0; i < subeler.Count; i++)
            {
                SubeModel eklenecekSubeModel= new SubeModel();
                eklenecekSubeModel.Kullanici =
                    kullaniciBusinessLayer.KullaniciGetir(subeler[i].KullaniciBilgileriTablo_KullaniciId);
                eklenecekSubeModel.Sehir=new SehirModel()
                {
                    SehirAdi=subeler[i].SehirTablo.SehirAdi,
                    SehirId=subeler[i].SehirTablo_SehirId
                };
                eklenecekSubeModel.Sira = i;
                eklenecekSubeModel.SubeId = subeler[i].SubeId;
                goruntulenecekSubeler.Add(eklenecekSubeModel);
            }

            return goruntulenecekSubeler;
        }
        public List<SubeModel> FiltreliSubeleriGetir(string aranan)
        {
            var subeler = subeDataLayer.FiltreliSubeleriGetir(aranan);
            var goruntulenecekSubeler = new List<SubeModel>();
            for (int i = 0; i < subeler.Count; i++)
            {
                SubeModel eklenecekSubeModel = new SubeModel();
                eklenecekSubeModel.Kullanici =
                    kullaniciBusinessLayer.KullaniciGetir(subeler[i].KullaniciBilgileriTablo_KullaniciId);
                eklenecekSubeModel.Sehir = new SehirModel()
                {
                    SehirAdi = subeler[i].SehirTablo.SehirAdi,
                    SehirId = subeler[i].SehirTablo_SehirId
                };
                eklenecekSubeModel.Sira = i;
                eklenecekSubeModel.SubeId = subeler[i].SubeId;
                goruntulenecekSubeler.Add(eklenecekSubeModel);
            }

            return goruntulenecekSubeler;
        }
        public bool sehirGorevlisiVarMi(int? SehirId)
        {
            return subeDataLayer.sehirGorevlisiVarMi(SehirId);
        }
        public bool SubeEkle(SubeModel yeniSube)
        {
            SubeTablo eklenecekSube = new SubeTablo()
            {
                SehirTablo_SehirId = yeniSube.Sehir.SehirId,
                KullaniciBilgileriTablo_KullaniciId = yeniSube.KullaniciId
            };
            return subeDataLayer.SubeEkle(eklenecekSube);
        }
        public bool SubeSil(int? id)
        {
            return subeDataLayer.SubeSil(id);
        }

        public SubeModel SubeBul(int? id)
        {
            var Sube = subeDataLayer.SubeBul(id);
            var dondurulecekSube = new SubeModel();
            dondurulecekSube.SubeId = Sube.SubeId;
            dondurulecekSube.Kullanici =
                kullaniciBusinessLayer.KullaniciGetir(Sube.KullaniciBilgileriTablo_KullaniciId);
            dondurulecekSube.Sehir = new SehirModel()
            {
                SehirAdi=Sube.SehirTablo.SehirAdi,
                SehirId=Sube.SehirTablo_SehirId
            };
            return dondurulecekSube;
        }
    }
}
