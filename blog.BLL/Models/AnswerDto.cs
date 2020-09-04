using System;
using System.Collections.Generic;
using System.Text;

namespace blog.BLL.Models
{
    public class AnswerDto
    {
        public int ID { get; set; }
        public string Text { get; set; }
        public string UserName { get; set; }
        public string UserProfession { get; set; }
        public int QuestionID { get; set; }

        public DateTime? DateCreated { get; set; }
        public DateTime? DateDeleted { get; set; }
    }
}
