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
    }
}
