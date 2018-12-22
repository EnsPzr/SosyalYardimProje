using BusinessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Siniflar
{
    public class GeriBildirim
    {
        DataLayer.Siniflar.GeriBildirim geriBildirimDAL = new DataLayer.Siniflar.GeriBildirim();

        public List<GeriBildirimModel> TumGeriBildirimleriGetir(int? kullaniciId)
        {
            var geriBildirimler = geriBildirimDAL.TumGeriBildirimleriGetir(kullaniciId);
            var donGeriBildirimler = geriBildirimler.Select(p => new GeriBildirimModel()
            {
                DurumInt=p.GeriBildirimDurumu,
                DurumStr=p.GeriBildirimDurumu==0?"Okunmadı": p.GeriBildirimDurumu == 1?"Okundu": p.GeriBildirimDurumu == 2?"Geri Dönüş Yapıldı":"Geri Dönüşe Gerek Görülmedi",
                GeriBildirimId=p.GeriBildirimId,
                Konu=p.GeriBildirimKonu,
                KullaniciAdiSoyadi=p.KullaniciBilgileriTablo.KullaniciAdi+" "+p.KullaniciBilgileriTablo.KullaniciSoyadi,
                KullaniciTel=p.KullaniciBilgileriTablo.KullaniciTelefonNumarasi,
                Mesaj=p.GeriBildirimMesaj,
                Tarih=p.Tarih
            }).ToList();
            return donGeriBildirimler;
        }
    }
}
