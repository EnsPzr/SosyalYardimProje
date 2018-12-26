using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer.Models.KullaniciModelleri;
using BusinessLayer.Models.OrtakModeller;
using DataLayer;

namespace BusinessLayer.Siniflar
{
    public class Kullanici
    {
        private DataLayer.Siniflar.Kullanici KullaniciDataLayer = new DataLayer.Siniflar.Kullanici();
        BusinessLayer.KullaniciYonetimi kullaniciYonetimi = new KullaniciYonetimi();
        public List<KullaniciModel> TumKullanicilariGetir(int? kullaniciId)
        {
            var Kullanici = kullaniciYonetimi.LoginKullaniciBul(kullaniciId);
            if (Convert.ToBoolean(Kullanici.KullaniciMerkezdeMi))
            {
                var kullanicilar = KullaniciDataLayer.TumKullanicilariGetir().Where(p=>p.BagisciMi==false).ToList();
                List<KullaniciModel> dondurulecekKullanicilar= new List<KullaniciModel>();
                for (int i = 0; i < kullanicilar.Count; i++)
                {
                    var eklenecekKullanici = new KullaniciModel();
                    eklenecekKullanici.AktifMi = kullanicilar[i].AktifMi != null
                        ? kullanicilar[i].AktifMi == true ? true : false
                        : false;
                    eklenecekKullanici.KullaniciSoyadi = kullanicilar[i].KullaniciSoyadi;
                    eklenecekKullanici.KullaniciAdi = kullanicilar[i].KullaniciAdi;
                    eklenecekKullanici.KullaniciId = kullanicilar[i].KullaniciId;
                    eklenecekKullanici.Sehir.SehirAdi = kullanicilar[i].SehirTablo!=null? kullanicilar[i].SehirTablo.SehirAdi:string.Empty;
                    eklenecekKullanici.Sehir.SehirId = kullanicilar[i].SehirTablo_SehirId;
                    eklenecekKullanici.KullaniciOnayliMi = kullanicilar[i].KullaniciOnayliMi != null
                        ? kullanicilar[i].KullaniciOnayliMi == true ? true : false
                        : false;
                    eklenecekKullanici.KullaniciTelegramKullaniciAdi = kullanicilar[i].KullaniciTelegramKullaniciAdi;
                    eklenecekKullanici.KullaniciTCKimlik = kullanicilar[i].KullaniciTCKimlikNumarasi;
                    eklenecekKullanici.KullaniciTelNo = kullanicilar[i].KullaniciTelefonNumarasi;
                    eklenecekKullanici.KullaniciMerkezdeMi = kullanicilar[i].KullaniciMerkezdeMi != null
                        ? kullanicilar[i].KullaniciMerkezdeMi == true ? true : false
                        : false;
                    eklenecekKullanici.KullaniciEPosta = kullanicilar[i].KullaniciEPosta;
                    eklenecekKullanici.AktifMiStr= kullanicilar[i].AktifMi != null
                        ? kullanicilar[i].AktifMi == true ? "Evet" : "Hayır"
                        : "Hayır";
                    eklenecekKullanici.KullaniciOnayliMiStr = kullanicilar[i].KullaniciOnayliMi != null
                        ? kullanicilar[i].KullaniciOnayliMi == true ? "Evet" : "Hayır"
                        : "Hayır";
                    eklenecekKullanici.KullaniciMerkezdeMiStr = kullanicilar[i].KullaniciMerkezdeMi != null
                        ? kullanicilar[i].KullaniciMerkezdeMi == true ? "Evet" : "Hayır"
                        : "Hayır";
                    eklenecekKullanici.Sira = i;
                    dondurulecekKullanicilar.Add(eklenecekKullanici);
                }

                //var dondurulecekKullanicilar = kullanicilar.Select(p => new KullaniciModel()
                //{
                //    AktifMi = p.AktifMi != null ? p.AktifMi == true ? true : false : false,
                //    KullaniciAdi = p.KullaniciAdi,
                //    KullaniciEPosta = p.KullaniciEPosta,
                //    KullaniciMerkezdeMi = p.KullaniciMerkezdeMi != null ? p.KullaniciMerkezdeMi == true ? true : false : false,
                //    KullaniciSoyadi = p.KullaniciSoyadi,
                //    Sehir = new SehirModel()
                //    {
                //        SehirAdi = p.SehirTablo.SehirAdi,
                //        SehirId = p.SehirTablo.SehirId
                //    },
                //    KullaniciOnayliMi = p.KullaniciOnayliMi != null ? p.KullaniciOnayliMi == true ? true : false : false,
                //    KullaniciTCKimlik = p.KullaniciTCKimlikNumarasi,
                //    KullaniciTelegramKullaniciAdi = p.KullaniciTelegramKullaniciAdi,
                //    KullaniciTelNo = p.KullaniciTelefonNumarasi,
                //    KullaniciId = p.KullaniciId
                //}).ToList();
                return dondurulecekKullanicilar.OrderBy(p => p.Sira).ToList();
            }
            else
            {
                var kullanicilar = KullaniciDataLayer.TumKullanicilariGetir();
                List<KullaniciModel> dondurulecekKullanicilar = new List<KullaniciModel>();
                for (int i = 0; i < kullanicilar.Count; i++)
                {
                    var eklenecekKullanici = new KullaniciModel();
                    eklenecekKullanici.AktifMi = kullanicilar[i].AktifMi != null
                        ? kullanicilar[i].AktifMi == true ? true : false
                        : false;
                    eklenecekKullanici.KullaniciSoyadi = kullanicilar[i].KullaniciSoyadi;
                    eklenecekKullanici.KullaniciAdi = kullanicilar[i].KullaniciAdi;
                    eklenecekKullanici.KullaniciId = kullanicilar[i].KullaniciId;
                    eklenecekKullanici.Sehir.SehirAdi = kullanicilar[i].SehirTablo != null ? kullanicilar[i].SehirTablo.SehirAdi : string.Empty;
                    eklenecekKullanici.Sehir.SehirId = kullanicilar[i].SehirTablo_SehirId;
                    eklenecekKullanici.KullaniciOnayliMi = kullanicilar[i].KullaniciOnayliMi != null
                        ? kullanicilar[i].KullaniciOnayliMi == true ? true : false
                        : false;
                    eklenecekKullanici.KullaniciTelegramKullaniciAdi = kullanicilar[i].KullaniciTelegramKullaniciAdi;
                    eklenecekKullanici.KullaniciTCKimlik = kullanicilar[i].KullaniciTCKimlikNumarasi;
                    eklenecekKullanici.KullaniciTelNo = kullanicilar[i].KullaniciTelefonNumarasi;
                    eklenecekKullanici.KullaniciMerkezdeMi = kullanicilar[i].KullaniciMerkezdeMi != null
                        ? kullanicilar[i].KullaniciMerkezdeMi == true ? true : false
                        : false;
                    eklenecekKullanici.KullaniciEPosta = kullanicilar[i].KullaniciEPosta;
                    eklenecekKullanici.AktifMiStr = kullanicilar[i].AktifMi != null
                        ? kullanicilar[i].AktifMi == true ? "Evet" : "Hayır"
                        : "Hayır";
                    eklenecekKullanici.KullaniciOnayliMiStr = kullanicilar[i].KullaniciOnayliMi != null
                        ? kullanicilar[i].KullaniciOnayliMi == true ? "Evet" : "Hayır"
                        : "Hayır";
                    eklenecekKullanici.KullaniciMerkezdeMiStr = kullanicilar[i].KullaniciMerkezdeMi != null
                        ? kullanicilar[i].KullaniciMerkezdeMi == true ? "Evet" : "Hayır"
                        : "Hayır";
                    eklenecekKullanici.Sira = i;
                    dondurulecekKullanicilar.Add(eklenecekKullanici);
                }
                //var dondurulecekKullanicilar = kullanicilar.Select(p => new KullaniciModel()
                //{
                //    AktifMi = p.AktifMi != null ? p.AktifMi == true ? true : false : false,
                //    KullaniciAdi = p.KullaniciAdi,
                //    KullaniciEPosta = p.KullaniciEPosta,
                //    KullaniciMerkezdeMi = p.KullaniciMerkezdeMi != null ? p.KullaniciMerkezdeMi == true ? true : false : false,
                //    KullaniciSoyadi = p.KullaniciSoyadi,
                //    Sehir = new SehirModel()
                //    {
                //        SehirAdi = p.SehirTablo.SehirAdi,
                //        SehirId = p.SehirTablo.SehirId
                //    },
                //    KullaniciOnayliMi = p.KullaniciOnayliMi != null ? p.KullaniciOnayliMi == true ? true : false : false,
                //    KullaniciTCKimlik = p.KullaniciTCKimlikNumarasi,
                //    KullaniciTelegramKullaniciAdi = p.KullaniciTelegramKullaniciAdi,
                //    KullaniciTelNo = p.KullaniciTelefonNumarasi,
                //    KullaniciId = p.KullaniciId
                //}).ToList();
                return dondurulecekKullanicilar.OrderBy(p=>p.Sira).ToList();
            }
        }
        public List<KullaniciModel> FiltreliKullanicilariGetir(string aranan, int? sehirId, int? kullaniciId, bool? OnayliMi, bool? merkezdemi, bool? aktifMi)
        {
            var Kullanici = kullaniciYonetimi.LoginKullaniciBul(kullaniciId);
            if (Convert.ToBoolean(Kullanici.KullaniciMerkezdeMi))
            {
                var kullanicilar = KullaniciDataLayer.FiltreliKullanicilariGetir(aranan,sehirId,kullaniciId,OnayliMi,merkezdemi,aktifMi).Where(p => p.BagisciMi == false).ToList();
                List<KullaniciModel> dondurulecekKullanicilar = new List<KullaniciModel>();
                for (int i = 0; i < kullanicilar.Count; i++)
                {
                    var eklenecekKullanici = new KullaniciModel();
                    eklenecekKullanici.AktifMi = kullanicilar[i].AktifMi != null
                        ? kullanicilar[i].AktifMi == true ? true : false
                        : false;
                    eklenecekKullanici.KullaniciSoyadi = kullanicilar[i].KullaniciSoyadi;
                    eklenecekKullanici.KullaniciAdi = kullanicilar[i].KullaniciAdi;
                    eklenecekKullanici.KullaniciId = kullanicilar[i].KullaniciId;
                    eklenecekKullanici.Sehir.SehirAdi = kullanicilar[i].SehirTablo != null ? kullanicilar[i].SehirTablo.SehirAdi : string.Empty;
                    eklenecekKullanici.Sehir.SehirId = kullanicilar[i].SehirTablo_SehirId;
                    eklenecekKullanici.KullaniciOnayliMi = kullanicilar[i].KullaniciOnayliMi != null
                        ? kullanicilar[i].KullaniciOnayliMi == true ? true : false
                        : false;
                    eklenecekKullanici.KullaniciTelegramKullaniciAdi = kullanicilar[i].KullaniciTelegramKullaniciAdi;
                    eklenecekKullanici.KullaniciTCKimlik = kullanicilar[i].KullaniciTCKimlikNumarasi;
                    eklenecekKullanici.KullaniciTelNo = kullanicilar[i].KullaniciTelefonNumarasi;
                    eklenecekKullanici.KullaniciMerkezdeMi = kullanicilar[i].KullaniciMerkezdeMi != null
                        ? kullanicilar[i].KullaniciMerkezdeMi == true ? true : false
                        : false;
                    eklenecekKullanici.KullaniciEPosta = kullanicilar[i].KullaniciEPosta;
                    eklenecekKullanici.AktifMiStr = kullanicilar[i].AktifMi != null
                        ? kullanicilar[i].AktifMi == true ? "Evet" : "Hayır"
                        : "Hayır";
                    eklenecekKullanici.KullaniciOnayliMiStr = kullanicilar[i].KullaniciOnayliMi != null
                        ? kullanicilar[i].KullaniciOnayliMi == true ? "Evet" : "Hayır"
                        : "Hayır";
                    eklenecekKullanici.KullaniciMerkezdeMiStr = kullanicilar[i].KullaniciMerkezdeMi != null
                        ? kullanicilar[i].KullaniciMerkezdeMi == true ? "Evet" : "Hayır"
                        : "Hayır";
                    eklenecekKullanici.Sira = i;
                    dondurulecekKullanicilar.Add(eklenecekKullanici);
                }
                return dondurulecekKullanicilar.OrderBy(p => p.Sira).ToList();
            }
            else
            {
                var kullanicilar = KullaniciDataLayer.FiltreliKullanicilariGetir(aranan, sehirId, kullaniciId, OnayliMi, merkezdemi, aktifMi).Where(p => p.BagisciMi == false).ToList();
                List<KullaniciModel> dondurulecekKullanicilar = new List<KullaniciModel>();
                for (int i = 0; i < kullanicilar.Count; i++)
                {
                    var eklenecekKullanici = new KullaniciModel();
                    eklenecekKullanici.AktifMi = kullanicilar[i].AktifMi != null
                        ? kullanicilar[i].AktifMi == true ? true : false
                        : false;
                    eklenecekKullanici.KullaniciSoyadi = kullanicilar[i].KullaniciSoyadi;
                    eklenecekKullanici.KullaniciAdi = kullanicilar[i].KullaniciAdi;
                    eklenecekKullanici.KullaniciId = kullanicilar[i].KullaniciId;
                    eklenecekKullanici.Sehir.SehirAdi = kullanicilar[i].SehirTablo != null ? kullanicilar[i].SehirTablo.SehirAdi : string.Empty;
                    eklenecekKullanici.Sehir.SehirId = kullanicilar[i].SehirTablo_SehirId;
                    eklenecekKullanici.KullaniciOnayliMi = kullanicilar[i].KullaniciOnayliMi != null
                        ? kullanicilar[i].KullaniciOnayliMi == true ? true : false
                        : false;
                    eklenecekKullanici.KullaniciTelegramKullaniciAdi = kullanicilar[i].KullaniciTelegramKullaniciAdi;
                    eklenecekKullanici.KullaniciTCKimlik = kullanicilar[i].KullaniciTCKimlikNumarasi;
                    eklenecekKullanici.KullaniciTelNo = kullanicilar[i].KullaniciTelefonNumarasi;
                    eklenecekKullanici.KullaniciMerkezdeMi = kullanicilar[i].KullaniciMerkezdeMi != null
                        ? kullanicilar[i].KullaniciMerkezdeMi == true ? true : false
                        : false;
                    eklenecekKullanici.KullaniciEPosta = kullanicilar[i].KullaniciEPosta;
                    eklenecekKullanici.AktifMiStr = kullanicilar[i].AktifMi != null
                        ? kullanicilar[i].AktifMi == true ? "Evet" : "Hayır"
                        : "Hayır";
                    eklenecekKullanici.KullaniciOnayliMiStr = kullanicilar[i].KullaniciOnayliMi != null
                        ? kullanicilar[i].KullaniciOnayliMi == true ? "Evet" : "Hayır"
                        : "Hayır";
                    eklenecekKullanici.KullaniciMerkezdeMiStr = kullanicilar[i].KullaniciMerkezdeMi != null
                        ? kullanicilar[i].KullaniciMerkezdeMi == true ? "Evet" : "Hayır"
                        : "Hayır";
                    eklenecekKullanici.Sira = i;
                    dondurulecekKullanicilar.Add(eklenecekKullanici);
                }
                return dondurulecekKullanicilar.OrderBy(p => p.Sira).ToList();
            }
        }
        public List<SehirModel> SehirleriGetir(int? kullaniciId)
        {
            var kullanici = kullaniciYonetimi.LoginKullaniciBul(kullaniciId);
            if (Convert.ToBoolean(kullanici.KullaniciMerkezdeMi))
            {
                return KullaniciDataLayer.TumSehirler().Select(p=>new SehirModel()
                {
                    SehirAdi=p.SehirAdi,
                    SehirId=p.SehirId
                }).ToList();
            }
            else
            {
                return KullaniciDataLayer.TumSehirler().Where(p=>p.SehirId==kullanici.SehirTablo_SehirId).Select(p => new SehirModel()
                {
                    SehirAdi = p.SehirAdi,
                    SehirId = p.SehirId
                }).ToList();
            }
        }

        public List<SehirModel> TumSehirleriGetir()
        {
            return KullaniciDataLayer.TumSehirler().Select(p=>new SehirModel()
            {
                SehirAdi=p.SehirAdi,
                SehirId=p.SehirId
            }).ToList();
        }

        public KullaniciModel KullaniciGetir(int? id)
        {
            var kullanici= KullaniciDataLayer.KullaniciGetir(id);
            if (kullanici != null)
            {
                KullaniciModel goruntulenecekKullanici = new KullaniciModel();
                goruntulenecekKullanici.AktifMi = kullanici.AktifMi != null
                    ? kullanici.AktifMi == true ? true : false
                    : false;
                goruntulenecekKullanici.KullaniciSoyadi = kullanici.KullaniciSoyadi;
                goruntulenecekKullanici.KullaniciAdi = kullanici.KullaniciAdi;
                goruntulenecekKullanici.KullaniciId = kullanici.KullaniciId;
                goruntulenecekKullanici.Sehir.SehirAdi = kullanici.SehirTablo != null ? kullanici.SehirTablo.SehirAdi : string.Empty;
                goruntulenecekKullanici.Sehir.SehirId = kullanici.SehirTablo_SehirId;
                goruntulenecekKullanici.KullaniciOnayliMi = kullanici.KullaniciOnayliMi != null
                    ? kullanici.KullaniciOnayliMi == true ? true : false
                    : false;
                goruntulenecekKullanici.KullaniciTelegramKullaniciAdi = kullanici.KullaniciTelegramKullaniciAdi;
                goruntulenecekKullanici.KullaniciTCKimlik = kullanici.KullaniciTCKimlikNumarasi;
                goruntulenecekKullanici.KullaniciTelNo = kullanici.KullaniciTelefonNumarasi;
                goruntulenecekKullanici.KullaniciMerkezdeMi = kullanici.KullaniciMerkezdeMi != null
                    ? kullanici.KullaniciMerkezdeMi == true ? true : false
                    : false;
                goruntulenecekKullanici.KullaniciEPosta = kullanici.KullaniciEPosta;
                goruntulenecekKullanici.AktifMiStr = kullanici.AktifMi != null
                    ? kullanici.AktifMi == true ? "Evet" : "Hayır"
                    : "Hayır";
                goruntulenecekKullanici.KullaniciOnayliMiStr = kullanici.KullaniciOnayliMi != null
                    ? kullanici.KullaniciOnayliMi == true ? "Evet" : "Hayır"
                    : "Hayır";
                goruntulenecekKullanici.KullaniciMerkezdeMiStr = kullanici.KullaniciMerkezdeMi != null
                    ? kullanici.KullaniciMerkezdeMi == true ? "Evet" : "Hayır"
                    : "Hayır";
                return goruntulenecekKullanici;
            }
            else
            {
                return null;
            }
        }

        public bool KullaniciSil(int? id)
        {
            return KullaniciDataLayer.KullaniciSil(id);
        }
        public bool KullaniciVarMi(String eposta)
        {
            return KullaniciDataLayer.KullaniciVarMi(eposta);
        }

        public bool KullaniciVarMi(String eposta, int? id)
        {
            return KullaniciDataLayer.KullaniciVarMi(eposta, id);
        }
        public bool KullaniciEkle(KullaniciModel yeniKullanici)
        {
            KullaniciBilgileriTablo eklenecekKullanici = new KullaniciBilgileriTablo();
            eklenecekKullanici.AktifMi = yeniKullanici.AktifMi;
            eklenecekKullanici.BagisciMi = false;
            eklenecekKullanici.KullaniciAdi = yeniKullanici.KullaniciAdi;
            eklenecekKullanici.KullaniciEPosta = yeniKullanici.KullaniciEPosta;
            eklenecekKullanici.KullaniciMerkezdeMi = yeniKullanici.KullaniciMerkezdeMi;
            eklenecekKullanici.KullaniciSifre = yeniKullanici.KullaniciSifre;
            eklenecekKullanici.KullaniciTCKimlikNumarasi = yeniKullanici.KullaniciTCKimlik;
            eklenecekKullanici.KullaniciSoyadi = yeniKullanici.KullaniciSoyadi;
            eklenecekKullanici.SehirTablo_SehirId = yeniKullanici.Sehir.SehirId;
            eklenecekKullanici.KullaniciOnayliMi = yeniKullanici.KullaniciOnayliMi;
            eklenecekKullanici.KullaniciTelegramKullaniciAdi = yeniKullanici.KullaniciTelegramKullaniciAdi;
            eklenecekKullanici.KullaniciTelefonNumarasi = yeniKullanici.KullaniciTelNo;
            return KullaniciDataLayer.KullaniciEkle(eklenecekKullanici);
        }

        public bool KullaniciGuncelle(KullaniciModel duzenlenmisKullanici)
        {
            var guncellenecekKullanici = KullaniciDataLayer.KullaniciGetir(duzenlenmisKullanici.KullaniciId);
            if (guncellenecekKullanici != null)
            {
                guncellenecekKullanici.AktifMi = duzenlenmisKullanici.AktifMi;
                guncellenecekKullanici.BagisciMi = false;
                guncellenecekKullanici.KullaniciAdi = duzenlenmisKullanici.KullaniciAdi;
                guncellenecekKullanici.KullaniciEPosta = duzenlenmisKullanici.KullaniciEPosta;
                guncellenecekKullanici.KullaniciMerkezdeMi = duzenlenmisKullanici.KullaniciMerkezdeMi;
                guncellenecekKullanici.KullaniciTCKimlikNumarasi = duzenlenmisKullanici.KullaniciTCKimlik;
                guncellenecekKullanici.KullaniciSoyadi = duzenlenmisKullanici.KullaniciSoyadi;
                guncellenecekKullanici.SehirTablo_SehirId = duzenlenmisKullanici.Sehir.SehirId;
                guncellenecekKullanici.KullaniciOnayliMi = duzenlenmisKullanici.KullaniciOnayliMi;
                guncellenecekKullanici.KullaniciTelegramKullaniciAdi = duzenlenmisKullanici.KullaniciTelegramKullaniciAdi;
                guncellenecekKullanici.KullaniciTelefonNumarasi = duzenlenmisKullanici.KullaniciTelNo;
                return KullaniciDataLayer.KullaniciGuncelle(guncellenecekKullanici);
            }
            else
            {
                return false;
            }
        }
        
    }
}
