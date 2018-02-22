using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CVSWebApp2.Models
{
    public class SocialMedia
    {
        public int SocialMediaId { get; set; }
        public string Url { get; set; }
        public string Name { get; set; }
        public string JSONSettings { get; set; }
        public bool Active { get; set; }

        [ForeignKey("CompanyId")]
        public int CompanyId { get; set; }
        public Company Company { get; set; }
    }
}
