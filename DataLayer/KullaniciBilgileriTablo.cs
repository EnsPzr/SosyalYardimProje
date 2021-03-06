//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DataLayer
{
    using System;
    using System.Collections.Generic;
    
    public partial class KullaniciBilgileriTablo
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public KullaniciBilgileriTablo()
        {
            this.BagisTablo = new HashSet<BagisTablo>();
            this.GeriBildirimTablo = new HashSet<GeriBildirimTablo>();
            this.IhtiyacSahibiVeKullaniciTablo = new HashSet<IhtiyacSahibiVeKullaniciTablo>();
            this.LogTablo = new HashSet<LogTablo>();
            this.MesajDetayTablo = new HashSet<MesajDetayTablo>();
            this.MesajTablo = new HashSet<MesajTablo>();
            this.SubeTablo = new HashSet<SubeTablo>();
            this.YetkiTablo = new HashSet<YetkiTablo>();
            this.KasaTablo = new HashSet<KasaTablo>();
        }
    
        public int KullaniciId { get; set; }
        public string KullaniciAdi { get; set; }
        public string KullaniciSoyadi { get; set; }
        public Nullable<int> SehirTablo_SehirId { get; set; }
        public Nullable<bool> KullaniciOnayliMi { get; set; }
        public string KullaniciTelegramKullaniciAdi { get; set; }
        public string KullaniciTCKimlikNumarasi { get; set; }
        public string KullaniciTelefonNumarasi { get; set; }
        public Nullable<bool> KullaniciMerkezdeMi { get; set; }
        public Nullable<bool> BagisciMi { get; set; }
        public string KullaniciEPosta { get; set; }
        public string KullaniciSifre { get; set; }
        public string AndroidToken { get; set; }
        public Nullable<bool> AktifMi { get; set; }
        public string KullaniciAdres { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BagisTablo> BagisTablo { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<GeriBildirimTablo> GeriBildirimTablo { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<IhtiyacSahibiVeKullaniciTablo> IhtiyacSahibiVeKullaniciTablo { get; set; }
        public virtual SehirTablo SehirTablo { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<LogTablo> LogTablo { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MesajDetayTablo> MesajDetayTablo { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MesajTablo> MesajTablo { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SubeTablo> SubeTablo { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<YetkiTablo> YetkiTablo { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<KasaTablo> KasaTablo { get; set; }
    }
}
