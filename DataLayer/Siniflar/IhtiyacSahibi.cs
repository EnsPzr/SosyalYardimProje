using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Data.Entity.Migrations;

namespace DataLayer.Siniflar
{
    public class IhtiyacSahibi
    {
        private KullaniciYonetimi kullaniciDAL= new KullaniciYonetimi();
        private SosyalYardimDB db = new SosyalYardimDB();
        public List<IhtiyacSahibiTablo> TumIhtiyacSahipleriniGetir(int? KullaniciId)
        {
            if (kullaniciDAL.KullaniciBul(KullaniciId).KullaniciMerkezdeMi == true)
            {
                return db.IhtiyacSahibiTablo.Include(p=>p.SehirTablo).ToList();
            }
            else
            {
                int? sehirId = kullaniciDAL.KullaniciBul(KullaniciId).SehirTablo_SehirId;
                return db.IhtiyacSahibiTablo.Include(p => p.SehirTablo).Where(p =>
                    p.SehirTablo_SehirId == sehirId).ToList();
            }
        }

        public List<IhtiyacSahibiTablo> FiltreliIhtiyacSahipleriListesiniGetir(String aranan, int? sehirId,int? kullaniciId)
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
                return true;
            }
            else
            {
                return false;
            }
        }

        public IhtiyacSahibiTablo IhtiyacSahibiVarMi(string adi, string soyadi, string telNo)
        {
            var ihtiyacSahibi = db.IhtiyacSahibiTablo.Include(p=>p.SehirTablo).FirstOrDefault(p =>
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
                                                  && p.IhtiyacSahibiTelNo == telNo)&&p.IhtiyacSahibiId!=id);
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
    }
}
