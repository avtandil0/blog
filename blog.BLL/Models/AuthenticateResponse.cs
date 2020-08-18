using System;
using System.Collections.Generic;
using System.Text;

namespace blog.BLL.Models
{
    public class AuthenticateResponse
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Token { get; set; }
        public string Message { get; set; }

        public AuthenticateResponse()
        {
            Id = -1;
            FirstName = null;
            LastName = null;
            Username = null;
            Token = null;
            Message = "მომხმარებელი ან პაროლი არასწორია";
        }
        public AuthenticateResponse(User user, string token)
        {
            Id = user.Id;
            FirstName = user.FirstName;
            LastName = user.LastName;
            Username = user.Username;
            Token = token;
        }
    }
}
