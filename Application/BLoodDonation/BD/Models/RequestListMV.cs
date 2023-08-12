using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BD.Models
{
    public class RequestListMV
    {
        public int RequestID { get; set; }
        public System.DateTime RequestDate { get; set; }

        public int RequestByID { get; set; }
       
        public int AcceptedID { get; set; }
        public string AcceptedFullName { get; set; }
        public string RequestBy { get; set; }
        public int RequiredBloodGroupID { get; set; }

        public string BloodGroup { get; set; }
        public int RequestTypeID { get; set; }
        public int AcceptedTypeID { get; set; }

        public string ReqeustType { get; set; }
        public string AcceptedType { get; set; }
        public string RequestStatus { get; set; }
        public string ContactNo { get; set; }
        public string Address { get; set; }
        public int RequestStatusID { get; set; }
       
        public System.DateTime ExpectedDate { get; set; }
        
        public string RequestDetails { get; set; }
    }
}