using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer.Models.EsyaModelleri;
using DataLayer.Siniflar;
namespace BusinessLayer.Siniflar
{
    public class Esya
    {
        private DataLayer.Siniflar.Esya esyaDAL = new DataLayer.Siniflar.Esya();

        public List<EsyaModel> TumEsyalariGetir()
        {
            var esyalar = esyaDAL.TumEsyalariGetir().Select(p=>new EsyaModel()
            {
                EsyaAdi=p.EsyaAdi,
                EsyaId = p.EsyaId
            }).ToList();
            return esyalar;
        }
    }
}
