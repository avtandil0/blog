using AutoMapper;
using blog.BLL.Models;
using blog.DAL.Context.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace blog.Web.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Article, ArticleResponse>();
            CreateMap<Article, ArticleRequest>();
            CreateMap<Article, ArticleDto>();
            CreateMap<ArticleResponse, Article>();
            CreateMap<ArticleRequest, Article>();
            CreateMap<ArticleDto, Article>();
            CreateMap<QuestionDto, Question>();
            CreateMap<Question, QuestionDto>();
        }
    }
}
