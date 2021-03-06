﻿using System;
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

        public IslemOnayModel SubeGuncelle(SubeModel duzenlenmisSube, int? kullaniciId)
        {
            IslemOnayModel onay = new IslemOnayModel();
            if (subeDataLayer.KullaniciMerkezdeMi(kullaniciId))
            {
                var duzenlenecekSube = subeDataLayer.SubeBul(duzenlenmisSube.SubeId);
                if (duzenlenecekSube != null)
                {
                    var ayniSubedenVarMi =
                        subeDataLayer.sehirGorevlisiVarMi(duzenlenmisSube.Sehir.SehirId, duzenlenmisSube.SubeId);
                    if (!ayniSubedenVarMi)
                    {
                        duzenlenecekSube.KullaniciBilgileriTablo_KullaniciId = duzenlenmisSube.KullaniciId;
                        duzenlenecekSube.SehirTablo_SehirId = duzenlenmisSube.Sehir.SehirId;
                        if (subeDataLayer.SubeGuncelle(duzenlenecekSube))
                        {
                            onay.TamamlandiMi = true;
                        }
                        else
                        {
                            onay.TamamlandiMi = false;
                            onay.HataMesajlari.Add("Bilinmeyen bir hata oluştu.");
                        }
                        return onay;
                    }
                    else
                    {
                        onay.TamamlandiMi = false;
                        onay.HataMesajlari.Add("Güncel bilgilerini girdiğiniz şubenin zaten bir görevlisi var.");
                        return onay;
                    }

                }
                else
                {
                    onay.TamamlandiMi = false;
                    onay.HataMesajlari.Add("Güncellemek istediğiniz şube bulunamadı.");
                    return onay;
                }
            }
            else
            {
                onay.TamamlandiMi = false;
                onay.HataMesajlari.Add("Şube güncellemek için yetkiniz bulunmamaktadır.");
                return onay;
            }
        }
        public SubeModel SubeBul(int? id)
        {
            var Sube = subeDataLayer.SubeBul(id);
            var dondurulecekSube = new SubeModel();
            dondurulecekSube.SubeId = Sube.SubeId;
            dondurulecekSube.Kullanici =
                kullaniciBusinessLayer.KullaniciGetir(Sube.KullaniciBilgileriTablo_KullaniciId);
            dondurulecekSube.KullaniciId = Sube.KullaniciBilgileriTablo_KullaniciId;
            dondurulecekSube.Sehir = new SehirModel()
            {
                SehirAdi=Sube.SehirTablo.SehirAdi,
                SehirId=Sube.SehirTablo_SehirId
            };
            return dondurulecekSube;
        }

        public bool KullaniciMerkezdeMi(int? kullaniciId)
        {
            return subeDataLayer.KullaniciMerkezdeMi(kullaniciId);
        }
    }
}
