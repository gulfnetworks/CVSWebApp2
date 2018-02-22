﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CVSWebApp2.Models
{
    public class Post
    {
        public int PostId { get; set; }
        public string Title { get; set; }

        public ICollection<Tag> Tags { get; } = new List<Tag>();
    }

}
