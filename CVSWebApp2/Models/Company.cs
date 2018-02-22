using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CVSWebApp2.Models
{
    public class Company
    {
        [Key]
        public int CompanyId { get; set; }

        [DisplayName("Company")]
        public string CompanyName { get; set; }

        [DisplayName("Address")]
        public string CompanyAddress { get; set; }

        [DisplayName("Contact")]
        public string CompanyContact { get; set; }

        [DisplayName("Logo")]
        public string CompanyLogoUrl { get; set; }

        [DisplayName("Company Code")]
        public string CompanyCode { get; set; }

        [DisplayName("Created Date")]
        public DateTime CreatedDate { get; set; }

        [DisplayName("Expiry Date")]
        public DateTime ExpiryDate { get; set; }

      

        public virtual ICollection<Outlet> Outlets { get; set; }


        [DisplayName("Time Zone")]
        [ForeignKey("LocationTimeZoneId")]
        public int LocationTimeZoneId { get; set; }
        public LocationTimeZone LocationTimeZone { get; set; }


        [DisplayName("Country")]
        [ForeignKey("CompanyId")]
        public int CountryId { get; set; }
        public Country Country { get; set; }


        public virtual ICollection<SocialMedia> SocialMedias { get; set; }
        public virtual ICollection<Payment> Payments { get; set; }
    }
}
