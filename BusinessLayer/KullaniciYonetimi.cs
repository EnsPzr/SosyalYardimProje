using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer.Models;
using DataLayer;
namespace BusinessLayer
{
    public class KullaniciYonetimi
    {
        DataLayer.KullaniciYonetimi kullaniciYonetimi = new DataLayer.KullaniciYonetimi();
        public DataLayer.KullaniciBilgileriTablo LoginKullaniciBul(String KullaniciGuId)
        {
            return kullaniciYonetimi.KullaniciBul(KullaniciGuId);
        }
        public bool YetkiVarMi(String KullaniciGuId, String Controller, String Action)
        {
            var Kullanici = kullaniciYonetimi.KullaniciBul(KullaniciGuId);
            if (Kullanici != null)
            {
                var Rota = kullaniciYonetimi.RotaBul(Controller,Action);
                if (Rota != null)
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

        public List<NavbarModel> NavbarOlustur(String KullaniciGuId)
        {
            List<DataLayer.YetkiTablo> Yetkiler = kullaniciYonetimi.TumYetkileriGetir();
            Yetkiler = Yetkiler.Where(p => p.KullaniciBilgileriTablo_KullaniciGuId == KullaniciGuId).ToList();
            List<NavbarModel> navbarListModel = new List<NavbarModel>();
            for (int i = 0; i < Yetkiler.Count; i++)
            {
                NavbarModel navbar = new NavbarModel();
                if (Yetkiler[i].RotaTablo.RotaTablo_RotaId == null)
                {
                    if (Yetkiler[i].GirebilirMi == true && Yetkiler[i].RotaTablo.GosterilecekMi == true)
                    {
                        navbar.AltKategoriMi = false;
                        navbar.UrlText = Yetkiler[i].RotaTablo.LinkAdi;
                        navbar.UrlYol = Yetkiler[i].RotaTablo.ControllerAdi + "/" + Yetkiler[i].RotaTablo.ActionAdi;
                        navbarListModel.Add(navbar);
                        var altKategoriler = Yetkiler
                            .Where(p => p.RotaTablo.RotaTablo_RotaId == Yetkiler[i].RotaTablo.RotaId).ToList();
                        for (int j = 0; j < altKategoriler.Count; j++)
                        {
                            if (altKategoriler[j].GirebilirMi==true && altKategoriler[j].RotaTablo.GosterilecekMi==true)
                            {
                                NavbarModel altKategoriNavbar = new NavbarModel();
                                altKategoriNavbar.AltKategoriMi = true;
                                altKategoriNavbar.UrlText = altKategoriler[j].RotaTablo.LinkAdi;
                                altKategoriNavbar.UrlYol = altKategoriler[j].RotaTablo.ControllerAdi + "/" + altKategoriler[j].RotaTablo.ActionAdi;
                                navbarListModel.Add(altKategoriNavbar);
                            }
                        }
                    }
                }
            }
            return navbarListModel;
        }
    }
}
