using System;
using System.Collections.Generic;
using System.Text;

namespace MMWebAPI.Application.InOut.User
{
    public class UserToken
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public IEnumerable<ClaimResponse> Claims { get; set; }
    }
}
