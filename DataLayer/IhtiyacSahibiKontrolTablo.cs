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
    
    public partial class IhtiyacSahibiKontrolTablo
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public IhtiyacSahibiKontrolTablo()
        {
            this.IhtiyacSahibiVerilecekEsyaTablo = new HashSet<IhtiyacSahibiVerilecekEsyaTablo>();
            this.IhtiyacSahibiVerilecekMaddiTablo = new HashSet<IhtiyacSahibiVerilecekMaddiTablo>();
        }
    
        public int IhtiyacSahibiKontrolId { get; set; }
        public Nullable<int> IhtiyacSahibiTablo_IhtiyacSahibiId { get; set; }
        public Nullable<bool> MuhtacMi { get; set; }
        public Nullable<System.DateTime> Tarih { get; set; }
        public Nullable<System.DateTime> KontrolYapilmaTarihi { get; set; }
        public Nullable<System.DateTime> TahminiTeslimTarihi { get; set; }
        public Nullable<bool> TeslimTamamlandiMi { get; set; }
    
        public virtual IhtiyacSahibiTablo IhtiyacSahibiTablo { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<IhtiyacSahibiVerilecekEsyaTablo> IhtiyacSahibiVerilecekEsyaTablo { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<IhtiyacSahibiVerilecekMaddiTablo> IhtiyacSahibiVerilecekMaddiTablo { get; set; }
    }
}
