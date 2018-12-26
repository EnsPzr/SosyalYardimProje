using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer.Models;
using BusinessLayer.Models.AnaSayfaModelleri;
using BusinessLayer.Models.KullaniciModelleri;
using DataLayer;
namespace BusinessLayer
{
    public class KullaniciYonetimi
    {
        DataLayer.KullaniciYonetimi kullaniciYonetimi = new DataLayer.KullaniciYonetimi();
        public KullaniciBilgileriTablo LoginKullaniciBul(int? KullaniciId)
        {
            return kullaniciYonetimi.KullaniciBul(KullaniciId);
        }
        public KullaniciModel LoginKullaniciModelBul(int? KullaniciId)
        {
            var kullanici = kullaniciYonetimi.KullaniciBul(KullaniciId);
            if (kullanici != null)
            {
                var donKullanici = new KullaniciModel();
                donKullanici.KullaniciId = kullanici.KullaniciId;
                donKullanici.AktifMi = kullanici.AktifMi;
                return donKullanici;
            }
            else
            {
                return null;
            }
        }
        public bool YetkiVarMi(int? KullaniciId, String Controller, String Action)
        {
            var Kullanici = kullaniciYonetimi.KullaniciBul(KullaniciId);
            if (Kullanici != null)
            {
                var Rota = kullaniciYonetimi.RotaBul(Controller, Action);
                if (Rota != null)
                {
                    if (Rota.HerkesGirebilirMi == true)
                    {
                        return true;
                    }
                    else
                    {
                        var kullanicininYetkisiVarMi = kullaniciYonetimi.YetkiVarMi(Rota, Kullanici);
                        if (kullanicininYetkisiVarMi != null)
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
            else
            {
                return false;
            }
        }

        public bool BagisciMi(int? kullaniciId)
        {
            return kullaniciYonetimi.BagisciMi(kullaniciId);
        }

        public List<NavbarModel> NavbarOlustur(int? KullaniciId)
        {
            var Rotalar = kullaniciYonetimi.TumRotalariGetir().OrderBy(p => p.Sira).ToList();
            List<NavbarModel> navbarListModel = new List<NavbarModel>();
            for (int i = 0; i < Rotalar.Count; i++)
            {
                if (Rotalar[i].HerkesGirebilirMi == true && Rotalar[i].GosterilecekMi == true)
                {
                    var eklenecek = new NavbarModel();
                    if (Rotalar[i].RotaTablo_RotaId == null)
                    {
                        eklenecek.AltKategoriMi = false;
                        eklenecek.UrlText = Rotalar[i].LinkAdi;
                        eklenecek.UrlYol = Rotalar[i].ControllerAdi + "/" + Rotalar[i].ActionAdi;
                        eklenecek.UrlClass = Rotalar[i].LinkClass;
                        var altKategoriler = kullaniciYonetimi.AltRotalariGetir(Rotalar[i].RotaId);
                        if (altKategoriler.Count > 0)
                        {
                            eklenecek.AltKategoriSayisi = altKategoriler.Count;
                            eklenecek.DropDownBaslik = Rotalar[i].DropdownBaslikAdi;
                            eklenecek.AltKategoriler = new List<NavbarModel>();
                            for (int j = 0; j < altKategoriler.Count; j++)
                            {
                                var eklenecekAltKategori = new NavbarModel();
                                eklenecekAltKategori.AltKategoriMi = true;
                                eklenecekAltKategori.UrlText = altKategoriler[j].LinkAdi;
                                eklenecekAltKategori.UrlYol =
                                    altKategoriler[j].ControllerAdi + "/" + altKategoriler[j].ActionAdi;
                                eklenecek.AltKategoriler.Add(eklenecekAltKategori);
                            }
                            navbarListModel.Add(eklenecek);
                        }
                        else
                        {
                            navbarListModel.Add(eklenecek);
                        }
                    }
                }
                else
                {
                    if (kullaniciYonetimi.GirebilirMi(Rotalar[i].RotaId, KullaniciId))
                    {
                        if (Rotalar[i].GosterilecekMi == true)
                        {
                            var eklenecek = new NavbarModel();
                            if (Rotalar[i].RotaTablo_RotaId == null)
                            {
                                eklenecek.AltKategoriMi = false;
                                eklenecek.UrlText = Rotalar[i].LinkAdi;
                                eklenecek.UrlYol = Rotalar[i].ControllerAdi + "/" + Rotalar[i].ActionAdi;
                                eklenecek.UrlClass = Rotalar[i].LinkClass;
                                var altKategoriler = kullaniciYonetimi.AltRotalariGetir(Rotalar[i].RotaId);
                                if (altKategoriler.Count > 0)
                                {
                                    eklenecek.AltKategoriSayisi = altKategoriler.Count;
                                    eklenecek.DropDownBaslik = Rotalar[i].DropdownBaslikAdi;
                                    eklenecek.AltKategoriler = new List<NavbarModel>();
                                    for (int j = 0; j < altKategoriler.Count; j++)
                                    {
                                        var eklenecekAltKategori = new NavbarModel();
                                        eklenecekAltKategori.AltKategoriMi = true;
                                        eklenecekAltKategori.UrlText = altKategoriler[j].LinkAdi;
                                        eklenecekAltKategori.UrlYol =
                                            altKategoriler[j].ControllerAdi + "/" + altKategoriler[j].ActionAdi;
                                        eklenecek.AltKategoriler.Add(eklenecekAltKategori);
                                    }
                                    navbarListModel.Add(eklenecek);
                                }
                                else
                                {
                                    navbarListModel.Add(eklenecek);
                                }
                            }
                        }
                    }
                }
            }

            return navbarListModel;


            //List<YetkiTablo> Yetkiler = kullaniciYonetimi.TumYetkileriGetir();


            //Yetkiler = Yetkiler.Where(p => p.KullaniciBilgileriTablo_KullaniciId == KullaniciId
            //                               && p.GirebilirMi == true).OrderBy(p=>p.RotaTablo.Sira).ToList();
            //List<NavbarModel> navbarListModel = new List<NavbarModel>();
            //for (int i = 0; i < Yetkiler.Count; i++)
            //{
            //    NavbarModel navbar = new NavbarModel();
            //    if (Yetkiler[i].RotaTablo.RotaTablo_RotaId == null)
            //    {
            //        if (Yetkiler[i].GirebilirMi == true && Yetkiler[i].RotaTablo.GosterilecekMi == true)
            //        {
            //            var altKategoriler = Yetkiler
            //                .Where(p => p.RotaTablo.RotaTablo_RotaId == Yetkiler[i].RotaTablo.RotaId).ToList();
            //            if (altKategoriler.Count == 0)
            //            {
            //                navbar.AltKategoriMi = false;
            //                navbar.UrlText = Yetkiler[i].RotaTablo.LinkAdi;
            //                navbar.UrlYol = Yetkiler[i].RotaTablo.ControllerAdi + "/" + Yetkiler[i].RotaTablo.ActionAdi;
            //                navbarListModel.Add(navbar);
            //            }
            //            else
            //            {
            //                navbar.AltKategoriMi = false;
            //                navbar.AltKategoriSayisi = altKategoriler.Count;
            //                navbar.UrlText = Yetkiler[i].RotaTablo.LinkAdi;
            //                navbar.DropDownBaslik = Yetkiler[i].RotaTablo.DropdownBaslikAdi;
            //                navbar.UrlYol = Yetkiler[i].RotaTablo.ControllerAdi + "/" + Yetkiler[i].RotaTablo.ActionAdi;
            //                navbar.altKategoriler = new List<NavbarModel>();
            //                for (int j = 0; j < altKategoriler.Count; j++)
            //                {
            //                    if (altKategoriler[j].GirebilirMi == true && altKategoriler[j].RotaTablo.GosterilecekMi == true)
            //                    {
            //                        NavbarModel altKategoriNavbar = new NavbarModel();
            //                        altKategoriNavbar.AltKategoriMi = true;
            //                        altKategoriNavbar.UrlText = altKategoriler[j].RotaTablo.LinkAdi;
            //                        altKategoriNavbar.UrlYol = altKategoriler[j].RotaTablo.ControllerAdi + "/" + altKategoriler[j].RotaTablo.ActionAdi;
            //                        navbar.altKategoriler.Add(altKategoriNavbar);
            //                    }
            //                }
            //                navbarListModel.Add(navbar);
            //            }





            //            //navbar.AltKategoriMi = false;
            //            //navbar.UrlText = Yetkiler[i].RotaTablo.LinkAdi;
            //            //navbar.UrlYol = Yetkiler[i].RotaTablo.ControllerAdi + "/" + Yetkiler[i].RotaTablo.ActionAdi;

            //            //navbar.AltKategoriSayisi = altKategoriler.Count;
            //            //navbarListModel.Add(navbar);

            //            //for (int j = 0; j < altKategoriler.Count; j++)
            //            //{
            //            //    if (altKategoriler[j].GirebilirMi==true && altKategoriler[j].RotaTablo.GosterilecekMi==true)
            //            //    {
            //            //        NavbarModel altKategoriNavbar = new NavbarModel();
            //            //        altKategoriNavbar.AltKategoriMi = true;
            //            //        altKategoriNavbar.UrlText = altKategoriler[j].RotaTablo.LinkAdi;
            //            //        altKategoriNavbar.UrlYol = altKategoriler[j].RotaTablo.ControllerAdi + "/" + altKategoriler[j].RotaTablo.ActionAdi;
            //            //        navbarListModel.Add(altKategoriNavbar);
            //            //    }
            //            //}
            //        }
            //    }
            //}
            //return navbarListModel;
        }

        public String KullaniciBul(String EMail, String Sifre)
        {
            return kullaniciYonetimi.KullaniciBul(EMail, Sifre);
        }
        public String KullaniciBul(String EMail)
        {
            return kullaniciYonetimi.KullaniciBul(EMail);
        }

        public List<TeslimAlinmaBekleyenBagislarListe> TeslimAlinmaBekleyenBagislar(int? id)
        {
            var teslimAlinmaBekleyenBagislar = kullaniciYonetimi.TeslimAlinmaBekleyenBagislar(id).Select(p =>
                new TeslimAlinmaBekleyenBagislarListe()
                {
                    BagisciAdiSoyadi = p.KullaniciBilgileriTablo.KullaniciAdi + " " + p.KullaniciBilgileriTablo.KullaniciSoyadi,
                    Tarih = p.EklenmeTarihi
                }).ToList();
            return teslimAlinmaBekleyenBagislar;
        }

        public List<TeslimEdilecekEsyaListeModeli> TeslimBekleyenBagislar(int? id)
        {
            var teslimBekleyenlerListe = kullaniciYonetimi.TeslimBekleyenEsyalar(id).Select(p =>
                new TeslimEdilecekEsyaListeModeli()
                {
                    MuhtacAdiSoyadi = p.IhtiyacSahibiTablo.IhtiyacSahibiAdi + " " +
                                      p.IhtiyacSahibiTablo.IhtiyacSahibiSoyadi,
                    TahminiTeslimTarihi = p.TahminiTeslimTarihi
                }).ToList();
            return teslimBekleyenlerListe;
        }

        public List<OnayBekleyenIhtiyacSahipleriModeli> OnayBekleyenIhtiyacSahipleri(int? id)
        {
            var onayBekleyenIhtiyacSahipleri = kullaniciYonetimi.OnayBekleyenIhtiyacSahipleri(id).Select(p =>
                new OnayBekleyenIhtiyacSahipleriModeli()
                {
                    MuhtacAdiSoyadi = p.IhtiyacSahibiTablo.IhtiyacSahibiAdi + " " +
                                      p.IhtiyacSahibiTablo.IhtiyacSahibiSoyadi,
                    Tarih = p.Tarih
                }).ToList();
            return onayBekleyenIhtiyacSahipleri;
        }

        public AnaSayfaOrtakModeli AnaSayfaModeli(int? id)
        {
            AnaSayfaOrtakModeli anaSayfaModel = new AnaSayfaOrtakModeli()
            {
                OnayBekleyenMuhtacListesi = OnayBekleyenIhtiyacSahipleri(id),
                TeslimAlinmaBekleyenEsyaListe = TeslimAlinmaBekleyenBagislar(id),
                TeslimEdilecekEsyaListesi = TeslimBekleyenBagislar(id)
            };
            return anaSayfaModel;
        }

        public bool KullaniciAktifMi(int? id)
        {
            return kullaniciYonetimi.KullaniciAktifMi(id);
        }

        public bool KullaniciMerkezdeMi(int? id)
        {
            return kullaniciYonetimi.KullaniciMerkezdeMi(id);
        }

        public int? KullaniciSehirGetir(int? id)
        {
            return kullaniciYonetimi.KullaniciSehir(id);
        }
    }
}
