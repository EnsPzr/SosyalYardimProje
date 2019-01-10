using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
namespace DataLayer.Siniflar
{
    public class Log
    {
        private SosyalYardimDB db = new SosyalYardimDB();
        private KullaniciYonetimi kullaniciDAL = new KullaniciYonetimi();
        public bool Kaydet(LogTablo log)
        {
            if (log.IslemIcerik != null)
            {
                System.Globalization.CultureInfo cultureInfo = System.Threading.Thread.CurrentThread.CurrentCulture;
                System.Globalization.TextInfo textInfo = cultureInfo.TextInfo;
                log.IslemIcerik = textInfo.ToTitleCase(log.IslemIcerik);
            }
            db.LogTablo.Add(log);
            if (db.SaveChanges() > 0)
            {
                return true;
            }

            return false;
        }

        public List<LogTablo> TumLoglariGetir(int? kullaniciId)
        {
            if (kullaniciDAL.KullaniciMerkezdeMi(kullaniciId))
            {
                return db.LogTablo.Include(p => p.KullaniciBilgileriTablo).
                    OrderByDescending(p => p.IslemTarihi).ToList();
            }
            else
            {
                int? sehirId = kullaniciDAL.KullaniciSehir(kullaniciId);
                return db.LogTablo.Include(p => p.KullaniciBilgileriTablo)
                    .Where(p => p.KullaniciBilgileriTablo.SehirTablo_SehirId == sehirId).
                    OrderByDescending(p => p.IslemTarihi).ToList();
            }
        }
    }
}
