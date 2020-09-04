using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using blog.BLL.Models;
using blog.BLL.Models.Page;
using blog.BLL.Models.Video;
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
    public class VideoController : ControllerBase
    {
        private IRepositoryWrapper _repository;
        private IMapper _mapper;

        public VideoController(IRepositoryWrapper repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("getall")]
        [AllowAnonymous]
        public Page<VideoDto> GetAllVideo(int pageIndex = 1, int pageSize = 1)
        {
            var videos = _repository.Video.FindByCondition(r => r.DateDeleted == null)
                                        .OrderByDescending(r => r.DateCreated);

            var page = new Pager<Video>(videos, pageIndex, pageSize);
            var getPage = page.GetPage();

            var videosResult = _mapper.Map<IEnumerable<VideoDto>>(getPage.Items);

            return new Page<VideoDto>(videosResult, getPage.ItemsCount, getPage.PagesCount, getPage.CurrentIndex);
        }

        [HttpGet]
        [Route("getVideo/{id}")]
        [AllowAnonymous]
        public VideoDto GetVideo(int id)
        {
            var video = _repository.Video.FindByCondition(r => r.ID == id).FirstOrDefault();
            var videoResult = _mapper.Map<VideoDto>(video);
            return videoResult;
        }

        [HttpPost]
        [Route("addVideo")]
        public Result AddVideo(VideoDto video)
        {
            var videoResult = _mapper.Map<Video>(video);
            try
            {
                videoResult.DateCreated = DateTime.Now;

                _repository.Video.Create(videoResult);
                _repository.Save();
            }
            catch (Exception e)
            {
                return new Result(false, 500, "დაფიქსირდა შეცდომა");
            }
            return Result.SuccessInstance();
        }

        [HttpPut]
        [Route("updateVideo")]
        public Result UpdateVideo(VideoDto video)
        {
            var videoResult = _mapper.Map<Video>(video);
            try
            {
                videoResult.DateChanged = DateTime.Now;
                _repository.Video.Update(videoResult);
                _repository.Save();
            }
            catch (Exception e)
            {
                return new Result(false, 500, "დაფიქსირდა შეცდომა");
            }
            return Result.SuccessInstance();
        }

        [HttpPost]
        [Route("deleteVideo/{id}")]
        public Result DeleteVideo(int id)
        {
            try
            {
                var data = _repository.Video.FindByCondition(r => r.ID == id).FirstOrDefault();
                data.DateDeleted = DateTime.Now;
                _repository.Video.Update(data);

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