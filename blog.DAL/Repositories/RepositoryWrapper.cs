using blog.DAL.Context;
using blog.DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace blog.DAL.Repositories
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private RepositoryContext _repoContext;
        private IArticleRepository _article;
        private IQuestionRepository _question;
        private IAnswerRepository _answer;
        private IVideoRepository _video;

        public IVideoRepository Video
        {
            get
            {
                if (_video == null)
                {
                    _video = new VideoRepository(_repoContext);
                }

                return _video;
            }
        }

        public IAnswerRepository Answer
        {
            get
            {
                if (_answer == null)
                {
                    _answer = new AnswerRepository(_repoContext);
                }

                return _answer;
            }
        }

        public IQuestionRepository Question
        {
            get
            {
                if (_question == null)
                {
                    _question = new QuestionRepository(_repoContext);
                }

                return _question;
            }
        }

        public IArticleRepository Article
        {
            get
            {
                if (_article == null)
                {
                    _article = new ArticleRepository(_repoContext);
                }

                return _article;
            }
        }

        public RepositoryWrapper(RepositoryContext repositoryContext)
        {
            _repoContext = repositoryContext;
        }

        public void Save()
        {
            _repoContext.SaveChanges();
        }
    }
}
