using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    }
}
