using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace CVSWebApp2.Models
{
    public class Outlet
    {
        public int OutletId { get; set; }

        [DisplayName("Outlet Name")]
        public string OutletName { get; set; }

        [DisplayName("Address")]
        public string OutletAddress { get; set; }

        [DisplayName("Country")]
        [ForeignKey("CompanyId")]
        public int CountryId { get; set; }
        public Country Country { get; set; }


        public virtual ICollection<UserOutlet> UserOutlets { get; set; }


        [ForeignKey("CompanyId")]
        public int CompanyId { get; set; }
        public Company Company { get; set; }
    }
}