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
    
    public partial class RotaTablo
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public RotaTablo()
        {
            this.YetkiTablo = new HashSet<YetkiTablo>();
        }
    
        public int RotaId { get; set; }
        public string ControllerAdi { get; set; }
        public string ActionAdi { get; set; }
        public string LinkAdi { get; set; }
        public Nullable<int> RotaTablo_RotaId { get; set; }
        public Nullable<bool> GosterilecekMi { get; set; }
        public string DropdownBaslikAdi { get; set; }
        public Nullable<byte> Sira { get; set; }
        public Nullable<bool> HerkesGirebilirMi { get; set; }
        public string LinkClass { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<YetkiTablo> YetkiTablo { get; set; }
    }
}
