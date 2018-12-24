using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace BusinessLayer.Models.TeslimAlinacakBagis
{
    public class TeslimAlinacakBagisResimModel
    {
        public int? ResimId { get; set; }

        [DataType(DataType.Upload)]
        public String ResimYol { get; set; }

        public int? ResimId2 { get; set; }
        [DataType(DataType.Upload)]
        public String ResimYol2 { get; set; }

        public int? ResimId3 { get; set; }
        [DataType(DataType.Upload)]
        public String ResimYol3 { get; set; }
        

    }
}
