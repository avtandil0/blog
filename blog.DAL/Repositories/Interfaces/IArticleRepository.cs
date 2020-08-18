using blog.DAL.Context.Entities;
using blog.DAL.Enum;
using blog.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace blog.DAL.Repositories.Interfaces
{
    public interface IArticleRepository : IRepositoryBase<Article>
    {
        IQueryable<Article> GetArticleFilter(ArticleFilterModel filter);
    }
}
