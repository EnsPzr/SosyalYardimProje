using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Siniflar
{
    public class Log
    {
        private SosyalYardimDB db = new SosyalYardimDB();

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
    }
}
