using blog.DAL.Context;
using blog.DAL.Context.Entities;
using blog.DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace blog.DAL.Repositories
{
    public class AnswerRepository : RepositoryBase<Answer>, IAnswerRepository
    {
        public AnswerRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {
        }
    }
}
