using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer.Models.KullaniciModelleri;

namespace BusinessLayer.BagisciSiniflar
{
    public class BagisciYonetimi
    {
        private DataLayer.BagisciSiniflar.BagisciYonetimi bagisciDAL = new DataLayer.BagisciSiniflar.BagisciYonetimi();

        public KullaniciModel BagisciBul(String ePosta, String sifre)
        {
            var bagisci= bagisciDAL.BagisciBul(ePosta, sifre);
            if (bagisci != null)
            {
                var bagisModel=new KullaniciModel()
                {
                    KullaniciId=bagisci.KullaniciId,
                    KullaniciAdi=bagisci.KullaniciAdi,
                    KullaniciSoyadi = bagisci.KullaniciSoyadi
                };
                return bagisModel;
            }
            else
            {
                return null;
            }
        }
    }
}
