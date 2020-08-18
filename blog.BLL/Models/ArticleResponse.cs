using System;
using System.Collections.Generic;
using System.Text;

namespace blog.BLL.Models
{
    public class ArticleResponse
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string SubTitle { get; set; }
        public string Body { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateDeleted { get; set; }

    }
}
