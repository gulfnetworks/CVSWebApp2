using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CVSWebApp2.Models
{
    public class UserOutlet
    {
        [Key]
        public int UserOutletId { get; set; }

        [ForeignKey("OutletId")]
        public int OutletId { get; set; }
        public Outlet Outlet { get; set; }

        [ForeignKey("Id")]
        public int Id { get; set; }
        public ApplicationUser ApplicationUser { get; set; }

        public bool DefaultOutlet { get; set; }
    }
}
