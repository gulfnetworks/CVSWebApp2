using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace CVSWebApp2.Models
{
    public class GeneralSurvey
    {

        [DisplayName("Id")]
        public int SurveyId { get; set; }
        public string Action { get; set; }
        public string AmbienceComment { get; set; }


        [DisplayName("A")]
        public int AmbienceRate { get; set; }
        public string CheckNo { get; set; }
        public string Customer { get; set; }


        [DisplayName("Date Time")]
        public DateTime DateTime { get; set; }
        public string Email { get; set; }
        public string LastVisit { get; set; }
        public string LastVisitComment { get; set; }


        [DisplayName("Mobile No")]
        public string MobileNo { get; set; }
        public int OutletId { get; set; }
        public string QualityComment { get; set; }


        [DisplayName("Q")]
        public int QualityRate { get; set; }
        public string RecommendImprovements { get; set; }
        public string RecommendPoorArea { get; set; }


        [DisplayName("R")]
        public int RecommendRate { get; set; }
        public string RecommendSuggestions { get; set; }
        public string ServiceComment { get; set; }

        [DisplayName("S")]
        public int ServiceRate { get; set; }
        public string StaffId { get; set; }
        public string Status { get; set; }
        public string TableNo { get; set; }
        public string ValueComment { get; set; }

        [DisplayName("V")]
        public int ValueRate { get; set; }
        public int CompanyId { get; set; }
        public int CountryId { get; set; }
        public string OutletAddress { get; set; }


        [DisplayName("Outlet")]
        public string OutletName { get; set; }



        [DisplayName("Member Id")]
        public int MemberId { get; set; }

        [DisplayName("Member Id")]
        public string MemberEmail { get; set; }

        [DisplayName("Staff")]
        public string MemberFirstName { get; set; }

     
        public string MemberLastName { get; set; }


        [DisplayName("Manager Id")]
        public int ManagerId { get; set; }

        [DisplayName("Manager Email")]
        public string ManagerEmail { get; set; }

        [DisplayName("Manager")]
        public string ManagerFirstName { get; set; }


        public string ManagerLastName { get; set; }


        public int ResolutionLogId { get; set; }
        public int UpdaterId { get; set; }
        public string Status2 { get; set; }


        public string Country { get; set; }
        public float Average { get; set; }
        public int Compliment { get; set; }
        public int Feedback { get; set; }
        public int Complaint { get; set; }


    }
}
