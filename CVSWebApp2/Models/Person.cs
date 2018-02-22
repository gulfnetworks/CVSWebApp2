using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CVSWebApp2.Models
{
    public class Person
    {
        public int PersonId { get; set; }
        public string Name { get; set; }
        public virtual ICollection<PersonClub> PersonClubs { get; set; }
    }

}
