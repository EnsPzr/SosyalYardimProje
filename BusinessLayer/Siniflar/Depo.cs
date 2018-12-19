using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer.Models;
using BusinessLayer.Models.EsyaModelleri;
using BusinessLayer.Models.OrtakModeller;
using DataLayer;

namespace BusinessLayer.Siniflar
{
    public class Depo
    {
        private DataLayer.Siniflar.Depo depoDAL = new DataLayer.Siniflar.Depo();
        public List<DepoModel> DepoGetir(int? KullaniciId)
        {
            List<DepoModel> depoListe = new List<DepoModel>();
            var depoEsyalari = depoDAL.DepoGetir(KullaniciId);
            for (int i = 0; i < depoEsyalari.Count; i++)
            {
                DepoModel gonderilecekDepo = new DepoModel();
                gonderilecekDepo.Adet = depoEsyalari[i].Adet;
                gonderilecekDepo.DepoEsyaId = depoEsyalari[i].DepoEsyaId;
                gonderilecekDepo.EsyaId = depoEsyalari[i].EsyaTablo.EsyaId;
                gonderilecekDepo.EsyaAdi = depoEsyalari[i].EsyaTablo.EsyaAdi;
                gonderilecekDepo.Sehir = new SehirModel()
                {
                    SehirAdi = depoEsyalari[i].SehirTablo.SehirAdi,
                    SehirId = depoEsyalari[i].SehirTablo_SehirId
                };
                depoListe.Add(gonderilecekDepo);
            }

            return depoListe.OrderBy(p => p.Sehir.SehirId).ToList();
        }

        public List<DepoModel> FiltreliDepoGetir(int? KullaniciId, int? esyaId, int? sehirId, String aranan)
        {
            List<DepoModel> depoListe = new List<DepoModel>();
            var depoEsyalari = depoDAL.FiltreliDepoGetir(KullaniciId, esyaId, sehirId, aranan);
            for (int i = 0; i < depoEsyalari.Count; i++)
            {
                DepoModel gonderilecekDepo = new DepoModel();
                gonderilecekDepo.Adet = depoEsyalari[i].Adet;
                gonderilecekDepo.DepoEsyaId = depoEsyalari[i].DepoEsyaId;
                gonderilecekDepo.EsyaId = depoEsyalari[i].EsyaTablo.EsyaId;
                gonderilecekDepo.EsyaAdi = depoEsyalari[i].EsyaTablo.EsyaAdi;
                gonderilecekDepo.Sehir = new SehirModel()
                {
                    SehirAdi = depoEsyalari[i].SehirTablo.SehirAdi,
                    SehirId = depoEsyalari[i].SehirTablo_SehirId
                };
                depoListe.Add(gonderilecekDepo);
            }

            return depoListe.OrderBy(p => p.Sehir.SehirId).ToList();
        }


        public IslemOnayModel DepoyaEsyaEkle(DepoModel eklenecekEsya, int? kullaniciId)
        {
            IslemOnayModel onay = new IslemOnayModel();
            if (depoDAL.DepodaEsyaVarMi(eklenecekEsya.EsyaId, eklenecekEsya.Sehir.SehirId))
            {
                onay.TamamlandiMi = false;
                onay.HataMesajlari.Add("Eklemeye çalıştığınız eşya zaten deponuzda var. Güncelleme işlemlerini düzenleme sayfasında yapınız.");
            }
            else
            {
                DepoTablo esya = new DepoTablo()
                {
                    Adet = eklenecekEsya.Adet,
                    EsyaTablo_EsyaId = eklenecekEsya.EsyaId,
                    SehirTablo_SehirId = eklenecekEsya.Sehir.SehirId
                };
                if (depoDAL.KullaniciEklemeYapabilirMi(kullaniciId, eklenecekEsya.Sehir.SehirId))
                {
                    if (depoDAL.DepoyaEsyaEkle(esya))
                    {
                        onay.TamamlandiMi = true;
                    }
                    else
                    {
                        onay.TamamlandiMi = false;
                        onay.HataMesajlari.Add("Ekleme sırasında hata oluştu.");
                    }
                }
                else
                {
                    onay.TamamlandiMi = false;
                    onay.HataMesajlari.Add("Yalnızca kendi şehriniz için ekleme yapabilirsiniz.");
                }

            }

            return onay;
        }

        public DepoModel DepoEsyaGetir(int? esyaDepoId,int? kullaniciId)
        {
            var depoEsya = depoDAL.DepoEsyaGetir(esyaDepoId);
            if (depoEsya != null)
            {
                if (depoDAL.KullaniciEklemeYapabilirMi(kullaniciId, depoEsya.SehirTablo_SehirId))
                {
                    DepoModel rtrnModel = new DepoModel();
                    rtrnModel.EsyaId = depoEsya.EsyaTablo_EsyaId;
                    rtrnModel.Adet = depoEsya.Adet;
                    rtrnModel.DepoEsyaId = depoEsya.DepoEsyaId;
                    rtrnModel.DepoEsyaId = depoEsya.DepoEsyaId;
                    return rtrnModel;
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }

        public IslemOnayModel DepoEsyaGuncelle(DepoModel gunModel,int? kullaniciId)
        {
            IslemOnayModel onay = new IslemOnayModel();
            var depoEsya = depoDAL.DepoEsyaGetir(gunModel.DepoEsyaId);
            if (depoEsya != null)
            {
                if (depoDAL.KullaniciEklemeYapabilirMi(kullaniciId, depoEsya.SehirTablo_SehirId))
                {
                    depoEsya.SehirTablo_SehirId = gunModel.Sehir.SehirId;
                    depoEsya.Adet = gunModel.Adet;
                    depoEsya.DepoEsyaId = Convert.ToInt32(gunModel.DepoEsyaId);
                    depoEsya.EsyaTablo_EsyaId = gunModel.EsyaId;
                    if (!(depoDAL.AyniEsyaVarMi(depoEsya)))
                    {
                        if (depoDAL.DepoEsyaGuncelle(depoEsya))
                        {
                            onay.TamamlandiMi = true;
                        }
                        else
                        {
                            onay.TamamlandiMi = false;
                            onay.HataMesajlari.Add("Güncelleme işlemi sırasında bir hata oluştu.");
                        }
                    }
                    else
                    {
                        onay.TamamlandiMi = false;
                        onay.HataMesajlari.Add("Aynı isme sahip bir eşya zaten var.");
                    }
                }
                else
                {
                    onay.TamamlandiMi = false;
                    onay.HataMesajlari.Add("Sadece kendi deponuzda değişiklik yapabilirsiniz.");
                }
            }
            else
            {
                onay.TamamlandiMi = false;
                onay.HataMesajlari.Add("Düzenlenmek istenen eşya bulunamadı.");
            }

            return onay;
        }
    }
}
