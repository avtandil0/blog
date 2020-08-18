using System;
using System.Collections.Generic;
using System.Text;

namespace blog.DAL.Repositories.Interfaces
{
    public interface IRepositoryWrapper
    {
        IArticleRepository Article { get; }
        IQuestionRepository Question { get; }
        IVideoRepository Video { get; }
        void Save();
    }
}
