using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using blog.BLL.Interfaces;
using blog.BLL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace blog.Web.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public AuthenticateResponse Authenticate([FromBody]AuthenticateRequest model)
        {
            var response = _userService.Authenticate(model);

            if (response == null)
                return new AuthenticateResponse();

            //return Ok(response);
            return response;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var users = _userService.GetAll();
            return Ok(users);
        }
    }
}