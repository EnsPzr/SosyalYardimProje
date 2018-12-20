using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Models.IhtiyacSahibiModelleri
{
    public class IhtiyacSahibiVerileceklerModel
    {
        public int? EsyaId { get; set; }

        public String EsyaAdi { get; set; }

        public int Adet { get; set; }
    }
}
