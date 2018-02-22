using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace CVSWebApp2.Models
{
    public class UserOutletViewModel
    {
        public int UserOutletId { get; set; }

        public int OutletId { get; set; }
        [DisplayName("Outlet")]
        public string OutletName { get; set; }

        public int Id { get; set; }

        [DisplayName("User Name")]
        public string FullName { get; set; }

        [DisplayName("Default")]
        public bool DefaultOutlet { get; set; }
    }
}
