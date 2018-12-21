using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer.Models.TeslimAlinacakBagis;

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

        public List<TeslimAlinacakBagisModel> FiltreliBagislariGetir(int? kullaniciId, int sehirId, String aranan, String tarih)
        {
            var bagislar = tesDAL.FiltreliBagislariGetir(kullaniciId, sehirId, aranan, tarih);
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
    }
}
