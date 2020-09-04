using System;
using System.Collections.Generic;
using System.Text;

namespace blog.BLL.Models
{
    public class QuestionDto
    {
        public int ID { get; set; }
        public string AuthorName { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool Status { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateDeleted { get; set; }

        public ICollection<AnswerDto> Answers { get; set; }
    }
}
