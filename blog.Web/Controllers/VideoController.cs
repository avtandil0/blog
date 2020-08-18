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
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace blog.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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
        public Page<VideoDto> GetallVideo(int pageIndex = 1, int pageSize = 1)
        {
            var videos = _repository.Video.FindByCondition(r => r.DateDeleted == null)
                                        .OrderByDescending(r => r.DateCreated);

            var page = new Pager<Video>(videos, pageIndex, pageSize);
            var getPage = page.GetPage();

            var videosResult = _mapper.Map<IEnumerable<VideoDto>>(getPage.Items);

            return new Page<VideoDto>(videosResult, getPage.ItemsCount, getPage.PagesCount, getPage.CurrentIndex);
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
    }
}