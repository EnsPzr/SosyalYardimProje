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
    }
}
