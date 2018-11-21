﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer.Models.KullaniciModelleri;
using BusinessLayer.Models.OrtakModeller;

namespace BusinessLayer.Models.SubeModelleri
{
    public class SubeModel
    {
        public int? SubeId { get; set; }
        public int? Sira { get; set; }
        public SehirModel Sehir { get; set; }

        public KullaniciModel Kullanici { get; set; }
    }
}
