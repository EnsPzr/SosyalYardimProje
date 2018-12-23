using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Models.MesajModelleri
{
    public class MesajDetayModel
    {
        [Display(Name = "Gönderilen Kullanıcı")]
        public String KullaniciAdiSoyadi { get; set; }

        [Display(Name = "Mesaj Metni")]
        public String MesajMetni { get; set; }
    }
}
