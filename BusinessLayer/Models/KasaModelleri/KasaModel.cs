﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer.Models.KullaniciModelleri;
using BusinessLayer.Models.OrtakModeller;

namespace BusinessLayer.Models.KasaModelleri
{
    public class KasaModel
    {
        [Key]
        public int KasaId { get; set; }

        [Display(Name = "Miktar")]
        [Required(ErrorMessage = "Miktar girilmelidir.")]
        public double Miktar { get; set; }

        [Required(ErrorMessage = "Tarih girilmelidir.")]
        [DataType(DataType.Date)]
        [Display(Name = "Gerçekleşme Tarihi")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? Tarih { get; set; }

        [MinLength(6, ErrorMessage = "Açıklama uzunluğu en az {1} karakterden oluşmalıdır."), MaxLength(50, ErrorMessage = "Açıklama uzunluğu maksimum 50 karakterden oluşmalıdır.")]
        [Display(Name = "Açıklama")]
        public String Aciklama { get; set; }

        [Display(Name = "İşlem Yapan")]
        public int? KullaniciId { get; set; }

        [Display(Name = "İşlem Yapan")]
        public String KullaniciAdiSoyadi { get; set; }

        public SehirModel Sehir { get; set; }

        [Display(Name = "Gelir-Gider")]
        [Required(ErrorMessage = "Gelir gider seçilmek zorundadır.")]
        public int? GelirGider { get; set; }

        public KasaModel()
        {
            Sehir=new SehirModel();
        }
    }
}
