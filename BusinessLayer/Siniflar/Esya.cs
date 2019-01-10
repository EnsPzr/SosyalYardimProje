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
            var esyalar = esyaDAL.TumEsyalariGetir().Select(p => new EsyaModel()
            {
                EsyaAdi = p.EsyaAdi,
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
                    EsyaAdi = eklenecekEsya.EsyaAdi
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

        public EsyaModel EsyaGetir(int? id)
        {
            var esya = esyaDAL.EsyaGetir(id);
            if (esya == null)
            {
                return null;
            }
            else
            {
                EsyaModel gosterilecekEsya = new EsyaModel()
                {
                    EsyaAdi = esya.EsyaAdi,
                    EsyaId = esya.EsyaId
                };
                return gosterilecekEsya;
            }
        }

        public IslemOnayModel EsyaDuzenle(EsyaModel duzenlenmisEsya, int? kullaniciId)
        {
            IslemOnayModel onay = new IslemOnayModel();
            EsyaTablo esya = new EsyaTablo()
            {
                EsyaId = Convert.ToInt32(duzenlenmisEsya.EsyaId),
                EsyaAdi = duzenlenmisEsya.EsyaAdi
            };
            if (esyaDAL.EsyaVarMi(esya))
            {
                onay.HataMesajlari.Add("Sistemde aynı isimde bir başka eşya kayıtlıdır.");
                onay.TamamlandiMi = false;
            }
            else
            {
                if (esyaDAL.EsyaDuzenle(esya,kullaniciId))
                {
                    onay.TamamlandiMi = true;
                }
                else
                {
                    onay.TamamlandiMi = false;
                    onay.HataMesajlari.Add("Güncelleme işlemi sırasında hata oluştu. Yetkiniz olmadığından bu hatayı görüyor olabilirsiniz.");
                }
            }

            return onay;
        }

        public IslemOnayModel EsyaSil(int? id,int? kullaniciId)
        {
            IslemOnayModel onay = new IslemOnayModel();
            var esya = esyaDAL.EsyaGetir(id);
            if (esya != null)
            {
                if (esyaDAL.EsyaSil(id, kullaniciId))
                {
                    onay.TamamlandiMi = true;
                }
                else
                {
                    onay.TamamlandiMi = false;
                    onay.HataMesajlari.Add("Silme işlemi tamamlanamadı. Yetkiniz olmadığından dolayı silme işlemi durduruldu.");
                }
            }
            else
            {
                onay.TamamlandiMi = false;
                onay.HataMesajlari.Add("Silmek istediğiniz eşya bulunmadı.");
            }

            return onay;
        }
    }
}
