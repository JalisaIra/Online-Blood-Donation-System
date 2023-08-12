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
    using System.ComponentModel.DataAnnotations;

    public partial class DonorTable
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public DonorTable()
        {
            this.BloodStockDetails = new HashSet<BloodStockDetail>();
        }
    
        public int DonorID { get; set; }
        public string FullName { get; set; }
        public int BloodGroupID { get; set; }
        public System.DateTime LastDonationDate { get; set; }
        [RegularExpression(@"^01[3-9]\d{8}$", ErrorMessage = "Pleaser enter valid Mobile�Number")]
        public string ContactNo { get; set; }
        public string CNIC { get; set; }
        public string Location { get; set; }
        public int CityID { get; set; }
        public int UserID { get; set; }
    
        public virtual BloodGroupTable BloodGroupTable { get; set; }
        public virtual CityTable CityTable { get; set; }
        public virtual UserTable UserTable { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BloodStockDetail> BloodStockDetails { get; set; }
    }
}