using blog.DAL.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace blog.DAL.Context.Entities
{
    public class Article : BaseEntity
    {
        
        public string Title { get; set; }
        public string ImagePath { get; set; }
        public string ImageName { get; set; }
        public string ShortDescription { get; set; }
        public string Content { get; set; }
        public ArticleTypeEnum Category { get; set; }
    }
}
