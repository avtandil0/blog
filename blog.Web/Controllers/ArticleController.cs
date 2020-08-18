using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using AutoMapper;
using blog.BLL.Models;
using blog.BLL.Models.Page;
using blog.DAL.Context.Entities;
using blog.DAL.Enum;
using blog.DAL.Models;
using blog.DAL.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace blog.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticleController : ControllerBase
    {
        private IRepositoryWrapper _repository;
        private IMapper _mapper;

        public ArticleController( IRepositoryWrapper repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("getall")]
        public Page<ArticleDto> GetallArticle([FromQuery] ArticleFilterModel filter, int pageIndex = 1, int pageSize = 1)
        {
            var articles = _repository.Article.GetArticleFilter(filter);
            var page = new Pager<Article>(articles, pageIndex, pageSize);
            var getPage = page.GetPage();

            var articlesResult = _mapper.Map<IEnumerable<ArticleDto>>(getPage.Items);

            return new Page<ArticleDto>(articlesResult, getPage.ItemsCount, getPage.PagesCount, getPage.CurrentIndex);

        }

        [HttpGet]
        [Route("getArticle/{id}")]
        public ArticleDto GetallArticle(int id)
        {
            var article = _repository.Article.FindByCondition(r => r.ID == id).FirstOrDefault();
            var articleResult = _mapper.Map<ArticleDto>(article);
            return articleResult;
        }

        [HttpPost]
        [Route("saveArticle")]
        public Result SaveArticle(ArticleDto article)
        {
            var articleResult = _mapper.Map<Article>(article);
            try
            {
                articleResult.DateCreated = DateTime.Now;
                _repository.Article.Create(articleResult);
                _repository.Save();
            }
            catch(Exception e){
                return new Result(false, 500, "დაფიქსირდა შეცდომა");
            }
            return Result.SuccessInstance();
        }

        [HttpPut]
        [Route("updateArticle")]
        public Result UpdateArticle(ArticleDto article)
        {
            var articleResult = _mapper.Map<Article>(article);
            try
            {
                articleResult.DateChanged = DateTime.Now;
                _repository.Article.Update(articleResult);
                _repository.Save();
            }
            catch (Exception e)
            {
                return new Result(false, 500, "დაფიქსირდა შეცდომა");
            }
            return Result.SuccessInstance();
        }

        [HttpPost]
        [Route("deleteArticle/{id}")]
        public Result DeleteArticle(int id)
        {
            try
            {
                var data = _repository.Article.FindByCondition(r => r.ID == id).FirstOrDefault();
                data.DateDeleted = DateTime.Now;
                _repository.Article.Update(data);

                _repository.Save();
            }
            catch (KeyNotFoundException e)
            {
                return new Result(false, 500, "ID ვერ მოიძებნა");
            }
            catch (Exception e)
            {
                return new Result(false, 500, "დაფიქსირდა შეცდომა");
            }
            return Result.SuccessInstance();
        }

    }
}