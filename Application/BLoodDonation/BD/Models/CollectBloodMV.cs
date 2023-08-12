using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BD.Models
{
    public class CollectBloodMV
    {
        public CollectBloodMV()
        {
            DonorDetails = new CollectedBloodDonorDetailMV();
        }
        public int BloodStockDetailsID { get; set; }
        public int DonorID { get; set; }
        public int CityID { get; set; }
        public int BloodBankStockID { get; set; }
        public int BloodGroupID { get; set; }
        public double Quantity { get; set; }
        public System.DateTime DonatedDateTime { get; set; }
        public int CampaignID { get; set; }

        public CollectedBloodDonorDetailMV DonorDetails { get; set; }
    }
}