using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;

namespace DataLayer
{
    public class KullaniciYonetimi
    {
        private SosyalYardimDB db = new SosyalYardimDB();
        public KullaniciBilgileriTablo KullaniciBul(int? KullaniciId)
        {
            var Kullanici = db.KullaniciBilgileriTablo.Include(p=>p.SehirTablo).FirstOrDefault(p => p.KullaniciId == KullaniciId);
            return Kullanici;
        }

        public String KullaniciBul(String Eposta, String Sifre)
        {
            var Kullanici = db.KullaniciBilgileriTablo.FirstOrDefault(p => p.KullaniciEPosta == Eposta
                                                                               && p.KullaniciSifre == Sifre
                                                                           &&p.BagisciMi!=true);
            if (Kullanici != null) return Kullanici.KullaniciId.ToString();
            else return String.Empty;
        }
        public String KullaniciBul(String Eposta)
        {
            var Kullanici = db.KullaniciBilgileriTablo.FirstOrDefault(p => p.KullaniciEPosta == Eposta);
            if (Kullanici != null) return Kullanici.KullaniciId.ToString();
            else return String.Empty;
        }
        public RotaTablo RotaBul(String ControllerAdi, String ActionAdi)
        {
            var Rota = db.RotaTablo.FirstOrDefault(p => p.ActionAdi == ActionAdi && p.ControllerAdi == ControllerAdi);
            return Rota;
        }

        public bool BagisciMi(int? kullaniciId)
        {
            if (db.KullaniciBilgileriTablo.FirstOrDefault(p => p.BagisciMi == true && p.KullaniciId == kullaniciId) !=
                null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool GirebilirMi(int? rotaId, int? kullaniciId)
        {
            if (db.YetkiTablo.FirstOrDefault(p => p.KullaniciBilgileriTablo_KullaniciId == kullaniciId
                                                  && p.RotaTablo_RotaId == rotaId
                                                  && p.GirebilirMi == true) != null) return true;
            else return false;
        }

        public YetkiTablo YetkiVarMi(RotaTablo rota, KullaniciBilgileriTablo kullanici)
        {
            var YetkiVarMi = db.YetkiTablo.FirstOrDefault(p =>
                p.KullaniciBilgileriTablo_KullaniciId == kullanici.KullaniciId
                && p.RotaTablo_RotaId == rota.RotaId
                && p.GirebilirMi == true);
            return YetkiVarMi;
        }

        public List<YetkiTablo> TumYetkileriGetir()
        {
            var Yetkiler = db.YetkiTablo.Include(b => b.KullaniciBilgileriTablo).Include(b => b.RotaTablo).ToList();
            return Yetkiler;
        }

        public List<RotaTablo> TumRotalariGetir()
        {
            return db.RotaTablo.ToList();
        }

        public List<RotaTablo> AltRotalariGetir(int? rotaId)
        {
            return db.RotaTablo.Where(p => p.RotaTablo_RotaId == rotaId).ToList();
        }

        public List<BagisTablo> TeslimAlinmaBekleyenBagislar(int? id)
        {
            if (id == null)
            {
                return db.BagisTablo.Include(p => p.KullaniciBilgileriTablo).Where(p=>p.TeslimAlindiMi==false).ToList();
            }
            else
            {
                if (KullaniciMerkezdeMi(id))
                {
                    var alinacaklar= db.BagisTablo.Include(p => p.KullaniciBilgileriTablo).Include(p=>p.BagisDetayTablo).Where(p => p.TeslimAlindiMi == false).ToList();
                    List< BagisTablo > alinacakBagisler=new List<BagisTablo>();
                    for (int i = 0; i < alinacaklar.Count; i++)
                    {
                        int? bagisId = alinacaklar[i].BagisId;
                        var alinacakTrueVarMi = db.BagisDetayTablo
                            .Where(p => p.BagisTablo_BagisId == bagisId && p.AlinacakMi == true).ToList();
                        if (alinacakTrueVarMi.Count > 0)
                        {
                            alinacakBagisler.Add(alinacaklar[i]);
                        }
                    }

                    return alinacakBagisler;
                }
                else
                {
                    int? kullaniciSehirId = KullaniciSehir(id);
                    var alinacaklar= db.BagisTablo.Include(p => p.KullaniciBilgileriTablo).Where(p => p.TeslimAlindiMi == false && p.KullaniciBilgileriTablo.SehirTablo_SehirId == kullaniciSehirId).ToList();
                    List<BagisTablo> alinacakBagisler = new List<BagisTablo>();
                    for (int i = 0; i < alinacaklar.Count; i++)
                    {
                        int? bagisId = alinacaklar[i].BagisId;
                        var alinacakTrueVarMi = db.BagisDetayTablo
                            .Where(p => p.BagisTablo_BagisId == bagisId && p.AlinacakMi == true).ToList();
                        if (alinacakTrueVarMi.Count > 0)
                        {
                            alinacakBagisler.Add(alinacaklar[i]);
                        }
                    }
                    return alinacakBagisler;
                }
                
            }
        }

        public List<IhtiyacSahibiKontrolTablo> TeslimBekleyenEsyalar(int? id)
        {
            if (id == null)
            {
                return db.IhtiyacSahibiKontrolTablo.Include(p => p.IhtiyacSahibiTablo)
                    .Include(p => p.IhtiyacSahibiVerilecekEsyaTablo).Where(p => p.MuhtacMi == true && p.TeslimTamamlandiMi == false).ToList();
            }
            else
            {
                if (KullaniciMerkezdeMi(id))
                {
                    return db.IhtiyacSahibiKontrolTablo.Include(p => p.IhtiyacSahibiTablo)
                        .Include(p => p.IhtiyacSahibiVerilecekEsyaTablo).Where(p => p.MuhtacMi == true && p.TeslimTamamlandiMi == false).ToList();
                }
                else
                {
                    int? kullaniciSehirId = KullaniciSehir(id);
                    return db.IhtiyacSahibiKontrolTablo.Include(p => p.IhtiyacSahibiTablo)
                        .Include(p => p.IhtiyacSahibiVerilecekEsyaTablo).Where(p => p.IhtiyacSahibiTablo.SehirTablo_SehirId == kullaniciSehirId && p.MuhtacMi == true && p.TeslimTamamlandiMi == false).ToList();
                }
            }
        }

        public List<IhtiyacSahibiKontrolTablo> OnayBekleyenIhtiyacSahipleri(int? id)
        {
            if (id == null)
            {
                return db.IhtiyacSahibiKontrolTablo.Include(p => p.IhtiyacSahibiTablo).Where(p => p.MuhtacMi == null ||p.MuhtacMi==false)
                    .ToList();
            }
            else
            {
                if (KullaniciMerkezdeMi(id))
                {
                    return db.IhtiyacSahibiKontrolTablo.Include(p => p.IhtiyacSahibiTablo).Where(p => p.MuhtacMi == null || p.MuhtacMi == false)
                        .ToList();
                }
                else
                {
                    int? kullaniciSehirId = KullaniciSehir(id);
                    return db.IhtiyacSahibiKontrolTablo.Include(p => p.IhtiyacSahibiTablo).Where(p => (p.MuhtacMi == null || p.MuhtacMi == false) && p.IhtiyacSahibiTablo.SehirTablo_SehirId == kullaniciSehirId)
                        .ToList();
                }
                
            }
        }

        public bool KullaniciAktifMi(int? id)
        {
            Dispose();
            var kullanici = db.KullaniciBilgileriTablo.FirstOrDefault(p => p.KullaniciId == id);
            if (kullanici != null)
            {
                if (Convert.ToBoolean(kullanici.AktifMi)) return true;
                else return false;
            }
            else
            {
                return false;
            }
        }

        public void Dispose()
        {
            db.Dispose();
            db = null;
            db = new SosyalYardimDB();
        }

        public bool KullaniciMerkezdeMi(int? id)
        {
            var kullanici = KullaniciBul(id);
            if (kullanici != null)
            {
                if (kullanici.KullaniciMerkezdeMi == true)
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

        public int? KullaniciSehir(int? id)
        {
            var kullanici = KullaniciBul(id);
            if (kullanici != null)
            {
                return kullanici.SehirTablo_SehirId;
            }
            else
            {
                return 0;
            }
        }
    }
}
