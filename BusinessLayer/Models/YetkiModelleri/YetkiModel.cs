using System;
using System.Collections.Generic;
namespace BusinessLayer.Models.YetkiModelleri
{
    public class YetkiModel
    {

        public int? YetkiId { get; set; }

        public int? RotaId { get; set; }
        
        public bool? GirebilirMi { get; set; }

        public KullaniciModelleri.KullaniciModel Kullanici { get; set; }

        public String RotaAdi { get; set; }

    }
}
