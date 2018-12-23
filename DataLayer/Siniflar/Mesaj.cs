using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace DataLayer.Siniflar
{
    public class Mesaj
    {
        private SosyalYardimDB db = new SosyalYardimDB();
        private KullaniciYonetimi kullaniciDAL = new KullaniciYonetimi();
        private Kullanici kullaniciBilgileri = new Kullanici();
        private Sube subeDAL = new Sube();

        public List<MesajTablo> TumMesajlariGetir(int? kullaniciId)
        {
            if (kullaniciDAL.KullaniciMerkezdeMi(kullaniciId))
            {
                return db.MesajTablo.Include(p => p.KullaniciBilgileriTablo).ToList();
            }
            else
            {
                return db.MesajTablo.Include(p => p.KullaniciBilgileriTablo)
                    .Where(p => p.KullaniciBilgleriTablo_KullaniciId == kullaniciId).ToList();
            }
        }

        public List<MesajTablo> FiltreliMesajlariGetir(int? kullaniciId, int? arananKullaniciId, string aranan,
            string tarih, int? kimeGonderildi)
        {
            if (kullaniciDAL.KullaniciMerkezdeMi(kullaniciId))
            {
                var sorgu = db.MesajTablo.Include(p => p.KullaniciBilgileriTablo).Include(p => p.MesajDetayTablo)
                    .AsQueryable();
                if (arananKullaniciId != null)
                {
                    sorgu = sorgu.Where(p => p.KullaniciBilgleriTablo_KullaniciId == arananKullaniciId);
                }

                if (tarih != null)
                {
                    DateTime? tarihDate = Convert.ToDateTime(tarih);
                    sorgu = sorgu.Where(p => p.Tarih == tarihDate);
                }

                if (kimeGonderildi != null)
                {
                    sorgu = sorgu.Where(p => p.KimeAtildi == kimeGonderildi);
                }

                return sorgu.ToList();
            }
            else
            {
                var sorgu = db.MesajTablo.Include(p => p.KullaniciBilgileriTablo).Include(p => p.MesajDetayTablo)
                    .Where(p => p.KullaniciBilgleriTablo_KullaniciId == kullaniciId).AsQueryable();

                if (tarih != null)
                {
                    DateTime? tarihDate = Convert.ToDateTime(tarih);
                    sorgu = sorgu.Where(p => p.Tarih == tarihDate);
                }

                if (kimeGonderildi != null)
                {
                    sorgu = sorgu.Where(p => p.KimeAtildi == kimeGonderildi);
                }

                return sorgu.ToList();
            }
        }

        public List<MesajDetayTablo> TumMesajDetayGetir(int? mesajId)
        {
            return db.MesajDetayTablo.Include(p => p.KullaniciBilgileriTablo).Where(p => p.MesajTablo_MesajId == mesajId)
                .ToList();
        }

        public List<MesajDetayTablo> FiltreliMesajDetayGetir(int? mesajId, string aranan)
        {
            var sorgu = db.MesajDetayTablo.Include(p => p.KullaniciBilgileriTablo)
                .Where(p => p.MesajTablo_MesajId == mesajId).AsQueryable();

            if (aranan != null)
            {
                sorgu = sorgu.Where(p => p.MesajMetni.Contains(aranan)
                                         || p.KullaniciBilgileriTablo.KullaniciAdi.Contains(aranan)
                                         || p.KullaniciBilgileriTablo.KullaniciSoyadi.Contains(aranan)
                                         || p.KullaniciBilgileriTablo.KullaniciEPosta.Contains(aranan));
            }

            return sorgu.ToList();
        }

        public bool KullaniciIslemYapabilirMi(int? kullaniciId, int? mesajId)
        {
            if (kullaniciDAL.KullaniciMerkezdeMi(kullaniciId))
            {
                return true;
            }
            else
            {
                if (db.MesajTablo.FirstOrDefault(p =>
                        p.KullaniciBilgleriTablo_KullaniciId == kullaniciId && p.MesajId == mesajId) != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public bool MesajGonder(MesajTablo mesajTablo, MesajDetayTablo mesajDetay, int? sehirId)
        {
            if (kullaniciDAL.KullaniciMerkezdeMi(mesajTablo.KullaniciBilgleriTablo_KullaniciId))
            {
                db.MesajTablo.Add(mesajTablo);
                db.SaveChanges();
                var id = db.MesajTablo.FirstOrDefault(p =>
                    p.KullaniciBilgleriTablo_KullaniciId == mesajTablo.KullaniciBilgleriTablo_KullaniciId
                    && p.Tarih == mesajTablo.Tarih && p.Zaman == mesajTablo.Zaman);
                if (id != null)
                {
                    if (mesajTablo.KimeAtildi == 0)
                    {
                        var tumKullanicilar =
                            kullaniciBilgileri.TumKullanicilariGetir();
                        tumKullanicilar = tumKullanicilar.Where(p => p.BagisciMi == false || p.BagisciMi == null)
                            .ToList();
                        for (int i = 0; i < tumKullanicilar.Count; i++)
                        {
                            if (sehirId != null)
                            {
                                if (sehirId == 82)
                                {
                                    MesajDetayTablo yeniMesaj = new MesajDetayTablo();
                                    yeniMesaj.MesajTablo_MesajId = id.MesajId;
                                    yeniMesaj.KullaniciBilgileriTablo_KullaniciId = tumKullanicilar[i].KullaniciId;
                                    yeniMesaj.MesajMetni = mesajDetay.MesajMetni;
                                    db.MesajDetayTablo.Add(yeniMesaj);
                                    db.SaveChanges();
                                }
                                else
                                {
                                    if (tumKullanicilar[i].SehirTablo_SehirId == sehirId)
                                    {
                                        MesajDetayTablo yeniMesaj = new MesajDetayTablo();
                                        yeniMesaj.MesajTablo_MesajId = id.MesajId;
                                        yeniMesaj.KullaniciBilgileriTablo_KullaniciId = tumKullanicilar[i].KullaniciId;
                                        yeniMesaj.MesajMetni = mesajDetay.MesajMetni;
                                        db.MesajDetayTablo.Add(yeniMesaj);
                                        db.SaveChanges();
                                    }
                                }
                            }
                            else
                            {
                                MesajDetayTablo yeniMesaj = new MesajDetayTablo();
                                yeniMesaj.MesajTablo_MesajId = id.MesajId;
                                yeniMesaj.KullaniciBilgileriTablo_KullaniciId = tumKullanicilar[i].KullaniciId;
                                yeniMesaj.MesajMetni = mesajDetay.MesajMetni;
                                db.MesajDetayTablo.Add(yeniMesaj);
                                db.SaveChanges();
                            }
                        }

                        return true;
                    }
                    else if (mesajTablo.KimeAtildi == 1)
                    {
                        var subeler = subeDAL.TumSubeleriGetir();
                        for (int i = 0; i < subeler.Count; i++)
                        {
                            if (sehirId != null)
                            {
                                if (sehirId == 82)
                                {
                                    MesajDetayTablo yeniMesaj = new MesajDetayTablo();
                                    yeniMesaj.MesajTablo_MesajId = id.MesajId;
                                    yeniMesaj.KullaniciBilgileriTablo_KullaniciId =
                                        subeler[i].KullaniciBilgileriTablo_KullaniciId;
                                    yeniMesaj.MesajMetni = mesajDetay.MesajMetni;
                                    db.MesajDetayTablo.Add(yeniMesaj);
                                    db.SaveChanges();
                                }
                                else
                                {
                                    if (subeler[i].KullaniciBilgileriTablo.SehirTablo_SehirId == sehirId)
                                    {
                                        MesajDetayTablo yeniMesaj = new MesajDetayTablo();
                                        yeniMesaj.MesajTablo_MesajId = id.MesajId;
                                        yeniMesaj.KullaniciBilgileriTablo_KullaniciId =
                                            subeler[i].KullaniciBilgileriTablo_KullaniciId;
                                        yeniMesaj.MesajMetni = mesajDetay.MesajMetni;
                                        db.MesajDetayTablo.Add(yeniMesaj);
                                        db.SaveChanges();
                                    }
                                }
                            }
                            else
                            {
                                MesajDetayTablo yeniMesaj = new MesajDetayTablo();
                                yeniMesaj.MesajTablo_MesajId = id.MesajId;
                                yeniMesaj.KullaniciBilgileriTablo_KullaniciId =
                                    subeler[i].KullaniciBilgileriTablo_KullaniciId;
                                yeniMesaj.MesajMetni = mesajDetay.MesajMetni;
                                db.MesajDetayTablo.Add(yeniMesaj);
                                db.SaveChanges();
                            }
                        }

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
                db.MesajTablo.Add(mesajTablo);
                db.SaveChanges();
                var id = db.MesajTablo.FirstOrDefault(p =>
                    p.KullaniciBilgleriTablo_KullaniciId == mesajTablo.KullaniciBilgleriTablo_KullaniciId
                    && p.Tarih == mesajTablo.Tarih && p.Zaman == mesajTablo.Zaman);
                if (id != null)
                {
                    if (mesajTablo.KimeAtildi == 0)
                    {
                        var tumKullanicilar =
                            kullaniciBilgileri.TumKullanicilariGetir(mesajTablo.KullaniciBilgleriTablo_KullaniciId);
                        tumKullanicilar = tumKullanicilar.Where(p => p.BagisciMi == false || p.BagisciMi == null)
                            .ToList();
                        for (int i = 0; i < tumKullanicilar.Count; i++)
                        {
                            MesajDetayTablo yeniMesaj = new MesajDetayTablo();
                            yeniMesaj.MesajTablo_MesajId = id.MesajId;
                            yeniMesaj.KullaniciBilgileriTablo_KullaniciId = tumKullanicilar[i].KullaniciId;
                            yeniMesaj.MesajMetni = mesajDetay.MesajMetni;
                            db.MesajDetayTablo.Add(yeniMesaj);
                            db.SaveChanges();
                        }

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
