using blog.DAL.Context;
using blog.DAL.Context.Entities;
using blog.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace blog.DAL.Repositories
{
    public class QuestionRepository : RepositoryBase<Question>, IQuestionRepository
    {
        public QuestionRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {
            
        }

        public Question GetQuestionWithDetails(int questionId)
        {
            return FindByCondition(r => r.ID == questionId)
                .Include(ans => ans.Answers)
                .FirstOrDefault();
        }
    }
}
