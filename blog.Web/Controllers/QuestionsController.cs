using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using blog.BLL.Models;
using blog.BLL.Models.Page;
using blog.DAL.Context.Entities;
using blog.DAL.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace blog.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionsController : ControllerBase
    {
        private IRepositoryWrapper _repository;
        private IMapper _mapper;

        public QuestionsController(IRepositoryWrapper repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("getall")]
        public Page<QuestionDto> GetallQuestion(int pageIndex = 1, int pageSize = 20)
        {
            var questions = _repository.Question.FindByCondition(r => r.DateDeleted == null)
                                        .OrderByDescending(r => r.DateCreated);

            var page = new Pager<Question>(questions, pageIndex, pageSize);
            var getPage = page.GetPage();

            var questionsResult = _mapper.Map<IEnumerable<QuestionDto>>(getPage.Items);

            return new Page<QuestionDto>(questionsResult, getPage.ItemsCount, getPage.PagesCount, getPage.CurrentIndex);
        }

        [HttpPost]
        [Route("addQuestion")]
        public Result AddQuestion(QuestionDto question)
        {
            var questioonResult = _mapper.Map<Question>(question);
            try
            {
                questioonResult.DateCreated = DateTime.Now;

                _repository.Question.Create(questioonResult);
                _repository.Save();
            }
            catch (Exception e)
            {
                return new Result(false, 500, "დაფიქსირდა შეცდომა");
            }
            return Result.SuccessInstance();
        }
    }
}