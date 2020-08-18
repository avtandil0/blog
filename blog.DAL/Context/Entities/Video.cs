using System;
using System.Collections.Generic;
using System.Text;

namespace blog.DAL.Context.Entities
{
    public class Video : BaseEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Url { get; set; }
    }
}
