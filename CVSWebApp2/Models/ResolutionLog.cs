using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CVSWebApp2.Models
{
    public class ResolutionLog
    {

        [Key]
        public int ResolutionLogId { get; set; }


        public DateTime DateTime { get; set; }
        public int UpdaterId { get; set; }
        public string ResolutionDetails { get; set; }

        [ForeignKey("SurveyId")]
        public int SurveyId { get; set; }
        public Survey Survey { get; set; }

        public string Status { get; set; }


        [ForeignKey("ApplicationUserId")]
        public int ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
    }
}
