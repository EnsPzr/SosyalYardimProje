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
    
    public partial class MesajTablo
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public MesajTablo()
        {
            this.MesajDetayTablo = new HashSet<MesajDetayTablo>();
        }
    
        public int MesajId { get; set; }
        public Nullable<int> SehirTablo_SehirId { get; set; }
        public string KullaniciBilgileriTablo_KullaniciGuId { get; set; }
        public Nullable<int> KimeAtildi { get; set; }
    
        public virtual KullaniciBilgileriTablo KullaniciBilgileriTablo { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MesajDetayTablo> MesajDetayTablo { get; set; }
        public virtual SehirTablo SehirTablo { get; set; }
    }
}