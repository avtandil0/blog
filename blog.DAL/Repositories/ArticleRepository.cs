using blog.DAL.Context;
using blog.DAL.Context.Entities;
using blog.DAL.Enum;
using blog.DAL.Models;
using blog.DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace blog.DAL.Repositories
{
    public class ArticleRepository : RepositoryBase<Article>, IArticleRepository
    {
        public ArticleRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {
        }


        public IQueryable<Article> GetArticleFilter(ArticleFilterModel filter)
        {
            var query = from articles in RepositoryContext.Articles
                        .Where(r => r.DateDeleted == null)
                        .OrderByDescending(r => r.DateCreated)
                        select articles;

            if (filter.CategoryId != 0)
            {
                query = query.Where(r => r.Category == filter.CategoryId);
            }

           
            return query;
        }
    }
}
