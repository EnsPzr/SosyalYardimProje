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
    
    public partial class MesajDetayTablo
    {
        public int MesajDetayId { get; set; }
        public Nullable<int> MesajTablo_MesajId { get; set; }
        public string KullaniciBilgileriTablo_KullaniciGuId { get; set; }
        public string MesajMetni { get; set; }
    
        public virtual MesajTablo MesajTablo { get; set; }
    }
}
