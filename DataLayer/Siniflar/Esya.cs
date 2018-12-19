using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Siniflar
{
    public class Esya
    {
        private SosyalYardimDB db = new SosyalYardimDB();
        public List<EsyaTablo> TumEsyalariGetir()
        {
            return db.EsyaTablo.ToList();
        }

        public List<EsyaTablo> FiltreliEsyalariGetir(String aranan)
        {
            return db.EsyaTablo.Where(p => p.EsyaAdi.Contains(aranan)).ToList();
        }

        public bool Ekle(EsyaTablo eklenecekEsya)
        {
            db.EsyaTablo.Add(eklenecekEsya);
            if (db.SaveChanges() > 0)
            {
                return true;
            }

            return false;
        }

        public bool EsyaVarMi(String esyaAdi)
        {
            if (db.EsyaTablo.FirstOrDefault(p => p.EsyaAdi.Trim().ToLower().Equals(esyaAdi.Trim().ToLower())) != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
