using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CVSWebApp2.Models
{
    public class UsersByOutletViewModel
    {
        public List<ApplicationUser> Managers { get; set; }
        public List<ApplicationUser> Staffs { get; set; }
    }
}
