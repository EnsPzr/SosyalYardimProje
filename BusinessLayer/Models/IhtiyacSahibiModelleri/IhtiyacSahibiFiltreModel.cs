using System;
using BusinessLayer.Models.OrtakModeller;

namespace BusinessLayer.Models.IhtiyacSahibiModelleri
{
    public class IhtiyacSahibiFiltreModel
    {
        public String IhtiyacSahibiAranan { get; set; }

        public SehirModel Sehir { get; set; }

    }
}
