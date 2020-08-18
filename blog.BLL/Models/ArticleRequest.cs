using System;
using System.Collections.Generic;
using System.Text;

namespace blog.BLL.Models
{
    public class ArticleRequest
    {
        public string Title { get; set; }
        public string ShortDescription { get; set; }
        public int Category { get; set; }
        public string Content { get; set; }
    }
}
