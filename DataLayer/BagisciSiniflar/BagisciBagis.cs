using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
namespace DataLayer.BagisciSiniflar
{
    public class BagisciBagis
    {
        private KullaniciYonetimi kullaniciDAL = new KullaniciYonetimi();
        private SosyalYardimDB db = new SosyalYardimDB();

        public List<BagisTablo> TumBagislariGetir(int? kullaniciId)
        {
            return db.BagisTablo.Where(p => p.KullaniciBilgileriTablo_KullaniciId == kullaniciId).ToList();
        }

        public List<BagisTablo> FiltreliBagislariGetir(int? kullaniciId, string tarih)
        {
            var sorgu = db.BagisTablo.Where(p => p.KullaniciBilgileriTablo_KullaniciId == kullaniciId).AsQueryable();

            if (tarih != null)
            {
                DateTime trh = Convert.ToDateTime(tarih);
                sorgu = sorgu.Where(p => p.TahminiTeslimAlmaTarihi == trh || p.EklenmeTarihi == trh);
            }

            return sorgu.ToList();
        }

        public int? YeniBagisKaydet(BagisTablo bagis)
        {
            db.BagisTablo.Add(bagis);
            db.SaveChanges();
            var bagisTablo = db.BagisTablo.FirstOrDefault(p => p.EklenmeSaati == bagis.EklenmeSaati
                                                               && p.KullaniciBilgileriTablo_KullaniciId == bagis.KullaniciBilgileriTablo_KullaniciId);
            return bagisTablo.BagisId;
            //if (bagisTablo != null)
            //{
            //    for (int i = 0; i < bagisDetay.Count; i++)
            //    {
            //        bagisDetay[i].BagisTablo_BagisId = bagisTablo.BagisId;
            //        db.BagisDetayTablo.Add(bagisDetay[i]);
            //        db.SaveChanges();
            //        int? esyaId = bagisDetay[i].EsyaTablo_EsyaId;
            //        int? bagisId = bagisTablo.BagisId;
            //        var eklenenBagis = db.BagisDetayTablo.FirstOrDefault(p => p.EsyaTablo_EsyaId == esyaId
            //                                                                  && p.BagisTablo_BagisId == bagisId);
            //        if (eklenenBagis != null)
            //        {
            //            var resimTablo = resimler[i];
            //            BagisDetayResimTablo eklenecekResim = new BagisDetayResimTablo();
            //            eklenecekResim.BagisResimUrl = resimTablo.BagisResimUrl;
            //            eklenecekResim.BagisDetayTablo_BagisDetayId = resimTablo.;
            //            db.BagisDetayResimTablo.Add(eklenecekResim);
            //            db.SaveChanges();
            //        }
            //    }

            //    return true;
            //}
            //else
            //{
            //    return false;
            //}

        }

        public int? bagisDetayKaydeT(BagisDetayTablo bagisDetay)
        {
            BagisDetayTablo detayTablo = new BagisDetayTablo();
            detayTablo.BagisTablo_BagisId = bagisDetay.BagisTablo_BagisId;
            detayTablo.Adet = bagisDetay.Adet;
            detayTablo.EsyaTablo_EsyaId = bagisDetay.EsyaTablo_EsyaId;
            db.BagisDetayTablo.Add(detayTablo);
            db.SaveChanges();
            var bagisBilgisi = db.BagisDetayTablo.FirstOrDefault(p => p.Adet == detayTablo.Adet
                                                             && p.EsyaTablo_EsyaId == detayTablo.EsyaTablo_EsyaId
                                                             && p.BagisTablo_BagisId == detayTablo.BagisTablo_BagisId);
            return bagisBilgisi.BagisDetayId;
        }

        public bool bagisResimKaydet(BagisDetayResimTablo resim)
        {
            BagisDetayResimTablo resimTablo = new BagisDetayResimTablo();
            resimTablo.BagisResimUrl = resim.BagisResimUrl;
            resimTablo.BagisDetayTablo_BagisDetayId = resim.BagisDetayTablo_BagisDetayId;
            db.BagisDetayResimTablo.Add(resimTablo);
            db.SaveChanges();
            return true;
        }
    }
}
