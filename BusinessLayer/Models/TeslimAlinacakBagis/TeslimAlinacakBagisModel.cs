using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Models.TeslimAlinacakBagis
{
    public class TeslimAlinacakBagisModel
    {
        public int? BagisId { get; set; }

        [Display(Name = "Ad Soyad")]
        public String BagisciAdiSoyadi { get; set; }

        [Display(Name = "Tel No")]
        public String BagisciTelNo { get; set; }

        [Display(Name = "Adres")]
        public String BagisciAdres { get; set; }

        [Display(Name = "Eklenme Tarihi")]
        public DateTime? EklenmeTarihi { get; set; }

        [Display(Name = "Eklenme Tarihi")]
        public String EklenmeTarihiStr { get; set; }
        
        [Display(Name = "Onaylandı Mı")]
        public String OnaylandiMiStr { get; set; }

        [Display(Name = "Teslim Alındı Mı")]
        public String TeslimAlindiMi { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Tahmini Teslim Alma")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? TahminiTeslimAlma { get; set; }

        public String TahminiTeslimAlmaStr { get; set; }

        public List<TeslimAlinacakBagisEsyaModel> esyaModel { get; set; }

        public TeslimAlinacakBagisModel()
        {
            esyaModel = new List<TeslimAlinacakBagisEsyaModel>();
        }
    }
}
