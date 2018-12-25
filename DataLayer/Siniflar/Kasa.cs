using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace DataLayer.Siniflar
{
    public class Kasa
    {
        private Kullanici Kullanici2DAL = new Kullanici();
        private KullaniciYonetimi KullaniciDAL = new KullaniciYonetimi();
        private SosyalYardimDB db = new SosyalYardimDB();
        public List<KasaTablo> TumKasaGetir(int? kullaniciId)
        {
            if (KullaniciDAL.KullaniciMerkezdeMi(kullaniciId))
            {
                return db.KasaTablo.Include(p => p.KullaniciBilgileriTablo).Include(p => p.SehirTablo).OrderByDescending(p => p.Tarih).ToList();
            }
            else
            {
                var kullanici = Kullanici2DAL.KullaniciGetir(kullaniciId);
                if (kullanici.BagisciMi == true)
                {
                    return db.KasaTablo.Include(p => p.KullaniciBilgileriTablo).Where(p => p.KullaniciBilgleriTablo_KullaniciId == kullaniciId).Include(p => p.SehirTablo)
                        .OrderByDescending(p => p.Tarih).ToList();
                }
                else
                {
                    int? kullaniciSehirId = KullaniciDAL.KullaniciSehir(kullaniciId);
                    return db.KasaTablo.Include(p => p.KullaniciBilgileriTablo).Include(p => p.SehirTablo).
                        Where(p => p.SehirTablo_SehirId == kullaniciSehirId).OrderByDescending(p => p.Tarih).ToList();
                }
            }
        }

        public List<KasaTablo> FiltreliKasaGetir(int? kullaniciId, string aranan, string tarih, int? sehirId, int? gelirGider)
        {
            if (KullaniciDAL.KullaniciMerkezdeMi(kullaniciId))
            {
                var sorgu = db.KasaTablo.Include(p => p.KullaniciBilgileriTablo).Include(p => p.SehirTablo)
                    .AsQueryable();
                if (aranan != null)
                {
                    sorgu = sorgu.Where(p => p.KullaniciBilgileriTablo.KullaniciAdi.Contains(aranan)
                                             || p.KullaniciBilgileriTablo.KullaniciSoyadi.Contains(aranan)
                                             || p.KullaniciBilgileriTablo.KullaniciEPosta.Contains(aranan)
                                             || p.Miktar.ToString().Contains(aranan)
                                             || p.SehirTablo.SehirAdi.Contains(aranan)
                                             || p.Aciklama.Contains(aranan)
                                             || p.Tarih.ToString().Contains(aranan));
                }

                if (tarih != null)
                {
                    DateTime Tarih = Convert.ToDateTime(tarih);
                    sorgu = sorgu.Where(p => p.Tarih == Tarih);
                }

                if (sehirId != null)
                {
                    sorgu = sorgu.Where(p => p.SehirTablo_SehirId == sehirId);
                }

                if (gelirGider != null)
                {
                    if (!(gelirGider == 0))
                    {
                        bool GelirGider = gelirGider == 1 ? true : false;
                        sorgu = sorgu.Where(p => p.GelirGider == GelirGider);
                    }
                }
                return sorgu.OrderByDescending(p => p.Tarih).ToList();
            }
            else
            {
                var kullanici = Kullanici2DAL.KullaniciGetir(kullaniciId);
                if (kullanici.BagisciMi == true)
                {
                    var sorgu = db.KasaTablo.Include(p => p.KullaniciBilgileriTablo).Include(p => p.SehirTablo)
                        .Where(p => p.KullaniciBilgleriTablo_KullaniciId == kullaniciId).AsQueryable();
                    if (aranan != null)
                    {
                        sorgu = sorgu.Where(p => p.KullaniciBilgileriTablo.KullaniciAdi.Contains(aranan)
                                                 || p.KullaniciBilgileriTablo.KullaniciSoyadi.Contains(aranan)
                                                 || p.KullaniciBilgileriTablo.KullaniciEPosta.Contains(aranan)
                                                 || p.Miktar.ToString().Contains(aranan)
                                                 || p.SehirTablo.SehirAdi.Contains(aranan)
                                                 || p.Aciklama.Contains(aranan)
                                                 || p.Tarih.ToString().Contains(aranan));
                    }

                    if (tarih != null)
                    {
                        DateTime Tarih = Convert.ToDateTime(tarih);
                        sorgu = sorgu.Where(p => p.Tarih == Tarih);
                    }
                    if (gelirGider != null)
                    {
                        if (!(gelirGider == 0))
                        {
                            bool GelirGider = gelirGider == 1 ? true : false;
                            sorgu = sorgu.Where(p => p.GelirGider == GelirGider);
                        }
                    }
                    return sorgu.OrderByDescending(p => p.Tarih).ToList();
                }
                else
                {
                    int? kullaniciSehirId = KullaniciDAL.KullaniciSehir(kullaniciId);
                    var sorgu = db.KasaTablo.Include(p => p.KullaniciBilgileriTablo).Include(p => p.SehirTablo)
                        .Where(p => p.SehirTablo_SehirId == kullaniciSehirId).AsQueryable();
                    if (aranan != null)
                    {
                        sorgu = sorgu.Where(p => p.KullaniciBilgileriTablo.KullaniciAdi.Contains(aranan)
                                                 || p.KullaniciBilgileriTablo.KullaniciSoyadi.Contains(aranan)
                                                 || p.KullaniciBilgileriTablo.KullaniciEPosta.Contains(aranan)
                                                 || p.Miktar.ToString().Contains(aranan)
                                                 || p.SehirTablo.SehirAdi.Contains(aranan)
                                                 || p.Aciklama.Contains(aranan)
                                                 || p.Tarih.ToString().Contains(aranan));
                    }

                    if (tarih != null)
                    {
                        DateTime Tarih = Convert.ToDateTime(tarih);
                        sorgu = sorgu.Where(p => p.Tarih == Tarih);
                    }
                    if (gelirGider != null)
                    {
                        if (!(gelirGider == 0))
                        {
                            bool GelirGider = gelirGider == 1 ? true : false;
                            sorgu = sorgu.Where(p => p.GelirGider == GelirGider);
                        }
                    }
                    return sorgu.OrderByDescending(p => p.Tarih).ToList();
                }
            }
        }

        public bool KullaniciIslemYapabilirMi(int? kullaniciId, int? sehirId)
        {
            if (KullaniciDAL.KullaniciMerkezdeMi(kullaniciId))
            {
                return true;
            }
            else
            {
                int? kullaniciSehirId = KullaniciDAL.KullaniciSehir(kullaniciId);
                if (kullaniciSehirId == sehirId)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public bool KasaIslemKaydet(KasaTablo model)
        {
            db.KasaTablo.Add(model);
            if (db.SaveChanges() > 0)
            {
                return true;
            }

            return false;
        }

        public KasaTablo KasaGetir(int? kasaId)
        {
            return db.KasaTablo.Include(p => p.SehirTablo).Include(p => p.KullaniciBilgileriTablo)
                .FirstOrDefault(p => p.KasaId == kasaId);
        }

        public bool KasaIslemGuncelle(KasaTablo model)
        {
            var gunKasa = db.KasaTablo.FirstOrDefault(p => p.KasaId == model.KasaId);
            if (gunKasa != null)
            {
                gunKasa.Aciklama = model.Aciklama;
                gunKasa.GelirGider = model.GelirGider;
                gunKasa.Miktar = model.Miktar;
                gunKasa.SehirTablo_SehirId = model.SehirTablo_SehirId;
                gunKasa.Tarih = model.Tarih;
                db.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool KasaSil(int? kasaId)
        {
            var bilKasa = db.KasaTablo.FirstOrDefault(p => p.KasaId == kasaId);
            if (bilKasa != null)
            {
                db.KasaTablo.Remove(bilKasa);
                db.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool KrediKartiEkleme(KullaniciBilgileriTablo kullanici, KasaTablo kasa)
        {
            if (Kullanici2DAL.KullaniciVarMi(kullanici.KullaniciEPosta))
            {
                int? kullaniciId = Kullanici2DAL.KullaniciVarMiInt(kullanici.KullaniciEPosta);
                kasa.KullaniciBilgleriTablo_KullaniciId = kullaniciId;
                db.KasaTablo.Add(kasa);
                db.SaveChanges();
                return true;
            }
            else
            {
                db.KullaniciBilgileriTablo.Add(kullanici);
                db.SaveChanges();
                int? kullaniciId = Kullanici2DAL.KullaniciVarMiInt(kullanici.KullaniciEPosta);
                kasa.KullaniciBilgleriTablo_KullaniciId = kullaniciId;
                db.KasaTablo.Add(kasa);
                db.SaveChanges();
                return true;
            }
        }

        public bool DisardanKartBagis(KullaniciBilgileriTablo kullanici, KasaTablo kasa)
        {
            var kullaniciVarMi = db.KullaniciBilgileriTablo.FirstOrDefault(p => p.KullaniciEPosta == kullanici.KullaniciEPosta);
            if (kullaniciVarMi != null)
            {
                kasa.KullaniciBilgleriTablo_KullaniciId = kullaniciVarMi.KullaniciId;
                db.KasaTablo.Add(kasa);
                db.SaveChanges();
                return true;
            }
            else
            {
                db.KullaniciBilgileriTablo.Add(kullanici);
                db.SaveChanges();
                var eklenenKullanici = db.KullaniciBilgileriTablo.FirstOrDefault(p => p.KullaniciEPosta == kullanici.KullaniciEPosta);
                if (eklenenKullanici != null)
                {
                    kasa.KullaniciBilgleriTablo_KullaniciId = eklenenKullanici.KullaniciId;
                    db.KasaTablo.Add(kasa);
                    db.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
    }
}
