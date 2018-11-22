using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer.Models.KullaniciModelleri;
using BusinessLayer.Models.OrtakModeller;
using DataLayer;
namespace BusinessLayer.Siniflar
{
    public class Yetki
    {
        private DataLayer.Siniflar.Yetki yetkiDAL = new DataLayer.Siniflar.Yetki();
        private Kullanici kullaniciBAL = new Kullanici();

        public List<KullaniciModel> KullanicilariGetir(int? id)
        {
            return KullanicilariGetir(id);
        }
    }
}
