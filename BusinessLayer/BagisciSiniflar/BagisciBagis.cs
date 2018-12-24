using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer.Models.TeslimAlinacakBagis;

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
                eklenecek.EklenmeTarihiStr = bagislar[i].EklenmeTarihi != null ? bagislar[i].EklenmeTarihi.ToString() : "";
                eklenecek.OnaylandiMiStr = bagislar[i].OnaylandiMi != null
                    ? bagislar[i].OnaylandiMi == true ? "Evet" : "Hayır"
                    : "Hayır";
                eklenecek.TeslimAlindiMi = bagislar[i].TeslimAlindiMi != null
                    ? bagislar[i].TeslimAlindiMi == true ? "Evet" : "Hayır"
                    : "Hayır";
                teslimBagis.Add(eklenecek);
            }

            return teslimBagis;
        }
    }
}
