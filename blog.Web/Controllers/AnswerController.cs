using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using blog.BLL.Models;
using blog.DAL.Context.Entities;
using blog.DAL.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace blog.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnswerController : ControllerBase
    {
        private IRepositoryWrapper _repository;
        private IMapper _mapper;

        public AnswerController(IRepositoryWrapper repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("getAnswer/{id}")]
        public AnswerDto GetAnswer(int id)
        {
            var answer = _repository.Answer.FindByCondition(r => r.Question.ID == id).FirstOrDefault();
            var answerResult = _mapper.Map<AnswerDto>(answer);
            return answerResult;
        }

        [HttpPost]
        [Route("addAnswer")]
        public Result AddAnswer(AnswerDto answer)
        {
            var answerResult = _mapper.Map<Answer>(answer);
            try
            {
                answerResult.DateCreated = DateTime.Now;
                _repository.Answer.Create(answerResult);

                var question = _repository.Question.FindByCondition(r => r.ID == answer.QuestionID).FirstOrDefault();
                question.Status = true;
                _repository.Question.Update(question);

                _repository.Save();
            }
            catch (Exception e)
            {
                return new Result(false, 500, "დაფიქსირდა შეცდომა");
            }
            return Result.SuccessInstance();
        }

        [HttpPut]
        [Route("updateAnswer")]
        public Result UpdateAnswer(AnswerDto answer)
        {
            var answernResult = _mapper.Map<Answer>(answer);
            try
            {
                answernResult.DateChanged = DateTime.Now;
                _repository.Answer.Update(answernResult);
                _repository.Save();
            }
            catch (Exception e)
            {
                return new Result(false, 500, "დაფიქსირდა შეცდომა");
            }
            return Result.SuccessInstance();
        }

        [HttpPost]
        [Route("deleteAnswer/{id}")]
        public Result DeleteAnswer(int id)
        {
            try
            {
                var data = _repository.Answer.FindByCondition(r => r.ID == id).FirstOrDefault();
                data.DateDeleted = DateTime.Now;
                _repository.Answer.Update(data);

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