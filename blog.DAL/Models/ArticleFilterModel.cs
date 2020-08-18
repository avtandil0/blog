using blog.DAL.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace blog.DAL.Models
{
    public class ArticleFilterModel
    {
        public ArticleTypeEnum CategoryId { get; set; }
    }
}
