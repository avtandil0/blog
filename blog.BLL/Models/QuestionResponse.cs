using System;
using System.Collections.Generic;
using System.Text;

namespace blog.BLL.Models
{
    public class QuestionResponse
    {
        public int ID { get; set; }
        public string Author { get; set; }
        public string Title { get; set; }
        public string Question { get; set; }
        public string Answer { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateDeleted { get; set; }
    }
}
