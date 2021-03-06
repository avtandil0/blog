﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using blog.BLL.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace blog.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileController : ControllerBase
    {
        private readonly IHostingEnvironment _hostingEnv;

        public FileController(IHostingEnvironment hostingEnv)
        {
            _hostingEnv = hostingEnv;
        }

        [HttpPost("UploadFile")]
        public async Task<Result> UploadFile([FromForm] IFormCollection filesData)
        {

            if(filesData.Files.Count == 0)
            {
                return new Result(false, 0, "ატვირთეთ სურათი");
            }
            var images = "StaticFiles\\images";
            var webRootPath = _hostingEnv;
            var file = filesData.Files.FirstOrDefault();

            //var folderName = Path.Combine("Resources", "Images");
            //var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);

            //var exists = System.IO.File.Exists(webRootPath + @"\\" + images + @"\\" + file.FileName);
            //if (exists)
            //{
            //    return new Result(false, 0, "ამ სახელით სურათი უკვე არსებობს");
            //}

            var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), images);
            var path = Path.Combine(pathToSave, file.FileName);

            using (var stream = new FileStream(path, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return new Result(true, 1, @"\" + images + @"\" + file.FileName);
        }
    }
}