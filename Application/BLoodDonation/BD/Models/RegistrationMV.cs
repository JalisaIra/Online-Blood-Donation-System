using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BD.Models
{
    public class RegistrationMV
    {
        public RegistrationMV()
        {
            Seeker = new SeekerMV();
            Hospital = new HospitalMV();
            BloodBank = new BloodBankMV();
            Donor = new DonorMV();
            User = new UserMV();
        }
        public int UserTypeID { get; set; }
        [RegularExpression(@"^01[3-9]\d{8}$", ErrorMessage = "Pleaser enter valid Mobile Number")]
        public string ContactNo { get; set; }
        public int CityID { get; set; }
        public int BloodGroupID { get; set; }
        public int GenderID { get; set; }
        public SeekerMV Seeker { get; set; }
        public HospitalMV Hospital { get; set; }
        public BloodBankMV BloodBank { get; set; }
        public DonorMV Donor { get; set; }
        public UserMV User { get; set; }
        
    }
}