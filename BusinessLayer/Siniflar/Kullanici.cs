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
    }
}
