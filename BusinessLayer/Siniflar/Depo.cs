using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer.Models;
using BusinessLayer.Models.EsyaModelleri;
using BusinessLayer.Models.OrtakModeller;

namespace BusinessLayer.Siniflar
{
    public class Depo
    {
        private DataLayer.Siniflar.Depo depoDAL = new DataLayer.Siniflar.Depo();
        public List<DepoModel> DepoGetir(int? KullaniciId)
        {
            List<DepoModel> depoListe= new List<DepoModel>();
            var depoEsyalari = depoDAL.DepoGetir(KullaniciId);
            for (int i = 0; i < depoEsyalari.Count; i++)
            {
                DepoModel gonderilecekDepo = new DepoModel();
                gonderilecekDepo.Adet = depoEsyalari[i].Adet;
                gonderilecekDepo.DepoEsyaId = depoEsyalari[i].DepoEsyaId;
                gonderilecekDepo.EsyaId = depoEsyalari[i].EsyaTablo.EsyaId;
                gonderilecekDepo.EsyaAdi = depoEsyalari[i].EsyaTablo.EsyaAdi;
                gonderilecekDepo.Sehir= new SehirModel()
                {
                    SehirAdi=depoEsyalari[i].SehirTablo.SehirAdi,
                    SehirId=depoEsyalari[i].SehirTablo_SehirId
                };
                depoListe.Add(gonderilecekDepo);
            }

            return depoListe.OrderBy(p => p.Sehir).ToList();
        }

    }
}
