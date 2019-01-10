using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Data.Entity.Migrations;

namespace DataLayer.Siniflar
{
    public class Kullanici
    {
        private SosyalYardimDB db = new SosyalYardimDB();
        public List<KullaniciBilgileriTablo> TumKullanicilariGetir()
        {
            return db.KullaniciBilgileriTablo.Include(p => p.SehirTablo).ToList();
        }
        public List<KullaniciBilgileriTablo> TumKullanicilariGetir(int? id)
        {
            return db.KullaniciBilgileriTablo.Include(p => p.SehirTablo).Where(p => p.SehirTablo_SehirId == id).ToList();
        }
        public List<KullaniciBilgileriTablo> FiltreliKullanicilariGetir(string aranan, int? SehirId, int? KullaniciId, bool? OnayliMi, bool? merkezdemi, bool? aktifMi)
        {
            var kullanici = KullaniciGetir(KullaniciId);
            if (Convert.ToBoolean(kullanici.KullaniciMerkezdeMi))
            {
                IQueryable<KullaniciBilgileriTablo> sorgu = db.KullaniciBilgileriTablo.Include(p => p.SehirTablo)
                    .Where(p => (p.KullaniciAdi.Contains(aranan) ||
                                 p.KullaniciSoyadi.Contains(aranan) ||
                                 p.KullaniciTelegramKullaniciAdi.Contains(aranan) ||
                                 p.KullaniciTelefonNumarasi.Contains(aranan) ||
                                 p.KullaniciEPosta.Contains(aranan)
                                 || p.SehirTablo.SehirAdi.Contains(aranan)));
                if (SehirId != null)
                {
                    sorgu = sorgu.Where(p => p.SehirTablo_SehirId == SehirId);
                }

                if (aktifMi != null)
                {
                    sorgu = sorgu.Where(p => p.AktifMi == aktifMi);
                }

                if (OnayliMi != null)
                {
                    sorgu = sorgu.Where(p => p.KullaniciOnayliMi==OnayliMi);
                }

                if (merkezdemi != null)
                {
                    sorgu = sorgu.Where(p => p.KullaniciMerkezdeMi == merkezdemi);
                }
                return sorgu.ToList();
            }
            else
            {
                IQueryable<KullaniciBilgileriTablo> sorgu = db.KullaniciBilgileriTablo.Include(p => p.SehirTablo)
                    .Where(p => (p.KullaniciAdi.Contains(aranan) ||
                                 p.KullaniciSoyadi.Contains(aranan) ||
                                 p.KullaniciTelegramKullaniciAdi.Contains(aranan) ||
                                 p.KullaniciTelefonNumarasi.Contains(aranan) ||
                                 p.KullaniciEPosta.Contains(aranan)) && p.SehirTablo_SehirId == kullanici.SehirTablo_SehirId);

                if (aktifMi != null)
                {
                    sorgu = sorgu.Where(p => p.AktifMi == aktifMi);
                }

                if (OnayliMi != null)
                {
                    sorgu = sorgu.Where(p => p.KullaniciOnayliMi == OnayliMi);
                }
                return sorgu.ToList();
            }

        }

        public List<SehirTablo> TumSehirler()
        {
            return db.SehirTablo.ToList();
        }

        public KullaniciBilgileriTablo KullaniciGetir(int? id)
        {
            return db.KullaniciBilgileriTablo.Include(p => p.SehirTablo).FirstOrDefault(p => p.KullaniciId == id);
        }

        public bool KullaniciSil(int? id)
        {
            KullaniciGetir(id).AktifMi = false;
            if (db.SaveChanges() > 0) return true;
            else return false;
        }

        public bool KullaniciVarMi(String eposta)
        {
            return db.KullaniciBilgileriTablo.FirstOrDefault(p => p.KullaniciEPosta == eposta) != null ? true : false;
        }
        public int? KullaniciVarMiInt(String eposta)
        {
            return db.KullaniciBilgileriTablo.FirstOrDefault(p => p.KullaniciEPosta == eposta) != null ? db.KullaniciBilgileriTablo.FirstOrDefault(p => p.KullaniciEPosta == eposta).KullaniciId : -1;
        }
        public bool KullaniciVarMi(String eposta, int? id)
        {
            return db.KullaniciBilgileriTablo.FirstOrDefault(p => p.KullaniciEPosta == eposta && p.KullaniciId != id) != null ? true : false;
        }

        public bool KullaniciEkle(KullaniciBilgileriTablo eklenecekKullanici)
        {
            db.KullaniciBilgileriTablo.Add(eklenecekKullanici);
            if (db.SaveChanges() > 0)
            {
                var eklenenKullanici =
                    db.KullaniciBilgileriTablo.FirstOrDefault(p =>
                        p.KullaniciEPosta == eklenecekKullanici.KullaniciEPosta);
                if (eklenenKullanici != null)
                {
                    for (int i = 0; i < 60; i++)
                    {
                        var rotaVarMi = db.RotaTablo.FirstOrDefault(p => p.RotaId == i);
                        if (rotaVarMi != null)
                        {
                            YetkiTablo yetki = new YetkiTablo();
                            yetki.GirebilirMi = true;
                            yetki.RotaTablo_RotaId = i;
                            yetki.KullaniciBilgileriTablo_KullaniciId = eklenenKullanici.KullaniciId;
                            db.YetkiTablo.Add(yetki);
                        }
                    }
                    YetkiTablo ekYetki = new YetkiTablo();
                    ekYetki.GirebilirMi = true;
                    ekYetki.RotaTablo_RotaId = 78;
                    ekYetki.KullaniciBilgileriTablo_KullaniciId = eklenenKullanici.KullaniciId;
                    db.YetkiTablo.Add(ekYetki);
                    db.SaveChanges();
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

        public bool KullaniciGuncelle(KullaniciBilgileriTablo guncellenmisKullanici)
        {
            db.KullaniciBilgileriTablo.AddOrUpdate(guncellenmisKullanici);
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
}
