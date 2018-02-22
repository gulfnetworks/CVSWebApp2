using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CVSWebApp2.Models
{
    public class GeneralSurvey
    {
        public int SurveyId { get; set; }
        public string Action { get; set; }
        public string AmbienceComment { get; set; }
        public int AmbienceRate { get; set; }
        public string CheckNo { get; set; }
        public string Customer { get; set; }
        public DateTime DateTime { get; set; }
        public string Email { get; set; }
        public string LastVisit { get; set; }
        public string LastVisitComment { get; set; }
        public int ManagerId { get; set; }
        public string MobileNo { get; set; }
        public int OutletId { get; set; }
        public string QualityComment { get; set; }
        public int QualityRate { get; set; }
        public string RecommendImprovements { get; set; }
        public string RecommendPoorArea { get; set; }
        public int RecommendRate { get; set; }
        public string RecommendSuggestions { get; set; }
        public string ServiceComment { get; set; }
        public int ServiceRate { get; set; }
        public string StaffId { get; set; }
        public string Status { get; set; }
        public string TableNo { get; set; }
        public string ValueComment { get; set; }
        public int ValueRate { get; set; }
        public int CompanyId { get; set; }
        public int CountryId { get; set; }
        public string OutletAddress { get; set; }
        public string OutletName { get; set; }
        public int Id { get; set; }
        public string Email2 { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int ResolutionLogId { get; set; }
        public int ApplicationUserId { get; set; }
        public string Status2 { get; set; }
    }
}
