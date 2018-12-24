using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Data.Entity.ModelConfiguration.Configuration;
using System.Runtime.InteropServices.WindowsRuntime;

namespace DataLayer.Siniflar
{
    public class IhtiyacSahibi
    {
        private KullaniciYonetimi kullaniciDAL = new KullaniciYonetimi();
        private SosyalYardimDB db = new SosyalYardimDB();
        public List<IhtiyacSahibiTablo> TumIhtiyacSahipleriniGetir(int? KullaniciId)
        {
            if (kullaniciDAL.KullaniciBul(KullaniciId).KullaniciMerkezdeMi == true)
            {
                return db.IhtiyacSahibiTablo.Include(p => p.SehirTablo).ToList();
            }
            else
            {
                if (kullaniciDAL.KullaniciBul(KullaniciId).BagisciMi == true)
                {
                    var ihtiyacSahipleri =
                        db.IhtiyacSahibiVeKullaniciTablo.Where(
                            p => p.KullaniciBilgileriTablo_KullaniciId == KullaniciId).ToList();
                    List<IhtiyacSahibiTablo> listIhtiyacSahipleri = new List<IhtiyacSahibiTablo>();
                    for (int i = 0; i < ihtiyacSahipleri.Count(); i++)
                    {
                        var ihtiyacSahibiId = ihtiyacSahipleri[i].IhtiyacSahibiTablo_IhtiyacSahibiId;
                        var ihtiyacSahibi = db.IhtiyacSahibiTablo.Include(p => p.SehirTablo)
                            .FirstOrDefault(p => p.IhtiyacSahibiId == ihtiyacSahibiId);
                        listIhtiyacSahipleri.Add(ihtiyacSahibi);
                    }

                    return listIhtiyacSahipleri;
                }
                else
                {
                    int? sehirId = kullaniciDAL.KullaniciBul(KullaniciId).SehirTablo_SehirId;
                    return db.IhtiyacSahibiTablo.Include(p => p.SehirTablo).Where(p =>
                        p.SehirTablo_SehirId == sehirId).ToList();
                }
            }
        }

        public List<IhtiyacSahibiTablo> FiltreliIhtiyacSahipleriListesiniGetir(String aranan, int? sehirId, int? kullaniciId)
        {
            var ihtiyacSahipleri = db.IhtiyacSahibiTablo.Include(p => p.SehirTablo).Where(p =>
                p.IhtiyacSahibiAdi.Contains(aranan) ||
                p.IhtiyacSahibiSoyadi.Contains(aranan) ||
                p.IhtiyacSahibiTelNo.Contains(aranan) ||
                p.IhtiyacSahibiAdres.Contains(aranan) ||
                p.SehirTablo.SehirAdi.Contains(aranan) ||
                p.IhtiyacSahibiAciklama.Contains(aranan)).AsQueryable();
            if (sehirId != null)
            {
                ihtiyacSahipleri = ihtiyacSahipleri.Where(p => p.SehirTablo_SehirId == sehirId);
            }

            if (kullaniciId != null)
            {
                var kullanici = kullaniciDAL.KullaniciBul(kullaniciId);
                if (kullanici.KullaniciMerkezdeMi == false)
                {
                    ihtiyacSahipleri = ihtiyacSahipleri.Where(p =>
                        p.SehirTablo_SehirId == kullanici.SehirTablo_SehirId);
                }
            }
            return ihtiyacSahipleri.ToList();
        }

        public bool IhtiyacSahibiKaydet(IhtiyacSahibiTablo yeniIhtiyacSahibi)
        {
            db.IhtiyacSahibiTablo.Add(yeniIhtiyacSahibi);
            if (db.SaveChanges() > 0)
            {
                IhtiyacSahibiKontrolTablo kontrolTablo= new IhtiyacSahibiKontrolTablo();
                kontrolTablo.IhtiyacSahibiTablo_IhtiyacSahibiId = db.IhtiyacSahibiTablo.FirstOrDefault(p =>
                    p.IhtiyacSahibiAdi == yeniIhtiyacSahibi.IhtiyacSahibiAdi
                    && p.IhtiyacSahibiSoyadi == yeniIhtiyacSahibi.IhtiyacSahibiSoyadi
                    && p.IhtiyacSahibiTelNo == yeniIhtiyacSahibi.IhtiyacSahibiTelNo).IhtiyacSahibiId;
                kontrolTablo.MuhtacMi = false;
                kontrolTablo.Tarih=DateTime.Now;
                kontrolTablo.TeslimTamamlandiMi = false;
                db.IhtiyacSahibiKontrolTablo.Add(kontrolTablo);
                db.SaveChanges();
                kontrolTablo.IhtiyacSahibiVerilecekMaddiTablo.Add(new IhtiyacSahibiVerilecekMaddiTablo()
                {
                    IhtiyacSahibiKontrolTablo_IhtiyacSahibiKontrolId=kontrolTablo.IhtiyacSahibiKontrolId,
                    VerilecekMaddiYardim=0
                });
                db.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        public IhtiyacSahibiTablo IhtiyacSahibiVarMi(string adi, string soyadi, string telNo)
        {
            var ihtiyacSahibi = db.IhtiyacSahibiTablo.Include(p => p.SehirTablo).FirstOrDefault(p =>
                  p.IhtiyacSahibiAdi.Contains(adi) && p.IhtiyacSahibiSoyadi.Contains(soyadi)
                                                   && p.IhtiyacSahibiTelNo == telNo);
            if (ihtiyacSahibi != null)
            {
                return ihtiyacSahibi;
            }
            else
            {
                return null;
            }
        }

        public IhtiyacSahibiTablo IhtiyacSahibiVarMi(int? id, string adi, string soyadi, string telNo)
        {
            var ihtiyacSahibi = db.IhtiyacSahibiTablo.Include(p => p.SehirTablo).FirstOrDefault(p =>
                (p.IhtiyacSahibiAdi.Contains(adi) && p.IhtiyacSahibiSoyadi.Contains(soyadi)
                                                  && p.IhtiyacSahibiTelNo == telNo) && p.IhtiyacSahibiId != id);
            if (ihtiyacSahibi != null)
            {
                return ihtiyacSahibi;
            }
            else
            {
                return null;
            }
        }

        public bool IhtiyacSahibiSil(int? ihtiyacSahibiId)
        {
            var ihtiyacSahibi = db.IhtiyacSahibiTablo.FirstOrDefault(p => p.IhtiyacSahibiId == ihtiyacSahibiId);
            db.IhtiyacSahibiTablo.Remove(ihtiyacSahibi);
            if (db.SaveChanges() > 0)
            {
                return true;
            }
            else
            {
                return false;

            }
        }

        public IhtiyacSahibiTablo IhtiyacSahibiGetir(int? ihtiyacSahibiId)
        {
            var ihtiyacSahibi = db.IhtiyacSahibiTablo.Include(p => p.SehirTablo)
                .FirstOrDefault(p => p.IhtiyacSahibiId == ihtiyacSahibiId);
            if (ihtiyacSahibi != null)
            {
                return ihtiyacSahibi;
            }
            else
            {
                return null;
            }
        }

        public bool IhtiyacSahibiGuncelle(IhtiyacSahibiTablo duzenlenmisIhtiyacSahibi)
        {
            var ihtiyacSahibi =
                db.IhtiyacSahibiTablo.FirstOrDefault(p =>
                    p.IhtiyacSahibiId == duzenlenmisIhtiyacSahibi.IhtiyacSahibiId);
            ihtiyacSahibi.IhtiyacSahibiAdi = duzenlenmisIhtiyacSahibi.IhtiyacSahibiAdi;
            ihtiyacSahibi.IhtiyacSahibiSoyadi = duzenlenmisIhtiyacSahibi.IhtiyacSahibiSoyadi;
            ihtiyacSahibi.IhtiyacSahibiTelNo = duzenlenmisIhtiyacSahibi.IhtiyacSahibiTelNo;
            ihtiyacSahibi.SehirTablo_SehirId = duzenlenmisIhtiyacSahibi.SehirTablo_SehirId;
            ihtiyacSahibi.IhtiyacSahibiAdres = duzenlenmisIhtiyacSahibi.IhtiyacSahibiAdres;
            ihtiyacSahibi.IhtiyacSahibiAciklama = duzenlenmisIhtiyacSahibi.IhtiyacSahibiAciklama;
            db.SaveChanges();
            return true;
        }

        public List<IhtiyacSahibiKontrolTablo> KontrolEdilecekIhtiyacSahipleriniGetir(int? kullaniciId)
        {
            var kullanici = kullaniciDAL.KullaniciBul(kullaniciId);
            if (kullanici.KullaniciMerkezdeMi == true)
            {
                return db.IhtiyacSahibiKontrolTablo.Include(p => p.IhtiyacSahibiTablo).OrderBy(p => p.Tarih).ToList();
            }
            else
            {
                int? sehirId = kullanici.SehirTablo_SehirId;
                return db.IhtiyacSahibiKontrolTablo.Include(p => p.IhtiyacSahibiTablo).OrderBy(p => p.Tarih)
                    .Where(p => p.IhtiyacSahibiTablo.SehirTablo_SehirId == sehirId).ToList();
            }
        }

        public List<IhtiyacSahibiKontrolTablo> KontrolEdilecekFiltreliIhtiyacSahipleriniGetir(int? kullaniciId, string aranan, int? sehirId, String tarih)
        {
            var kullanici = kullaniciDAL.KullaniciBul(kullaniciId);
            if (kullanici.KullaniciMerkezdeMi == true)
            {
                IQueryable<IhtiyacSahibiKontrolTablo> sorgu = db.IhtiyacSahibiKontrolTablo
                    .Include(p => p.IhtiyacSahibiTablo).OrderBy(p => p.Tarih);
                if (!(aranan.Equals("")))
                {
                    sorgu = sorgu.Where(p => p.IhtiyacSahibiTablo.IhtiyacSahibiAdi.Contains(aranan)
                                             || p.IhtiyacSahibiTablo.IhtiyacSahibiSoyadi.Contains(aranan)
                                             || p.IhtiyacSahibiTablo.IhtiyacSahibiTelNo.Contains(aranan)
                                             || p.IhtiyacSahibiTablo.IhtiyacSahibiAdres.Contains(aranan));
                }

                if (sehirId != null)
                {
                    sorgu = sorgu.Where(p => p.IhtiyacSahibiTablo.SehirTablo_SehirId == sehirId);
                }

                if (tarih != null)
                {
                    if (DonusurMu(tarih, 1))
                    {
                        DateTime? trh = Convert.ToDateTime(tarih);
                        sorgu = sorgu.Where(p => p.Tarih == trh || p.KontrolYapilmaTarihi == trh || p.TahminiTeslimTarihi == trh);
                    }
                }

                return sorgu.ToList();
            }
            else
            {
                int? kullaniciSehirId = kullanici.SehirTablo_SehirId;
                IQueryable<IhtiyacSahibiKontrolTablo> sorgu = db.IhtiyacSahibiKontrolTablo.Include(p => p.IhtiyacSahibiTablo).OrderBy(p => p.Tarih)
                    .Where(p => p.IhtiyacSahibiTablo.SehirTablo_SehirId == kullaniciSehirId);
                if (!(aranan.Equals("")))
                {
                    sorgu = sorgu.Where(p => p.IhtiyacSahibiTablo.IhtiyacSahibiAdi.Contains(aranan)
                                             || p.IhtiyacSahibiTablo.IhtiyacSahibiSoyadi.Contains(aranan)
                                             || p.IhtiyacSahibiTablo.IhtiyacSahibiTelNo.Contains(aranan)
                                             || p.IhtiyacSahibiTablo.IhtiyacSahibiAdres.Contains(aranan));
                }
                if (tarih != null)
                {
                    if (DonusurMu(tarih, 1))
                    {
                        DateTime? trh = Convert.ToDateTime(tarih);
                        sorgu = sorgu.Where(p => p.Tarih == trh || p.KontrolYapilmaTarihi == trh || p.TahminiTeslimTarihi == trh);
                    }
                }

                return sorgu.ToList();
            }
        }
        //
        public List<IhtiyacSahibiVerilecekEsyaTablo> IhtiyacSahibiVerilecekEsyaGetir(int? ihtiyacSahibiKontrolId)
        {
            return db.IhtiyacSahibiVerilecekEsyaTablo.Include(p => p.IhtiyacSahibiKontrolTablo).Include(p => p.EsyaTablo).Include(p => p.IhtiyacSahibiKontrolTablo.IhtiyacSahibiTablo)
                .Where(p => p.IhtiyacSahibiKontrolTablo_IhtiyacSahibiKontrolId == ihtiyacSahibiKontrolId).OrderBy(p => p.EsyaTablo.EsyaAdi).ToList();
        }
        //
        public IhtiyacSahibiVerilecekMaddiTablo IhtıyacSahibiVerilecekMaddiGetir(int? ihtiyacSahibiKontrolId)
        {
            return db.IhtiyacSahibiVerilecekMaddiTablo.Include(p => p.IhtiyacSahibiKontrolTablo)
                .Include(p => p.IhtiyacSahibiKontrolTablo.IhtiyacSahibiTablo).FirstOrDefault(p =>
                    p.IhtiyacSahibiKontrolTablo_IhtiyacSahibiKontrolId == ihtiyacSahibiKontrolId);
        }
        //
        public IhtiyacSahibiTablo ihtiyacSahibiGetir(int? ihtiyacSahibiKontrolId)
        {
            var ihtiyacSahibiKontrol =
                db.IhtiyacSahibiKontrolTablo.Include(p => p.IhtiyacSahibiTablo).FirstOrDefault(p => p.IhtiyacSahibiKontrolId == ihtiyacSahibiKontrolId);
            return ihtiyacSahibiKontrol.IhtiyacSahibiTablo;
        }

        public IhtiyacSahibiKontrolTablo IhtiyacSahibiKontrolBilgileri(int? ihtiyacSahibiKontrolId)
        {
            return db.IhtiyacSahibiKontrolTablo.Include(p => p.IhtiyacSahibiVerilecekEsyaTablo)
                .Include(p => p.IhtiyacSahibiTablo).Include(p => p.IhtiyacSahibiTablo.SehirTablo)
                .FirstOrDefault(p => p.IhtiyacSahibiKontrolId == ihtiyacSahibiKontrolId);
        }

        public IhtiyacSahibiVerilecekMaddiTablo VerilecekMaddiTutariGetir(int? ihtiyacSahibiKontrolId)
        {
            return db.IhtiyacSahibiVerilecekMaddiTablo.FirstOrDefault(p =>
                p.IhtiyacSahibiKontrolTablo_IhtiyacSahibiKontrolId == ihtiyacSahibiKontrolId);
        }

        public bool ihtiyacSahibiKontrolKaydet(IhtiyacSahibiKontrolTablo kontrolTablo,
            List<IhtiyacSahibiVerilecekEsyaTablo> esyaTablo, IhtiyacSahibiVerilecekMaddiTablo maddiTablo)
        {
            var ihtiyacSahibiKontrolTablo =
                db.IhtiyacSahibiKontrolTablo.FirstOrDefault(p =>
                    p.IhtiyacSahibiKontrolId == kontrolTablo.IhtiyacSahibiKontrolId);
            ihtiyacSahibiKontrolTablo.MuhtacMi = kontrolTablo.MuhtacMi;
            ihtiyacSahibiKontrolTablo.TahminiTeslimTarihi = kontrolTablo.TahminiTeslimTarihi;
            ihtiyacSahibiKontrolTablo.KontrolYapilmaTarihi = DateTime.Now;
            db.SaveChanges();
            for (int i = 0; i < esyaTablo.Count; i++)
            {
                if (esyaTablo[i].Adet > 0)
                {
                    int ihtiyacSahibiKontrolId = Convert.ToInt32(esyaTablo[i].IhtiyacSahibiKontrolTablo_IhtiyacSahibiKontrolId);
                    int esyaId = Convert.ToInt32(esyaTablo[i].EsyaTablo_EsyaId);
                    var verilecekEsyaTablo = db.IhtiyacSahibiVerilecekEsyaTablo.FirstOrDefault(p =>
                        p.IhtiyacSahibiKontrolTablo_IhtiyacSahibiKontrolId == ihtiyacSahibiKontrolId
                        && p.EsyaTablo_EsyaId == esyaId);
                    if (verilecekEsyaTablo != null)
                    {
                        verilecekEsyaTablo.Adet = esyaTablo[i].Adet;
                        db.SaveChanges();
                    }
                    else
                    {
                        IhtiyacSahibiVerilecekEsyaTablo esTablo = new IhtiyacSahibiVerilecekEsyaTablo();
                        esTablo.Adet = esyaTablo[i].Adet;
                        esTablo.EsyaTablo_EsyaId = esyaTablo[i].EsyaTablo_EsyaId;
                        esTablo.IhtiyacSahibiKontrolTablo_IhtiyacSahibiKontrolId = kontrolTablo.IhtiyacSahibiKontrolId;
                        db.IhtiyacSahibiVerilecekEsyaTablo.Add(esTablo);
                        db.SaveChanges();
                    }
                }
                else
                {
                    int? ihtiyacSahibiKontrolId = esyaTablo[i].IhtiyacSahibiKontrolTablo_IhtiyacSahibiKontrolId;
                    int esyaId = Convert.ToInt32(esyaTablo[i].EsyaTablo_EsyaId);
                    var verilecekEsyaTablo = db.IhtiyacSahibiVerilecekEsyaTablo.FirstOrDefault(p =>
                        p.IhtiyacSahibiKontrolTablo_IhtiyacSahibiKontrolId == ihtiyacSahibiKontrolId
                        &&p.EsyaTablo_EsyaId== esyaId);
                    if (verilecekEsyaTablo != null)
                    {
                        db.IhtiyacSahibiVerilecekEsyaTablo.Remove(verilecekEsyaTablo);
                        db.SaveChanges();
                    }
                }
            }

            if (maddiTablo.VerilecekMaddiYardim > 0)
            {
                var maddiYardimTablo = db.IhtiyacSahibiVerilecekMaddiTablo.FirstOrDefault(p =>
                    p.IhtiyacSahibiKontrolTablo_IhtiyacSahibiKontrolId == kontrolTablo.IhtiyacSahibiKontrolId &&
                    p.IhtiyacSahibiVerilecekMaddiId == maddiTablo.IhtiyacSahibiVerilecekMaddiId);
                if (maddiYardimTablo != null)
                {
                    maddiYardimTablo.VerilecekMaddiYardim = maddiTablo.VerilecekMaddiYardim;
                    maddiYardimTablo.VerilmeGerceklesmeTarihi=DateTime.Now;
                    db.SaveChanges();
                }
                else
                {
                    IhtiyacSahibiVerilecekMaddiTablo maddiVerilecek= new IhtiyacSahibiVerilecekMaddiTablo()
                    {
                         IhtiyacSahibiKontrolTablo_IhtiyacSahibiKontrolId=kontrolTablo.IhtiyacSahibiKontrolId,
                        VerilecekMaddiYardim=maddiTablo.VerilecekMaddiYardim,
                        VerilmeGerceklesmeTarihi=DateTime.Now
                    };
                    db.IhtiyacSahibiVerilecekMaddiTablo.Add(maddiVerilecek);
                    db.SaveChanges();
                }
            }
            else
            {
                var maddiYardimTablo = db.IhtiyacSahibiVerilecekMaddiTablo.FirstOrDefault(p =>
                    p.IhtiyacSahibiKontrolTablo_IhtiyacSahibiKontrolId == kontrolTablo.IhtiyacSahibiKontrolId &&
                    p.IhtiyacSahibiVerilecekMaddiId == maddiTablo.IhtiyacSahibiVerilecekMaddiId);
                if (maddiYardimTablo != null)
                {
                    db.IhtiyacSahibiVerilecekMaddiTablo.Remove(maddiYardimTablo);
                    db.SaveChanges();
                }
            }

            return true;
        }

        public List<IhtiyacSahibiVerilecekEsyaTablo> verilecekEsyalariGetir(int? ihtiyacSahibiKontrolId)
        {
            return db.IhtiyacSahibiVerilecekEsyaTablo
                .Where(p => p.IhtiyacSahibiKontrolTablo_IhtiyacSahibiKontrolId == ihtiyacSahibiKontrolId).ToList();
        }

        public IhtiyacSahibiVerilecekMaddiTablo verilecekMaddiGetir(int? ihtiyacSahibiKontrolId)
        {
            return db.IhtiyacSahibiVerilecekMaddiTablo.FirstOrDefault(p =>
                p.IhtiyacSahibiKontrolTablo_IhtiyacSahibiKontrolId == ihtiyacSahibiKontrolId);
        }



        public bool ihtiyacSahibiTeslimKaydet(List<IhtiyacSahibiVerilecekEsyaTablo> esyaTablo,
            IhtiyacSahibiVerilecekMaddiTablo maddiTablo)
        {
            int sayac = 0;
            for (int i = 0; i < esyaTablo.Count; i++)
            {
                int? ihtiyacSahibiKontrolId = esyaTablo[i].IhtiyacSahibiKontrolTablo_IhtiyacSahibiKontrolId;
                int? esyaId = esyaTablo[i].EsyaTablo_EsyaId;
                var esya = db.IhtiyacSahibiVerilecekEsyaTablo.FirstOrDefault(p =>
                    p.IhtiyacSahibiKontrolTablo_IhtiyacSahibiKontrolId == ihtiyacSahibiKontrolId &&
                    p.EsyaTablo_EsyaId == esyaId);
                if (esya != null)
                {
                    if ((esyaTablo[i].TeslimGerceklesmeTarihi.HasValue))
                    {
                        esya.TeslimGerceklesmeTarihi = esyaTablo[i].TeslimGerceklesmeTarihi;
                        sayac++;
                    }
                }
            }

            if (sayac == esyaTablo.Count)
            {
                var ihtiyacSahibiKontrol = db.IhtiyacSahibiKontrolTablo.FirstOrDefault(p =>
                    p.IhtiyacSahibiKontrolId == maddiTablo.IhtiyacSahibiKontrolTablo_IhtiyacSahibiKontrolId);
                if (ihtiyacSahibiKontrol != null)
                {
                    ihtiyacSahibiKontrol.TeslimTamamlandiMi = true;
                }
            }
            db.SaveChanges();
            if ((maddiTablo.VerilmeGerceklesmeTarihi.HasValue))
            {
                var maddiYardim = db.IhtiyacSahibiVerilecekMaddiTablo.FirstOrDefault(p =>
                    p.IhtiyacSahibiKontrolTablo_IhtiyacSahibiKontrolId ==
                    maddiTablo.IhtiyacSahibiKontrolTablo_IhtiyacSahibiKontrolId);
                if (maddiYardim != null)
                {
                    maddiYardim.VerilmeGerceklesmeTarihi=DateTime.Now;
                }
            }

            db.SaveChanges();
            return true;
        }
        public bool KullaniciIslemYapabilirMi(int? kullaniciId, int? ihtiyacSahibiKontrolId)
        {
            if (kullaniciDAL.KullaniciMerkezdeMi(kullaniciId))
            {
                return true;
            }
            else
            {
                if (kullaniciDAL.KullaniciSehir(kullaniciId) ==
                    ihtiyacSahibiGetir(ihtiyacSahibiKontrolId).SehirTablo_SehirId)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public bool IhtiyacSahibiKontrolVarMi(int? ihtiyacSahibiKontrolId)
        {
            if (db.IhtiyacSahibiKontrolTablo.FirstOrDefault(p => p.IhtiyacSahibiKontrolId == ihtiyacSahibiKontrolId) !=
                null)
            {
                return true;
            }

            return false;
        }

        public bool IhtiyacSahibiMuhtacMi(int? ihtiyacSahibiKontrolId)
        {
            if (db.IhtiyacSahibiKontrolTablo.FirstOrDefault(p => p.IhtiyacSahibiKontrolId == ihtiyacSahibiKontrolId) !=
                null)
            {
                if (db.IhtiyacSahibiKontrolTablo.FirstOrDefault(p => p.IhtiyacSahibiKontrolId == ihtiyacSahibiKontrolId)
                        .MuhtacMi == true)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

            return false;
        }

        public bool TeslimTamamlandiMi(int? ihtiyacSahibiKontrolId)
        {
            if (IhtiyacSahibiKontrolVarMi(ihtiyacSahibiKontrolId))
            {
                var sonuc = db.IhtiyacSahibiKontrolTablo.FirstOrDefault(p =>
                    p.IhtiyacSahibiKontrolId == ihtiyacSahibiKontrolId);
                if (sonuc.TeslimTamamlandiMi != null)
                {
                    if (sonuc.TeslimTamamlandiMi == true)
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
                return false;
            }
        }
        public bool DonusurMu(string a, int? tip)
        {
            try
            {
                if (tip == 1)
                {
                    Convert.ToDateTime(a);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}
