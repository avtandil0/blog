using AutoMapper;
using blog.BLL.Models;
using blog.BLL.Models.Video;
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
            CreateMap<ArticleResponse, Article>();
            CreateMap<ArticleRequest, Article>();
            
            CreateMap<Article, ArticleDto>();
            CreateMap<ArticleDto, Article>();

            CreateMap<AnswerDto, Answer>();
            CreateMap<Answer, AnswerDto>();

            CreateMap<QuestionDto, Question>();
            CreateMap<Question, QuestionDto>();

            CreateMap<VideoDto, Video>();
            CreateMap<Video, VideoDto>();
        }
    }
}
