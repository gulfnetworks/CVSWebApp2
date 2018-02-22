using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CVSWebApp2.Models
{
    public class Email
    {
        public int EmailId { get; set; }
        public string Subject { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public string CC { get; set; }
        public DateTime DateTime { get; set; }
        public string Folder { get; set; }
        public string Content { get; set; }
        public bool IsRead { get; set; }
        public bool AutoEmail { get; set; }


        [ForeignKey("CompanyId")]
        public int CompanyId { get; set; }
        public Company Company { get; set; }

    }
}
