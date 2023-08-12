//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DatabaseLayer
{
    using System;
    using System.Collections.Generic;
    
    public partial class BloodBankStockTable
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public BloodBankStockTable()
        {
            this.BloodStockDetails = new HashSet<BloodStockDetail>();
        }
    
        public int BloodBankStockID { get; set; }
        public int BloodBankID { get; set; }
        public int BloodGroupID { get; set; }
        public bool Status { get; set; }
        public double Quantity { get; set; }
        public string Description { get; set; }
    
        public virtual BloodGroupTable BloodGroupTable { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BloodStockDetail> BloodStockDetails { get; set; }
        public virtual BloodBankTable BloodBankTable { get; set; }
    }
}