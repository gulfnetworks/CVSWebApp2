using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;

namespace CVSWebApp2.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser<int>
    {

        [Display(Name = "User Name")]
        public override string UserName { get; set; }

        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        public string LastName { get; set; }


        [Display(Name = "Full Name")]
        public string FullName
        {
            get
            {
                return FirstName + " " + LastName;
            }
        }

        [Display(Name = "Mobile No")]
        public string MobileNo { get; set; }

        public bool Active { get; set; }

        [Display(Name = "Remember Me")]
        public bool RememberMe { get; set; }


        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }



        [DisplayName("Role")]
        [ForeignKey("MemberRoleId")]
        public int MemberRoleId { get; set; }
        public MemberRole MemberRole { get; set; }


        [Display(Name = "Company ID")]
        [ForeignKey("CompanyId")]
        public int CompanyId { get; set; }
        public Company Company { get; set; }


        [InverseProperty("UserManager")]
        public virtual ICollection<Survey> SurveyManagers { get; set; }

        [InverseProperty("UserStaff")]
        public virtual ICollection<Survey> SurveyStaffs { get; set; }


        //public virtual ICollection<UserOutlet> UserOutlets { get; set; }

        //public virtual ICollection<ResolutionLog> ResolutionLogs { get; set; }
    }
}
