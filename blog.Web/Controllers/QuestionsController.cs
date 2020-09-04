using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using blog.BLL.Models;
using blog.BLL.Models.Page;
using blog.DAL.Context.Entities;
using blog.DAL.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace blog.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class QuestionsController : ControllerBase
    {
        private IRepositoryWrapper _repository;
        private IMapper _mapper;

        public QuestionsController(IRepositoryWrapper repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("getall")]
        public Page<QuestionDto> GetAllQuestion(int pageIndex = 1, int pageSize = 20)
        {
            bool isAuthenticated = User.Identity.IsAuthenticated;

            var questions = _repository.Question.FindByCondition(r => r.DateDeleted == null)
                                        .OrderByDescending(r => r.DateCreated);
            if (!isAuthenticated)
            {
                questions = questions.Where(r => r.Status == true).OrderByDescending(r => r.DateCreated);
            }
            var page = new Pager<Question>(questions, pageIndex, pageSize);
            var getPage = page.GetPage();

            var questionsResult = _mapper.Map<IEnumerable<QuestionDto>>(getPage.Items);

            return new Page<QuestionDto>(questionsResult, getPage.ItemsCount, getPage.PagesCount, getPage.CurrentIndex);
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("getQuestion/{id}")]
        public QuestionDto GetQuestion(int id)
        {
            var question = _repository.Question.GetQuestionWithDetails(id);
            var questionResult = _mapper.Map<QuestionDto>(question);
            questionResult.Answers = questionResult.Answers
                                .Where(r => r.DateDeleted == null)
                                .OrderByDescending(r => r.DateCreated).ToList();
            return questionResult;
        }

        [AllowAnonymous]
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

        [HttpPut]
        [Route("updateQuestion")]
        public Result UpdateQuestion(QuestionDto video)
        {
            var questionResult = _mapper.Map<Question>(video);
            try
            {
                questionResult.DateChanged = DateTime.Now;
                _repository.Question.Update(questionResult);
                _repository.Save();
            }
            catch (Exception e)
            {
                return new Result(false, 500, "დაფიქსირდა შეცდომა");
            }
            return Result.SuccessInstance();
        }

        [HttpPost]
        [Route("deleteQuestion/{id}")]
        public Result DeleteQuestion(int id)
        {
            try
            {
                var data = _repository.Question.FindByCondition(r => r.ID == id).FirstOrDefault();
                data.DateDeleted = DateTime.Now;
                _repository.Question.Update(data);

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

        [HttpPost]
        [Route("changeStatus/{id}")]
        public Result ChangeStatus(int id)
        {
            try
            {
                var data = _repository.Question.FindByCondition(r => r.ID == id).FirstOrDefault();
                data.Status = !data.Status;
                _repository.Question.Update(data);

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