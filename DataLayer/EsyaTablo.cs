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
    
    public partial class EsyaTablo
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public EsyaTablo()
        {
            this.BagisDetayTablo = new HashSet<BagisDetayTablo>();
            this.DepoTablo = new HashSet<DepoTablo>();
            this.IhtiyacSahibiVerilecekEsyaTablo = new HashSet<IhtiyacSahibiVerilecekEsyaTablo>();
        }
    
        public int EsyaId { get; set; }
        public string EsyaAdi { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BagisDetayTablo> BagisDetayTablo { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DepoTablo> DepoTablo { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<IhtiyacSahibiVerilecekEsyaTablo> IhtiyacSahibiVerilecekEsyaTablo { get; set; }
    }
}
