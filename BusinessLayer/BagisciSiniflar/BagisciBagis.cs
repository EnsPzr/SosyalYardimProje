using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer.Models.OrtakModeller;
using BusinessLayer.Models.TeslimAlinacakBagis;
using DataLayer;

namespace BusinessLayer.BagisciSiniflar
{
    public class BagisciBagis
    {
        private DataLayer.BagisciSiniflar.BagisciBagis bagisDAL = new DataLayer.BagisciSiniflar.BagisciBagis();
        private KullaniciYonetimi kullaniciBAL = new KullaniciYonetimi();

        public List<TeslimAlinacakBagisModel> TumBagislariGetir(int? kullaniciId)
        {
            var bagislar = bagisDAL.TumBagislariGetir(kullaniciId);
            List<TeslimAlinacakBagisModel> teslimBagis = new List<TeslimAlinacakBagisModel>();
            for (int i = 0; i < bagislar.Count; i++)
            {
                var eklenecek = new TeslimAlinacakBagisModel();
                eklenecek.BagisId = bagislar[i].BagisId;
                eklenecek.EklenmeTarihiStr = bagislar[i].EklenmeTarihi != null ? Convert.ToDateTime(bagislar[i].EklenmeTarihi).ToShortDateString() : "";
                eklenecek.OnaylandiMiStr = bagislar[i].OnaylandiMi != null
                    ? bagislar[i].OnaylandiMi == true ? "Evet" : "Hayır"
                    : "Hayır";
                eklenecek.TeslimAlindiMi = bagislar[i].TeslimAlindiMi != null
                    ? bagislar[i].TeslimAlindiMi == true ? "Evet" : "Hayır"
                    : "Hayır";
                eklenecek.TahminiTeslimAlmaStr = bagislar[i].TahminiTeslimAlmaTarihi != null ? Convert.ToDateTime(bagislar[i].TahminiTeslimAlmaTarihi).ToShortDateString() : "";
                teslimBagis.Add(eklenecek);
            }

            return teslimBagis;
        }

        public List<TeslimAlinacakBagisModel> FiltreliBagislariGetir(int? kullaniciId, string tarih)
        {
            var bagislar = bagisDAL.FiltreliBagislariGetir(kullaniciId, tarih);
            List<TeslimAlinacakBagisModel> teslimBagis = new List<TeslimAlinacakBagisModel>();
            for (int i = 0; i < bagislar.Count; i++)
            {
                var eklenecek = new TeslimAlinacakBagisModel();
                eklenecek.BagisId = bagislar[i].BagisId;
                eklenecek.EklenmeTarihiStr = bagislar[i].EklenmeTarihi != null ? Convert.ToDateTime(bagislar[i].EklenmeTarihi).ToShortDateString() : "";
                eklenecek.OnaylandiMiStr = bagislar[i].OnaylandiMi != null
                    ? bagislar[i].OnaylandiMi == true ? "Evet" : "Hayır"
                    : "Hayır";
                eklenecek.TeslimAlindiMi = bagislar[i].TeslimAlindiMi != null
                    ? bagislar[i].TeslimAlindiMi == true ? "Evet" : "Hayır"
                    : "Hayır";
                eklenecek.TahminiTeslimAlmaStr = bagislar[i].TahminiTeslimAlmaTarihi != null ? Convert.ToDateTime(bagislar[i].TahminiTeslimAlmaTarihi).ToShortDateString() : "";
                teslimBagis.Add(eklenecek);
            }

            return teslimBagis;
        }

        public bool BagisKaydet(TeslimAlinacakBagisModel model,int kullaniciId)
        {
            int sayac = 0;
            BagisTablo bagisTablo = new BagisTablo();
            bagisTablo.KullaniciBilgileriTablo_KullaniciId = kullaniciId;
            bagisTablo.EklenmeTarihi = DateTime.Now;
            bagisTablo.TeslimAlindiMi = false;
            bagisTablo.EklenmeSaati = DateTime.Parse(DateTime.Now.ToString()).TimeOfDay;
            int? bagisId = bagisDAL.YeniBagisKaydet(bagisTablo);
            var detaylar = model.esyaModel;
            for (int i = 0; i < detaylar.Count; i++)
            {
                var eklenecekBagisDetay=new BagisDetayTablo();
                eklenecekBagisDetay.Adet = detaylar[i].Adet;
                eklenecekBagisDetay.BagisTablo_BagisId = bagisId;
                eklenecekBagisDetay.EsyaTablo_EsyaId = detaylar[i].EsyaId;
                int? bagisDetayId = bagisDAL.bagisDetayKaydeT(eklenecekBagisDetay);
                var resimler = detaylar[i].resimModel;
                if (resimler[0].ResimYol != null)
                {
                    var eklenecekresim=new BagisDetayResimTablo();
                    eklenecekresim.BagisDetayTablo_BagisDetayId = bagisDetayId;
                    eklenecekresim.BagisResimUrl = resimler[0].ResimYol;
                    if (bagisDAL.bagisResimKaydet(eklenecekresim))
                    {
                        sayac++;
                    }
                }
                if (resimler[0].ResimYol2 != null)
                {
                    var eklenecekresim = new BagisDetayResimTablo();
                    eklenecekresim.BagisDetayTablo_BagisDetayId = bagisDetayId;
                    eklenecekresim.BagisResimUrl = resimler[0].ResimYol2;
                    if (bagisDAL.bagisResimKaydet(eklenecekresim))
                    {
                        sayac++;
                    }
                }
                if (resimler[0].ResimYol3 != null)
                {
                    var eklenecekresim = new BagisDetayResimTablo();
                    eklenecekresim.BagisDetayTablo_BagisDetayId = bagisDetayId;
                    eklenecekresim.BagisResimUrl = resimler[0].ResimYol3;
                    if (bagisDAL.bagisResimKaydet(eklenecekresim))
                    {
                        sayac++;
                    }
                }
            }

            return true;
        }

        public IslemOnayModel BagisSil(int? kullaniciId, int? bagisId)
        {
            IslemOnayModel onay = new IslemOnayModel();
            if (bagisDAL.KullaniciIslemYapabilirMi(kullaniciId, bagisId))
            {
                if (bagisDAL.BagisOnaylandiMi(bagisId, kullaniciId)==false)
                {
                    if (bagisDAL.BagisSil(bagisId))
                    {
                        onay.TamamlandiMi = true;
                    }
                    else
                    {
                        onay.TamamlandiMi = false;
                        onay.HataMesajlari.Add("Bağış silme işleminde bilinmeyen bir hata meydana geldi.");
                    }
                }
                else
                {
                    onay.TamamlandiMi = false;
                    onay.HataMesajlari.Add("Bağış onaylandığından dolayı silme işlemi yapılamıyor.");
                }
            }
            else
            {
                onay.TamamlandiMi = false;
                onay.HataMesajlari.Add("Sadece kendi yaptığınız bağış için işlem yapabilirsiniz.");
            }

            return onay;
        }
    }
}
