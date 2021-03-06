﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
namespace DataLayer.Siniflar
{
    public class GeriBildirim
    {
        private SosyalYardimDB db = new SosyalYardimDB();
        private KullaniciYonetimi kullaniciDAL = new KullaniciYonetimi();

        public List<GeriBildirimTablo> TumGeriBildirimleriGetir(int? kullaniciId)
        {
            if (kullaniciDAL.KullaniciMerkezdeMi(kullaniciId))
            {
                return db.GeriBildirimTablo.Include(p => p.KullaniciBilgileriTablo).ToList();
            }
            else
            {
                var kullanici = kullaniciDAL.KullaniciBul(kullaniciId);
                if (kullanici.BagisciMi==true)
                {
                    return db.GeriBildirimTablo.Include(p => p.KullaniciBilgileriTablo)
                        .Include(p => p.KullaniciBilgileriTablo.SehirTablo)
                        .Where(p => p.KullaniciBilgileriTablo_KullaniciId == kullaniciId).OrderByDescending(p => p.Tarih).ToList();
                }
                else
                {
                    int? kullaniciSehirId = kullaniciDAL.KullaniciSehir(kullaniciId);
                    return db.GeriBildirimTablo.Include(p => p.KullaniciBilgileriTablo)
                        .Include(p => p.KullaniciBilgileriTablo.SehirTablo).Where(p => p.KullaniciBilgileriTablo.SehirTablo_SehirId == kullaniciSehirId).OrderByDescending(p=>p.Tarih).ToList();
                }
            }
        }

        public List<GeriBildirimTablo> FiltreliGeriBildirimleriGetir(int? kullaniciId, string aranan, string tarih, int? sehirId)
        {
            if (kullaniciDAL.KullaniciMerkezdeMi(kullaniciId))
            {
                var sorgu = db.GeriBildirimTablo.Include(p => p.KullaniciBilgileriTablo).AsQueryable();

                if (aranan != null)
                {
                    sorgu = sorgu.Where(p => p.GeriBildirimKonu.Contains(aranan)
                                             || p.GeriBildirimMesaj.Contains(aranan)
                                             || p.Tarih.ToString().Contains(aranan)
                                             || p.KullaniciBilgileriTablo.KullaniciAdi.Contains(aranan)
                                             || p.KullaniciBilgileriTablo.KullaniciSoyadi.Contains(aranan)
                                             || p.KullaniciBilgileriTablo.KullaniciEPosta.Contains(aranan)
                                             || p.KullaniciBilgileriTablo.KullaniciTelefonNumarasi.Contains(aranan));
                }

                if (tarih != null)
                {
                    DateTime? cTarih = Convert.ToDateTime(tarih);
                    sorgu = sorgu.Where(p => p.Tarih == cTarih);
                }

                if (sehirId != null)
                {
                    sorgu = sorgu.Where(p => p.KullaniciBilgileriTablo.SehirTablo_SehirId == sehirId);
                }

                return sorgu.OrderByDescending(p=>p.Tarih).ToList();
            }
            else
            {
                int? kullaniciSehirId = kullaniciDAL.KullaniciSehir(kullaniciId);
                var sorgu = db.GeriBildirimTablo.Include(p => p.KullaniciBilgileriTablo).Where(p => p.KullaniciBilgileriTablo.SehirTablo_SehirId == kullaniciSehirId).AsQueryable();

                if (aranan != null)
                {
                    sorgu = sorgu.Where(p => p.GeriBildirimKonu.Contains(aranan)
                                             || p.GeriBildirimMesaj.Contains(aranan)
                                             || p.Tarih.ToString().Contains(aranan)
                                             || p.KullaniciBilgileriTablo.KullaniciAdi.Contains(aranan)
                                             || p.KullaniciBilgileriTablo.KullaniciSoyadi.Contains(aranan)
                                             || p.KullaniciBilgileriTablo.KullaniciEPosta.Contains(aranan)
                                             || p.KullaniciBilgileriTablo.KullaniciTelefonNumarasi.Contains(aranan));
                }

                if (tarih != null)
                {
                    DateTime? cTarih = Convert.ToDateTime(tarih);
                    sorgu = sorgu.Where(p => p.Tarih == cTarih);
                }
                
                return sorgu.OrderByDescending(p => p.Tarih).ToList();
            }
        }

        public GeriBildirimTablo GeriBildirimGetir(int? geriBildirimId,int? sayi)
        {
            var geriBildirim = db.GeriBildirimTablo.Include(p => p.KullaniciBilgileriTablo)
                .FirstOrDefault(p => p.GeriBildirimId == geriBildirimId);
            if (geriBildirim != null)
            {
                if (sayi == null)
                {
                    geriBildirim.GeriBildirimDurumu = 1;
                    db.SaveChanges();
                }
            }

            return geriBildirim;
        }

        public bool KullaniciIslemYapabilirMi(int? kullaniciId, int? geriBildirimId)
        {
            if (kullaniciDAL.KullaniciMerkezdeMi(kullaniciId))
            {
                return true;
            }
            else
            {
                var geriBildirim = db.GeriBildirimTablo.Include(p => p.KullaniciBilgileriTablo)
                    .FirstOrDefault(p => p.GeriBildirimId == geriBildirimId);
                if (geriBildirim != null)
                {
                    int? kullaniciSehirId = kullaniciDAL.KullaniciSehir(kullaniciId);
                    if (geriBildirim.KullaniciBilgileriTablo.SehirTablo_SehirId != null)
                    {
                        if (geriBildirim.KullaniciBilgileriTablo.SehirTablo_SehirId == kullaniciSehirId)
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
        }

        public bool GeriBildirimKaydet(int? geriBildirimId, int? durumId)
        {
            var geriBildirim = db.GeriBildirimTablo.FirstOrDefault(p => p.GeriBildirimId == geriBildirimId);
            if (geriBildirim != null)
            {
                geriBildirim.GeriBildirimDurumu = durumId;
                db.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool YeniGeriBildirimKaydet(KullaniciBilgileriTablo kullanici, GeriBildirimTablo geriBildirim)
        {
            if (kullanici == null)
            {
                db.GeriBildirimTablo.Add(geriBildirim);
                db.SaveChanges();
                return true;
            }
            else
            {
                kullanici.KullaniciSifre = "123456";
                kullanici.BagisciMi = true;
                db.KullaniciBilgileriTablo.Add(kullanici);
                db.SaveChanges();
                var kul = db.KullaniciBilgileriTablo.FirstOrDefault(p =>
                    p.KullaniciEPosta == kullanici.KullaniciEPosta);
                if (kul != null)
                {
                    geriBildirim.KullaniciBilgileriTablo_KullaniciId = kul.KullaniciId;
                    db.GeriBildirimTablo.Add(geriBildirim);
                    db.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public bool BagiciGeriBildirimiGuncelleyeBilirMi(int? kullaniciId, int? geriBildirimId)
        {
            var geriBildirim = db.GeriBildirimTablo.FirstOrDefault(p =>
                p.KullaniciBilgileriTablo_KullaniciId == kullaniciId
                && p.GeriBildirimId == geriBildirimId);
            if (geriBildirim != null)
            {
                var kullanici = db.KullaniciBilgileriTablo.FirstOrDefault(p => p.KullaniciId == kullaniciId);
                if (kullanici != null)
                {
                    if (geriBildirim.GeriBildirimDurumu > 0)
                    {
                        return false;
                    }
                    else
                    {
                        return true;
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

        public bool GeriBildirimGuncelle(GeriBildirimTablo geriBildirim)
        {
            var geriBildirimTablo =
                db.GeriBildirimTablo.FirstOrDefault(p => p.GeriBildirimId == geriBildirim.GeriBildirimId);
            if (geriBildirimTablo != null)
            {
                geriBildirimTablo.GeriBildirimKonu = geriBildirim.GeriBildirimKonu;
                geriBildirimTablo.GeriBildirimMesaj = geriBildirim.GeriBildirimMesaj;
                db.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool GeriBildirimSil(int? id)
        {
            var geriBildirim = db.GeriBildirimTablo.FirstOrDefault(p => p.GeriBildirimId == id);
            if (geriBildirim != null)
            {
                db.GeriBildirimTablo.Remove(geriBildirim);
                if (db.SaveChanges() > 0)
                {
                    return true;
                }

                return false;
            }
            else
            {
                return false;
            }
        }

        public bool DisardanGeriBildirimKaydet(KullaniciBilgileriTablo kullanici, GeriBildirimTablo geriBildirimTablo)
        {
            var kullaniciVarMi =
                db.KullaniciBilgileriTablo.FirstOrDefault(p => p.KullaniciEPosta == kullanici.KullaniciEPosta);
            if (kullaniciVarMi != null)
            {
                geriBildirimTablo.GeriBildirimDurumu = 0;
                geriBildirimTablo.KullaniciBilgileriTablo_KullaniciId = kullaniciVarMi.KullaniciId;
                geriBildirimTablo.Tarih=DateTime.Now;
                db.GeriBildirimTablo.Add(geriBildirimTablo);
                db.SaveChanges();
                return true;
            }
            else
            {
                kullanici.BagisciMi = true;
                kullanici.AktifMi = true;
                db.KullaniciBilgileriTablo.Add(kullanici);
                db.SaveChanges();
                var eklenenKullanici= db.KullaniciBilgileriTablo.
                    FirstOrDefault(p => p.KullaniciEPosta == kullanici.KullaniciEPosta);
                if (eklenenKullanici != null)
                {
                    geriBildirimTablo.GeriBildirimDurumu = 0;
                    geriBildirimTablo.KullaniciBilgileriTablo_KullaniciId = eklenenKullanici.KullaniciId;
                    geriBildirimTablo.Tarih = DateTime.Now;
                    db.GeriBildirimTablo.Add(geriBildirimTablo);
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
