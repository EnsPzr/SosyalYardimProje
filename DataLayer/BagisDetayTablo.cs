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
    
    public partial class BagisDetayTablo
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public BagisDetayTablo()
        {
            this.BagisDetayResimTablo = new HashSet<BagisDetayResimTablo>();
        }
    
        public int BagisDetayId { get; set; }
        public Nullable<int> BagisTablo_BagisId { get; set; }
        public Nullable<int> Adet { get; set; }
        public Nullable<int> EsyaTablo_EsyaId { get; set; }
        public Nullable<bool> AlinacakMi { get; set; }
        public Nullable<bool> AlindiMi { get; set; }
        public Nullable<bool> AlinmaTarihi { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BagisDetayResimTablo> BagisDetayResimTablo { get; set; }
        public virtual BagisTablo BagisTablo { get; set; }
        public virtual EsyaTablo EsyaTablo { get; set; }
    }
}
