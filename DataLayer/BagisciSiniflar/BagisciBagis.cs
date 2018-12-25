using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using DataLayer.Siniflar;
using System.IO;

namespace DataLayer.BagisciSiniflar
{
    public class BagisciBagis
    {
        private KullaniciYonetimi kullaniciDAL = new KullaniciYonetimi();
        private SosyalYardimDB db = new SosyalYardimDB();
        private Kullanici kullanici2DAL = new Kullanici();

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

        public bool KullaniciIslemYapabilirMi(int? kullaniciId, int? bagisId)
        {
            if (kullaniciDAL.KullaniciMerkezdeMi(kullaniciId))
            {
                return true;
            }
            else
            {
                var kullanici = kullanici2DAL.KullaniciGetir(kullaniciId);
                if (kullanici != null)
                {
                    if (kullanici.BagisciMi==true)
                    {
                        var bagis = db.BagisTablo.Include(p => p.KullaniciBilgileriTablo)
                            .FirstOrDefault(p => p.BagisId == bagisId);
                        if (bagis != null)
                        {
                            if (bagis.KullaniciBilgileriTablo_KullaniciId == kullaniciId)
                            {
                                return true;
                            }
                            else
                            {
                                return false;
                            }
                        }
                        else
                        {
                            return false;
                        }
                    }
                    else
                    {
                        int? kulSehir = kullaniciDAL.KullaniciSehir(kullaniciId);
                        var bagis = db.BagisTablo.Include(p => p.KullaniciBilgileriTablo)
                            .FirstOrDefault(p => p.BagisId == bagisId);
                        if (bagis != null)
                        {
                            if (bagis.KullaniciBilgileriTablo.SehirTablo_SehirId == kulSehir)
                            {
                                return true;
                            }
                            else
                            {
                                return false;
                            }
                        }
                        else
                        {
                            return false;
                        }
                    }
                }
                else
                {
                    return false;
                }
            }
        }

        public bool BagisSil(int? bagisId)
        {
            var bagis = db.BagisTablo.FirstOrDefault(p => p.BagisId == bagisId);
            if (bagis != null)
            {
                var bagisDetaylar = db.BagisDetayTablo.Where(p => p.BagisTablo_BagisId == bagis.BagisId).ToList();
                for (int i = 0; i < bagisDetaylar.Count; i++)
                {
                    int? bagisDetayId = Convert.ToInt32(bagisDetaylar[i].BagisDetayId);
                    var bagisResimler = db.BagisDetayResimTablo
                        .Where(p => p.BagisDetayTablo_BagisDetayId == bagisDetayId).ToList();
                    for (int j = 0; j < bagisResimler.Count; j++)
                    {
                        db.BagisDetayResimTablo.Remove(bagisResimler[j]);
                    }

                    db.SaveChanges();
                    for (int j = 0; j < bagisResimler.Count; j++)
                    {
                        File.Delete("~/" + bagisResimler[j].BagisResimUrl);
                    }
                    db.BagisDetayTablo.Remove(bagisDetaylar[i]);
                    db.SaveChanges();
                }

                db.BagisTablo.Remove(bagis);
                db.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool BagisOnaylandiMi(int? bagisId,int? kullaniciId)
        {
            if (kullaniciDAL.KullaniciMerkezdeMi(kullaniciId))
            {
                return false;
            }
            else
            {
                var bagis = db.BagisTablo.FirstOrDefault(p => p.BagisId == bagisId);
                if (bagis != null)
                {
                    if (bagis.OnaylandiMi == true)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
        }
    }
}
