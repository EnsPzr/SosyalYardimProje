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
    }
}
