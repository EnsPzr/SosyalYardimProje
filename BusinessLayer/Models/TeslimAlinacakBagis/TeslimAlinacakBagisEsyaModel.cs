﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Models.TeslimAlinacakBagis
{
    public class TeslimAlinacakBagisEsyaModel
    {
        public int? BagisDetayId { get; set; }

        [Display(Name = "Eşya Adı")]
        public String EsyaAdi { get; set; }

        public int? Adet { get; set; }

        public bool AlinacakMi { get; set; }

        public bool AlindiMi { get; set; }

        public List<TeslimAlinacakBagisResimModel> resimModel { get; set; }
        public TeslimAlinacakBagisEsyaModel()
        {
            resimModel = new List<TeslimAlinacakBagisResimModel>();
        }
    }
}