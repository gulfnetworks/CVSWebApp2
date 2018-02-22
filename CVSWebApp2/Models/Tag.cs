using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CVSWebApp2.Models
{
    public class Tag
    {

        public int TagId { get; set; }
        public string Text { get; set; }

        public ICollection<Post> Posts { get; } = new List<Post>();
    }
}
