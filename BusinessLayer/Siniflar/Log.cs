using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer.Models.LogModelleri;
using DataLayer;

namespace BusinessLayer.Siniflar
{
    public class Log
    {
        private DataLayer.Siniflar.Log logDAL = new DataLayer.Siniflar.Log();

        public void Kaydet(LogModel model)
        {
            LogTablo logTablo = new LogTablo();
            logTablo.IslemIcerik = model.IslemIcerik;
            logTablo.IslemTarihi=DateTime.Now;
            logTablo.IslemTipi = Convert.ToByte(model.IslemTipi);
            logTablo.KullaniciBilgileriTablo_KullaniciId = model.KullaniciId;
            logDAL.Kaydet(logTablo);
        }

        public List<LogModel> TumLoglariGetir(int? kullaniciId)
        {
            var loglar = logDAL.TumLoglariGetir(kullaniciId);
            List<LogModel> listModel = new List<LogModel>();
            for (int i = 0; i < loglar.Count; i++)
            {
                var log = new LogModel();
                log.IslemIcerik = loglar[i].IslemIcerik;
                if (loglar[i].IslemTipi != null)
                {
                    log.IslemTipiStr = IslemTipleri().Where(p => p.Key == loglar[i].IslemTipi).FirstOrDefault().Value;
                }

                log.KullaniciAdiSoyadi = loglar[i].KullaniciBilgileriTablo.KullaniciAdi + " " +
                                         loglar[i].KullaniciBilgileriTablo.KullaniciSoyadi;
                log.Tarih = loglar[i].IslemTarihi;
                listModel.Add(log);
            }

            return listModel;
        }

        public Dictionary<int,string> IslemTipleri()
        {
            Dictionary<int, string> islemTipleri = new Dictionary<int, string>();
            islemTipleri.Add(0, "Listeleme");
            islemTipleri.Add(1, "Ekleme");
            islemTipleri.Add(2, "Silme");
            islemTipleri.Add(3, "Güncelleme");
            islemTipleri.Add(4, "Detay Görüntüleme");
            islemTipleri.Add(5, "Kayıt");
            islemTipleri.Add(6, "Giriş");
            islemTipleri.Add(7, "Hata");
            return islemTipleri;
        }
    }
}
