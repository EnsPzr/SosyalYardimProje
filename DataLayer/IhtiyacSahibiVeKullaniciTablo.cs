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
    
    public partial class IhtiyacSahibiVeKullaniciTablo
    {
        public int IhtiyacSahibiVeKullaniciId { get; set; }
        public Nullable<int> IhtiyacSahibiTablo_IhtiyacSahibiId { get; set; }
        public Nullable<int> KullaniciBilgileriTablo_KullaniciId { get; set; }
    
        public virtual IhtiyacSahibiTablo IhtiyacSahibiTablo { get; set; }
        public virtual KullaniciBilgileriTablo KullaniciBilgileriTablo { get; set; }
    }
}
