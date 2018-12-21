using System;
using System.Collections.Generic;
using System.Data.Entity.Core;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer.Models.TeslimAlinacakBagis;
using DataLayer;

namespace BusinessLayer.Siniflar
{
    public class TeslimAlinacakBagis
    {
        private DataLayer.Siniflar.TeslimAlinacakBagis tesDAL = new DataLayer.Siniflar.TeslimAlinacakBagis();
        public List<TeslimAlinacakBagisModel> TumBagislariGetir(int? kullaniciId)
        {
            var bagislar = tesDAL.TumBagislariGetir(kullaniciId);
            List<TeslimAlinacakBagisModel> don = new List<TeslimAlinacakBagisModel>();
            for (int i = 0; i < bagislar.Count; i++)
            {
                var eklenecek = new TeslimAlinacakBagisModel();
                eklenecek.BagisId = bagislar[i].BagisId;
                eklenecek.BagisciAdiSoyadi = bagislar[i].KullaniciBilgileriTablo.KullaniciAdi + " " +
                                             bagislar[i].KullaniciBilgileriTablo.KullaniciSoyadi;
                eklenecek.BagisciTelNo = bagislar[i].KullaniciBilgileriTablo.KullaniciTelefonNumarasi;
                eklenecek.BagisciAdres = bagislar[i].KullaniciBilgileriTablo.KullaniciAdres;
                eklenecek.EklenmeTarihi = bagislar[i].EklenmeTarihi;
                if (bagislar[i].EklenmeTarihi != null)
                {
                    eklenecek.EklenmeTarihiStr = bagislar[i].EklenmeTarihi.Value.ToShortDateString();
                }
                if (bagislar[i].OnaylandiMi != null)
                {
                    if (bagislar[i].OnaylandiMi == true)
                    {
                        eklenecek.OnaylandiMiStr = "Evet";
                    }
                    else
                    {
                        eklenecek.OnaylandiMiStr = "Hayır";
                    }
                }
                else
                {
                    eklenecek.OnaylandiMiStr = "Hayır";
                }

                if (bagislar[i].TeslimAlindiMi != null)
                {
                    if (bagislar[i].TeslimAlindiMi == true)
                    {
                        eklenecek.TeslimAlindiMi = "Evet";
                    }
                    else
                    {
                        eklenecek.TeslimAlindiMi = "Hayır";
                    }
                }
                else
                {
                    eklenecek.TeslimAlindiMi = "Hayır";
                }
                don.Add(eklenecek);
            }

            return don;
        }

        public List<TeslimAlinacakBagisModel> FiltreliBagislariGetir(int? kullaniciId, int? sehirId, String aranan, String tarih)
        {
            var bagislar = tesDAL.FiltreliBagislariGetir(kullaniciId, sehirId, aranan, tarih);
            List<TeslimAlinacakBagisModel> don = new List<TeslimAlinacakBagisModel>();
            if (bagislar.Count == 0)
            {
                return don = new List<TeslimAlinacakBagisModel>();
            }
            else
            {
                for (int i = 0; i < bagislar.Count; i++)
                {
                    var eklenecek = new TeslimAlinacakBagisModel();
                    eklenecek.BagisId = bagislar[i].BagisId;
                    eklenecek.BagisciAdiSoyadi = bagislar[i].KullaniciBilgileriTablo.KullaniciAdi + " " +
                                                 bagislar[i].KullaniciBilgileriTablo.KullaniciSoyadi;
                    eklenecek.BagisciTelNo = bagislar[i].KullaniciBilgileriTablo.KullaniciTelefonNumarasi;
                    eklenecek.BagisciAdres = bagislar[i].KullaniciBilgileriTablo.KullaniciAdres;
                    eklenecek.EklenmeTarihi = bagislar[i].EklenmeTarihi;
                    if (bagislar[i].EklenmeTarihi != null)
                    {
                        eklenecek.EklenmeTarihiStr = bagislar[i].EklenmeTarihi.Value.ToShortDateString();
                    }
                    if (bagislar[i].OnaylandiMi != null)
                    {
                        if (bagislar[i].OnaylandiMi == true)
                        {
                            eklenecek.OnaylandiMiStr = "Evet";
                        }
                        else
                        {
                            eklenecek.OnaylandiMiStr = "Hayır";
                        }
                    }
                    else
                    {
                        eklenecek.OnaylandiMiStr = "Hayır";
                    }

                    if (bagislar[i].TeslimAlindiMi != null)
                    {
                        if (bagislar[i].TeslimAlindiMi == true)
                        {
                            eklenecek.TeslimAlindiMi = "Evet";
                        }
                        else
                        {
                            eklenecek.TeslimAlindiMi = "Hayır";
                        }
                    }
                    else
                    {
                        eklenecek.TeslimAlindiMi = "Hayır";
                    }
                    don.Add(eklenecek);
                }
            }

            return don;
        }

        public TeslimAlinacakBagisModel Detay(int? id)
        {
            var bagis = tesDAL.Detay(id);
            if (bagis != null)
            {
                TeslimAlinacakBagisModel model = new TeslimAlinacakBagisModel();
                model.BagisId = bagis.BagisId;
                model.BagisciAdres = bagis.KullaniciBilgileriTablo.KullaniciAdres;
                model.BagisciTelNo = bagis.KullaniciBilgileriTablo.KullaniciTelefonNumarasi;
                if (bagis.EklenmeTarihi != null)
                {
                    model.EklenmeTarihiStr = bagis.EklenmeTarihi.Value.ToShortDateString();
                }

                model.OnaylandiMiStr =
                    bagis.OnaylandiMi != null ? bagis.OnaylandiMi == true ? "Evet" : "Hayır" : "Hayır";
                model.TeslimAlindiMi = bagis.TeslimAlindiMi != null ? bagis.TeslimAlindiMi == true ? "Evet" : "Hayır" : "Hayır";
                model.BagisciAdiSoyadi = bagis.KullaniciBilgileriTablo.KullaniciAdi + " " +
                                         bagis.KullaniciBilgileriTablo.KullaniciSoyadi;

                var bagisEsya = tesDAL.BagisDetay(id);
                for (int i = 0; i < bagisEsya.Count; i++)
                {
                    var eklenecekBagisDetay=new TeslimAlinacakBagisEsyaModel();
                    eklenecekBagisDetay.EsyaAdi = bagisEsya[i].EsyaTablo.EsyaAdi;
                    eklenecekBagisDetay.Adet = bagisEsya[i].Adet;
                    eklenecekBagisDetay.AlinacakMi=bagisEsya[i].AlinacakMi != null ? bagisEsya[i].AlinacakMi == true ? true : false : false;
                    eklenecekBagisDetay.AlindiMi= bagisEsya[i].AlindiMi != null ? bagisEsya[i].AlindiMi == true ? true : false : false;
                    eklenecekBagisDetay.BagisDetayId = bagisEsya[i].BagisDetayId;
                    var resTablo = tesDAL.BagisResim(eklenecekBagisDetay.BagisDetayId);
                    for (int j = 0; j < resTablo.Count; j++)
                    {
                        var eklenecekResim=new TeslimAlinacakBagisResimModel();
                        eklenecekResim.ResimId = resTablo[j].BagisResimId;
                        eklenecekResim.ResimYol = resTablo[j].BagisResimUrl;
                        eklenecekBagisDetay.resimModel.Add(eklenecekResim);
                    }
                    model.esyaModel.Add(eklenecekBagisDetay);
                }

                return model;
            }
            else
            {
                return null;
            }
        }

        public bool KullaniciBagisDetayiGorebilirMi(int? kullaniciId, int? bagisId)
        {
            return tesDAL.KullaniciBagisDetayiGorebilirMi(kullaniciId, bagisId);
        }
    }
}
