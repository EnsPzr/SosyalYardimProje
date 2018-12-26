using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer.Models.DisardanIhtiyacSahibiModelleri;
using BusinessLayer.Models.KasaModelleri;
using BusinessLayer.Models.OrtakModeller;
using DataLayer;

namespace BusinessLayer.Siniflar
{
    public class Kasa
    {
        private DataLayer.Siniflar.Kasa kasaDAL = new DataLayer.Siniflar.Kasa();
        public List<KasaModel> TumKasaGetir(int? kullaniciId)
        {
            var kasa = kasaDAL.TumKasaGetir(kullaniciId);
            if (kasa.Count == 0)
            {
                return new List<KasaModel>();
            }
            else
            {
                var kasaList = kasa.Select(p => new KasaModel()
                {
                    Aciklama = p.Aciklama,
                    KasaId = p.KasaId,
                    KullaniciAdiSoyadi = p.KullaniciBilgileriTablo.KullaniciAdi + " " +
                                           p.KullaniciBilgileriTablo.KullaniciSoyadi,
                    KullaniciId = p.KullaniciBilgleriTablo_KullaniciId,
                    Miktar = Convert.ToDouble(p.Miktar),
                    Sehir = new SehirModel()
                    {
                        SehirAdi = p.SehirTablo.SehirAdi,
                        SehirId = p.SehirTablo_SehirId
                    },
                    Tarih = p.Tarih
                }).ToList();
                return kasaList;
            }
        }

        public List<KasaModel> FiltreliKasaGetir(int? kullaniciId, string aranan, string tarih, int? sehirId, int? gelirGider)
        {
            var kasa = kasaDAL.FiltreliKasaGetir(kullaniciId, aranan, tarih, sehirId, gelirGider);
            if (kasa.Count == 0)
            {
                return new List<KasaModel>();
            }
            else
            {
                var kasaList = kasa.Select(p => new KasaModel()
                {
                    Aciklama = p.Aciklama,
                    KasaId = p.KasaId,
                    KullaniciAdiSoyadi = p.KullaniciBilgileriTablo.KullaniciAdi + " " +
                                         p.KullaniciBilgileriTablo.KullaniciSoyadi,
                    KullaniciId = p.KullaniciBilgleriTablo_KullaniciId,
                    Miktar = Convert.ToDouble(p.Miktar),
                    Sehir = new SehirModel()
                    {
                        SehirAdi = p.SehirTablo.SehirAdi,
                        SehirId = p.SehirTablo_SehirId
                    },
                    Tarih = p.Tarih
                }).ToList();
                return kasaList;
            }
        }

        public bool KullaniciIslemYapabilirMi(int? kullaniciId, int? sehirId)
        {
            return kasaDAL.KullaniciIslemYapabilirMi(kullaniciId, sehirId);
        }

        public IslemOnayModel KasaKaydet(int? kullaniciId, KasaModel model)
        {
            IslemOnayModel onay = new IslemOnayModel();
            if (KullaniciIslemYapabilirMi(kullaniciId, model.Sehir.SehirId))
            {
                KasaTablo tablo = new KasaTablo();
                tablo.Aciklama = model.Aciklama;
                if (model.GelirGider == 1)
                {
                    tablo.GelirGider = true;
                }
                else
                {
                    tablo.GelirGider = false;
                }

                tablo.Miktar = model.Miktar;
                tablo.KullaniciBilgleriTablo_KullaniciId = model.KullaniciId;
                tablo.SehirTablo_SehirId = model.Sehir.SehirId;
                tablo.Tarih = model.Tarih;
                onay.TamamlandiMi = kasaDAL.KasaIslemKaydet(tablo);
            }
            else
            {
                onay.TamamlandiMi = false;
                onay.HataMesajlari.Add("Sadece kendi bölgeniz için işlem yapabilirsiniz.");
            }

            return onay;
        }

        public KasaModel KasaGetir(int? kasaId)
        {
            var kasa = kasaDAL.KasaGetir(kasaId);
            if (kasa != null)
            {
                KasaModel kasaModel = new KasaModel();
                kasaModel.KullaniciId = kasa.KullaniciBilgleriTablo_KullaniciId;
                kasaModel.GelirGider = 2;
                if (kasa.GelirGider == true)
                {
                    kasaModel.GelirGider = 1;
                }

                kasaModel.Tarih = kasa.Tarih;
                kasaModel.Aciklama = kasa.Aciklama;
                kasaModel.KasaId = kasa.KasaId;
                kasaModel.Miktar = Convert.ToDouble(kasa.Miktar);
                kasaModel.Sehir.SehirId = kasa.SehirTablo_SehirId;
                kasaModel.Sehir.SehirAdi = kasa.SehirTablo.SehirAdi;
                kasaModel.KullaniciAdiSoyadi = kasa.KullaniciBilgileriTablo.KullaniciAdi + " " +
                                               kasa.KullaniciBilgileriTablo.KullaniciSoyadi;
                return kasaModel;
            }
            else
            {
                return null;
            }
        }

        public IslemOnayModel KasaIslemGuncelle(int? kullaniciId, KasaModel kasa)
        {
            IslemOnayModel onay = new IslemOnayModel();
            if (KullaniciIslemYapabilirMi(kullaniciId, kasa.Sehir.SehirId))
            {
                KasaTablo gunKasa = new KasaTablo();
                gunKasa.Aciklama = kasa.Aciklama;
                gunKasa.GelirGider = false;
                if (kasa.GelirGider == 1)
                {
                    gunKasa.GelirGider = true;
                }

                gunKasa.KasaId = kasa.KasaId;
                gunKasa.Miktar = kasa.Miktar;
                gunKasa.SehirTablo_SehirId = kasa.Sehir.SehirId;
                gunKasa.Tarih = kasa.Tarih;
                onay.TamamlandiMi = kasaDAL.KasaIslemGuncelle(gunKasa);
            }
            else
            {
                onay.TamamlandiMi = false;
                onay.HataMesajlari.Add("Sadce kendi bölgeniz için işlem yapabilirsiniz.");
            }

            return onay;
        }

        public IslemOnayModel KasaSil(int? kullaniciId, int? kasaId)
        {
            IslemOnayModel onay = new IslemOnayModel();
            var kasa = KasaGetir(kasaId);
            if (kasa != null)
            {
                if (KullaniciIslemYapabilirMi(kullaniciId, kasa.Sehir.SehirId))
                {
                    if (kasaDAL.KasaSil(kasaId))
                    {
                        onay.TamamlandiMi = true;
                    }
                    else
                    {
                        onay.TamamlandiMi = false;
                        onay.HataMesajlari.Add("Silme sırasında hata oluştu.");
                    }
                }
                else
                {
                    onay.TamamlandiMi = false;
                    onay.HataMesajlari.Add("Sadece kendi bölgeniz için işlem yapabilirsiniz.");
                }
            }
            else
            {
                onay.TamamlandiMi = false;
                onay.HataMesajlari.Add("Silinecek kasa işlemi bulunamadı.");
            }

            return onay;
        }

        public IslemOnayModel KrediKartiEkleme(KrediKartiKasaModel model)
        {
            IslemOnayModel onay = new IslemOnayModel();
            KullaniciBilgileriTablo kullaniciTablo = new KullaniciBilgileriTablo();
            kullaniciTablo.KullaniciAdi = model.BagisciAdi;
            kullaniciTablo.KullaniciSoyadi = model.BagisciSoyadi;
            kullaniciTablo.KullaniciEPosta = model.BagisciEPosta;
            kullaniciTablo.KullaniciTelefonNumarasi = model.BagisciTelNo;
            kullaniciTablo.BagisciMi = true;
            kullaniciTablo.AktifMi = true;
            kullaniciTablo.KullaniciSifre = "123456";
            KasaTablo kasaTablo = new KasaTablo();
            kasaTablo.Aciklama = "Gelen Bağış";
            kasaTablo.GelirGider = true;
            kasaTablo.Miktar = model.Miktar;
            kasaTablo.SehirTablo_SehirId = model.Sehir.SehirId;
            kasaTablo.Tarih=DateTime.Now;
            if (kasaDAL.KrediKartiEkleme(kullaniciTablo, kasaTablo))
            {
                onay.TamamlandiMi = true;
            }
            else
            {
                onay.TamamlandiMi = false;
                onay.HataMesajlari.Add("Kayıt işlemi yapılamadı.");
            }

            return onay;
        }

        public IslemOnayModel DisardanKartIleBagis(DisardanNakdiBagisModel model)
        {
            KullaniciBilgileriTablo kullaniciTablo = new KullaniciBilgileriTablo();
            kullaniciTablo.KullaniciAdi = model.BagisciAdi;
            kullaniciTablo.KullaniciSoyadi = model.BagisciSoyadi;
            kullaniciTablo.SehirTablo_SehirId = model.SehirBagisci.SehirId;
            kullaniciTablo.KullaniciEPosta = model.BagisciEPosta;
            kullaniciTablo.AktifMi = true;
            kullaniciTablo.BagisciMi = true;
            kullaniciTablo.KullaniciAdres = model.BagisciAdres;
            kullaniciTablo.KullaniciSifre = model.BagisciSifre;
            kullaniciTablo.KullaniciTelefonNumarasi = model.TelNo;

            KasaTablo kasaTablo = new KasaTablo();
            kasaTablo.SehirTablo_SehirId = model.BagisSehir.SehirId;
            kasaTablo.Miktar = model.Miktar;
            kasaTablo.GelirGider = true;
            kasaTablo.Aciklama = "Gelen Bağış";
            kasaTablo.Tarih=DateTime.Now;

            IslemOnayModel onay = new IslemOnayModel();
            onay.TamamlandiMi = kasaDAL.DisardanKartBagis(kullaniciTablo, kasaTablo);
            if (onay.TamamlandiMi == false)
            {
                onay.HataMesajlari.Add("Kullanıcı ekleme işleminde hata oluştu.");
            }

            return onay;
        }

        
    }
}
