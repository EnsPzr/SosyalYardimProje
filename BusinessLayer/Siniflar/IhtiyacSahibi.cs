using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer.Models.IhtiyacSahibiModelleri;
using DataLayer.Siniflar;
namespace BusinessLayer.Siniflar
{
    public class IhtiyacSahibi
    {
        private IhtiyacSahibi ihtiyacSahibiDAL = new IhtiyacSahibi();

        public List<IhtiyacSahibiModel> TumIhtiyacSahipleriniGetir(int? KullaniciId)
        {
            var ihtiyacSahipleri = ihtiyacSahibiDAL.TumIhtiyacSahipleriniGetir(KullaniciId);
            var dondurulecekIhtiyacSahipleri=new List<IhtiyacSahibiModel>();
            for (int i = 0; i < ihtiyacSahipleri.Count; i++)
            {
                IhtiyacSahibiModel eklenecekIhtiyacSahibi= new IhtiyacSahibiModel()
                {
                    IhtiyacSahibiAciklama=ihtiyacSahipleri[i].IhtiyacSahibiAciklama,
                    IhtiyacSahibiAdi=ihtiyacSahipleri[i].IhtiyacSahibiAdi,
                    IhtiyacSahibiAdres=ihtiyacSahipleri[i].IhtiyacSahibiAdres,
                    IhtiyacSahibiId=ihtiyacSahipleri[i].IhtiyacSahibiId,
                    IhtiyacSahibiSoyadi=ihtiyacSahipleri[i].IhtiyacSahibiSoyadi,
                    IhtiyacSahibiTelNo=ihtiyacSahipleri[i].IhtiyacSahibiTelNo,
                    Sehir= new Models.OrtakModeller.SehirModel()
                    {
                        SehirAdi=ihtiyacSahipleri[i].Sehir.SehirAdi,
                        SehirId=ihtiyacSahipleri[i].Sehir.SehirId
                    }
                };
                dondurulecekIhtiyacSahipleri.Add(eklenecekIhtiyacSahibi);
            }

            return dondurulecekIhtiyacSahipleri;
        }
    }
}
