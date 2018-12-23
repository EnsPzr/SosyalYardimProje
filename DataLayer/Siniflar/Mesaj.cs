﻿using System;
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
            string tarih)
        {
            if (kullaniciDAL.KullaniciMerkezdeMi(kullaniciId))
            {
                var sorgu = db.MesajTablo.Include(p => p.KullaniciBilgileriTablo).Include(p => p.MesajDetayTablo)
                    .AsQueryable();
                if (arananKullaniciId != null)
                {
                    sorgu = sorgu.Where(p => p.KullaniciBilgleriTablo_KullaniciId == arananKullaniciId);
                }

                if (aranan != null)
                {
                    MesajDetayTablo mesaj = new MesajDetayTablo()
                    {
                        MesajMetni = aranan
                    };
                    sorgu = sorgu.Where(p => p.MesajDetayTablo.Contains(mesaj)
                                             || p.KullaniciBilgileriTablo.KullaniciAdi.Contains(aranan)
                                             || p.KullaniciBilgileriTablo.KullaniciSoyadi.Contains(aranan)
                                             || p.Tarih.ToString().Contains(aranan)
                                             || p.KullaniciBilgileriTablo.KullaniciEPosta.Contains(aranan));
                }

                if (tarih != null)
                {
                    DateTime? tarihDate = Convert.ToDateTime(tarih);
                    sorgu = sorgu.Where(p => p.Tarih == tarihDate);
                }

                return sorgu.ToList();
            }
            else
            {
                var sorgu = db.MesajTablo.Include(p => p.KullaniciBilgileriTablo).Include(p => p.MesajDetayTablo)
                    .Where(p => p.KullaniciBilgleriTablo_KullaniciId == kullaniciId).AsQueryable();

                if (aranan != null)
                {
                    MesajDetayTablo mesaj = new MesajDetayTablo()
                    {
                        MesajMetni = aranan
                    };
                    sorgu = sorgu.Where(p => p.MesajDetayTablo.Contains(mesaj)
                                             || p.KullaniciBilgileriTablo.KullaniciAdi.Contains(aranan)
                                             || p.KullaniciBilgileriTablo.KullaniciSoyadi.Contains(aranan)
                                             || p.Tarih.ToString().Contains(aranan)
                                             || p.KullaniciBilgileriTablo.KullaniciEPosta.Contains(aranan));
                }

                if (tarih != null)
                {
                    DateTime? tarihDate = Convert.ToDateTime(tarih);
                    sorgu = sorgu.Where(p => p.Tarih == tarihDate);
                }

                return sorgu.ToList();
            }
        }
    }
}
