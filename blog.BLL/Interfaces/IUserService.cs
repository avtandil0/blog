using blog.BLL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace blog.BLL.Interfaces
{
    public interface IUserService
    {
        AuthenticateResponse Authenticate(AuthenticateRequest model);
        IEnumerable<User> GetAll();
    }
}
