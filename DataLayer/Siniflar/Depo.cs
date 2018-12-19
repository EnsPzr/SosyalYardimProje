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
                var sonuc = db.DepoTablo.Include(p => p.EsyaTablo).Include(p => p.SehirTablo).AsQueryable();
                if (esyaId != null)
                {
                    sonuc.Where(p => p.EsyaTablo_EsyaId == esyaId);
                }

                if (sehirId != null)
                {
                    sonuc.Where(p => p.SehirTablo_SehirId == sehirId);
                }

                if (aranan != null)
                {
                    aranan = aranan.Trim().ToLower();
                    sonuc.Where(p => p.SehirTablo.SehirAdi.Trim().ToLower().Contains(aranan)
                                     || p.EsyaTablo.EsyaAdi.Trim().ToLower().Contains(aranan));
                }
                return sonuc.ToList();
            }
            else
            {
                int? KullaniciSehirId = kullaniciDAL.KullaniciSehir(KullaniciId);
                var sonuc = db.DepoTablo.Include(p => p.EsyaTablo).Include(p => p.SehirTablo).Where(p => p.SehirTablo_SehirId == KullaniciSehirId).AsQueryable();
                if (esyaId != null)
                {
                    sonuc.Where(p => p.EsyaTablo_EsyaId == esyaId);
                }

                if (aranan != null)
                {
                    aranan = aranan.Trim().ToLower();
                    sonuc.Where(p => p.SehirTablo.SehirAdi.Trim().ToLower().Contains(aranan)
                                     || p.EsyaTablo.EsyaAdi.Trim().ToLower().Contains(aranan));
                }
                return sonuc.ToList();
            }
        }
    }
}
