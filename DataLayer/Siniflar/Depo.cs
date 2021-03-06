﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
namespace DataLayer.Siniflar
{
    public class Depo
    {
        private KullaniciYonetimi kullaniciDAL = new KullaniciYonetimi();
        private SosyalYardimDB db = new SosyalYardimDB();
        public List<DepoTablo> DepoGetir(int? KullaniciId)
        {
            if (kullaniciDAL.KullaniciMerkezdeMi(KullaniciId))
            {
                return db.DepoTablo.Include(p => p.SehirTablo).Include(p => p.EsyaTablo).ToList();
            }
            else
            {
                int? KullaniciSehirId = kullaniciDAL.KullaniciSehir(KullaniciId);
                return db.DepoTablo.Include(p => p.SehirTablo).Include(p => p.EsyaTablo).Where(p => p.SehirTablo_SehirId == KullaniciSehirId).ToList();
            }
        }

        public List<DepoTablo> FiltreliDepoGetir(int? KullaniciId, int? esyaId, int? sehirId, String aranan)
        {
            if (kullaniciDAL.KullaniciMerkezdeMi(KullaniciId))
            {
                IQueryable<DepoTablo> sonuc = db.DepoTablo.Include(p => p.EsyaTablo).Include(p => p.SehirTablo);
                if (esyaId != null)
                {
                    sonuc = sonuc.Where(p => p.EsyaTablo.EsyaId == esyaId);
                }

                if (sehirId != null)
                {
                    sonuc = sonuc.Where(p => p.SehirTablo.SehirId == sehirId);
                }

                if (!(aranan.Equals("")))
                {
                    aranan = aranan.Trim().ToLower();
                    sonuc = sonuc.Where(p => p.SehirTablo.SehirAdi.Trim().ToLower().Contains(aranan)
                                       || p.EsyaTablo.EsyaAdi.Trim().ToLower().Contains(aranan));
                }
                return sonuc.ToList();
            }
            else
            {
                int? KullaniciSehirId = kullaniciDAL.KullaniciSehir(KullaniciId);
                IQueryable<DepoTablo> sonuc = db.DepoTablo.Include(p => p.EsyaTablo).Include(p => p.SehirTablo).Where(p => p.SehirTablo_SehirId == KullaniciSehirId).AsQueryable();
                if (esyaId != null)
                {
                    sonuc = sonuc.Where(p => p.EsyaTablo.EsyaId == esyaId);
                }

                if (!(aranan.Equals("")))
                {
                    aranan = aranan.Trim().ToLower();
                    sonuc = sonuc.Where(p => p.SehirTablo.SehirAdi.Trim().ToLower().Contains(aranan)
                                       || p.EsyaTablo.EsyaAdi.Trim().ToLower().Contains(aranan));
                }
                return sonuc.ToList();
            }
        }

        public bool DepodaEsyaVarMi(int? esyaId, int? sehirId)
        {
            if (db.DepoTablo.FirstOrDefault(p => p.EsyaTablo_EsyaId == esyaId && p.SehirTablo_SehirId == sehirId) !=
                null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool DepoyaEsyaEkle(DepoTablo eklenecekEsya)
        {
            db.DepoTablo.Add(eklenecekEsya);
            if (db.SaveChanges() > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool KullaniciEklemeYapabilirMi(int? kullaniciId, int? sehirId)
        {
            if (kullaniciDAL.KullaniciMerkezdeMi(kullaniciId))
            {
                return true;
            }
            else
            {
                if (kullaniciDAL.KullaniciSehir(kullaniciId) == sehirId)
                {
                    return true;
                }
                return false;
            }
        }

        public bool AyniEsyaVarMi(DepoTablo esya)
        {
            if (db.DepoTablo.FirstOrDefault(p =>
                    p.DepoEsyaId != esya.DepoEsyaId && p.EsyaTablo_EsyaId == esya.EsyaTablo_EsyaId &&
                    p.SehirTablo_SehirId == esya.SehirTablo_SehirId) != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public DepoTablo DepoEsyaGetir(int? id)
        {
            var esya = db.DepoTablo.Include(p => p.EsyaTablo).Include(p => p.SehirTablo)
                .FirstOrDefault(p => p.DepoEsyaId == id);
            return esya;
        }

        public bool DepoEsyaGuncelle(DepoTablo esya)
        {
            var guncellenecekEsya = db.DepoTablo.FirstOrDefault(p => p.DepoEsyaId == esya.DepoEsyaId);
            if (guncellenecekEsya != null)
            {
                if (guncellenecekEsya.SehirTablo_SehirId == esya.SehirTablo_SehirId && guncellenecekEsya.Adet ==
                                                                                    esya.Adet
                                                                                    && guncellenecekEsya
                                                                                        .EsyaTablo_EsyaId ==
                                                                                    esya.EsyaTablo_EsyaId)
                {
                    db.SaveChanges();
                    return true;
                }
                else
                {
                    guncellenecekEsya.Adet = esya.Adet;
                    guncellenecekEsya.EsyaTablo_EsyaId = esya.EsyaTablo_EsyaId;
                    guncellenecekEsya.SehirTablo_SehirId = esya.SehirTablo_SehirId;
                    if (db.SaveChanges() > 0)
                    {
                        return true;
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
}
