using System;
using System.Collections.Generic;
using System.Text;

namespace blog.DAL.Context.Entities
{
    public class Answer : BaseEntity
    {
        public string Text { get; set; }
        public string UserName { get; set; }
        public string UserProfession { get; set; }

        public Question Question { get; set; }

    }
}
