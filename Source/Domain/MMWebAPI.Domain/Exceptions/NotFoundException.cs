using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace MMWebAPI.Domain.Exceptions
{
    public class NotFoundException : BusinessException
    {
        public NotFoundException() : base ("Nothing Found")
        {
            httpStatusCode = HttpStatusCode.NotFound;
            
        }
        public NotFoundException(string message) : base (message)
        {
            httpStatusCode = HttpStatusCode.NotFound;
        }
        public NotFoundException(string message, Exception innerException) : base( message, innerException)
        {
            httpStatusCode = HttpStatusCode.NotFound;
        }
    }
}
