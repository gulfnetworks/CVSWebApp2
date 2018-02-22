using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CVSWebApp2.Models
{
    public class PersonClub
    {
        public int PersonId { get; set; }
        public Person Person { get; set; }
        public int ClubId { get; set; }
        public Club Club { get; set; }
    }
}
