using System;
using System.Collections.Generic;
using System.Text;

namespace blog.DAL.Context.Entities
{
    public class Question : BaseEntity
    {
        public string AuthorName { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool Status { get; set; }
        public ICollection<Answer> Answers { get; set; }
    }
}
