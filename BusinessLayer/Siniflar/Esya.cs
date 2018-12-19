using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer.Models.EsyaModelleri;
using BusinessLayer.Models.OrtakModeller;
using DataLayer;
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

        public List<EsyaModel> FiltreliEsyalariGetir(String aranan)
        {
            var esyalar = esyaDAL.FiltreliEsyalariGetir(aranan).Select(p => new EsyaModel()
            {
                EsyaAdi = p.EsyaAdi,
                EsyaId = p.EsyaId
            }).ToList();
            return esyalar;
        }

        public IslemOnayModel Ekle(EsyaModel eklenecekEsya)
        {
            IslemOnayModel onay = new IslemOnayModel();
            if (esyaDAL.EsyaVarMi(eklenecekEsya.EsyaAdi))
            {
                onay.TamamlandiMi = false;
                onay.HataMesajlari.Add("Eklenmek istenen eşya sistemde bulunuyor.");
                return onay;
            }
            else
            {
                EsyaTablo esyaTabloEklenecek = new EsyaTablo()
                {
                    EsyaAdi=eklenecekEsya.EsyaAdi
                };
                if (esyaDAL.Ekle(esyaTabloEklenecek))
                {
                    onay.TamamlandiMi = true;
                    return onay;
                }
                else
                {
                    onay.HataMesajlari.Add("Bilinmeyen bir hata oluştu.");
                    onay.TamamlandiMi = false;
                    return onay;
                }
            }
        }
    }
}
