using System;
using System.Collections.Generic;
using System.Text;

namespace MMWebAPI.Application.InOut.User
{
    public class LoginResponse
    {
        public string AccessToken { get; set; }
        public double ExpiresIn { get; set; }
        public UserToken UserToken { get; set; }
    }
}
