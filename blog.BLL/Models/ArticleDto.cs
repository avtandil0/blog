using System;
using System.Collections.Generic;
using System.Text;

namespace blog.BLL.Models
{
    public class ArticleDto
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string ImagePath { get; set; }
        public string ImageName { get; set; }
        public string ShortDescription { get; set; }
        public string Content { get; set; }
        public int Category { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateDeleted { get; set; }
    }
}
