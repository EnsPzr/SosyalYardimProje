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

        public EsyaTablo EsyaGetir(int? id)
        {
            return db.EsyaTablo.FirstOrDefault(p => p.EsyaId == id);
        }

        public bool EsyaDuzenle(EsyaTablo guncelEsya)
        {
            var esya = db.EsyaTablo.FirstOrDefault(p => p.EsyaId == guncelEsya.EsyaId);
            if (esya != null)
            {
                if (esya.EsyaAdi.Trim().ToLower().Equals(guncelEsya.EsyaAdi.Trim().ToLower()))
                {
                    return true;
                }
                else
                {
                    esya.EsyaAdi = guncelEsya.EsyaAdi;
                    if (db.SaveChanges() > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }

            return false;
        }

        public bool EsyaVarMi(EsyaTablo esya)
        {
            if (db.EsyaTablo.FirstOrDefault(p => p.EsyaId != esya.EsyaId && p.EsyaAdi.Trim().ToLower() == esya.EsyaAdi.Trim().ToLower()) != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool EsyaSil(int? id)
        {
            var esya = EsyaGetir(id);
            if (esya != null)
            {
                db.EsyaTablo.Remove(esya);
                if (db.SaveChanges() > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return false;
        }
    }
}
