using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Models.AnaSayfaModelleri
{
    public class AnaSayfaOrtakModeli
    {
        public List<TeslimAlinmaBekleyenBagislarListe> TeslimAlinmaBekleyenEsyaListe { get; set; }

        public List<TeslimEdilecekEsyaListeModeli> TeslimEdilecekEsyaListesi { get; set; }

        public List<OnayBekleyenIhtiyacSahipleriModeli> OnayBekleyenMuhtacListesi { get; set; }
    }
}
