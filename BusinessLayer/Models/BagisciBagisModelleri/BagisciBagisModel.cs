using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace BusinessLayer.Models.BagisciBagisModelleri
{
    public class BagisciBagisModel
    {
        public int? BagisId { get; set; }

        [Display(Name = "Eşya")]
        public int? EsyaId { get; set; }
        
        [Display(Name = "Adet")]
        public int? Adet { get; set; }

        [Display(Name = "Resim 1")]
        [Required(ErrorMessage = "En az 1 resim zorunludur.")]
        public HttpPostedFileBase Resim1_data { get; set; }
        [Display(Name = "Resim 2")]
        public HttpPostedFileBase Resim2_data { get; set; }
        [Display(Name = "Resim 3")]
        public HttpPostedFileBase Resim3_data { get; set; }
        
    }
}
