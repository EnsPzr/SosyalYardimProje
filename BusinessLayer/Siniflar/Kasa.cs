using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer.Models.KasaModelleri;
using BusinessLayer.Models.OrtakModeller;

namespace BusinessLayer.Siniflar
{
    public class Kasa
    {
        private DataLayer.Siniflar.Kasa kasaDAL = new DataLayer.Siniflar.Kasa();
        public List<KasaModel> TumKasaGetir(int? kullaniciId)
        {
            var kasa = kasaDAL.TumKasaGetir(kullaniciId);
            if (kasa.Count == 0)
            {
                return new List<KasaModel>();
            }
            else
            {
                var kasaList = kasa.Select(p => new KasaModel()
                {
                    Aciklama = p.Aciklama,
                    KasaId = p.KasaId,
                    KullaniciAdiSoyadi = p.KullaniciBilgileriTablo.KullaniciAdi + " " +
                                           p.KullaniciBilgileriTablo.KullaniciSoyadi,
                    KullaniciId = p.KullaniciBilgleriTablo_KullaniciId,
                    Miktar = Convert.ToDouble(p.Miktar),
                    Sehir = new SehirModel()
                    {
                        SehirAdi = p.SehirTablo.SehirAdi,
                        SehirId = p.SehirTablo_SehirId
                    },
                    Tarih = p.Tarih
                }).ToList();
                return kasaList;
            }
        }

        public List<KasaModel> FiltreliKasaGetir(int? kullaniciId, string aranan, string tarih, int? sehirId, int? gelirGider)
        {
            var kasa = kasaDAL.FiltreliKasaGetir(kullaniciId, aranan, tarih, sehirId, gelirGider);
            if (kasa.Count == 0)
            {
                return new List<KasaModel>();
            }
            else
            {
                var kasaList = kasa.Select(p => new KasaModel()
                {
                    Aciklama = p.Aciklama,
                    KasaId = p.KasaId,
                    KullaniciAdiSoyadi = p.KullaniciBilgileriTablo.KullaniciAdi + " " +
                                         p.KullaniciBilgileriTablo.KullaniciSoyadi,
                    KullaniciId = p.KullaniciBilgleriTablo_KullaniciId,
                    Miktar = Convert.ToDouble(p.Miktar),
                    Sehir = new SehirModel()
                    {
                        SehirAdi = p.SehirTablo.SehirAdi,
                        SehirId = p.SehirTablo_SehirId
                    },
                    Tarih = p.Tarih
                }).ToList();
                return kasaList;
            }
        }
    }
}
