using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace MMWebAPI.Domain.Exceptions
{
    public class NoContentException : BusinessException
    {

        public NoContentException() : base ("No Content Found")
        {
            httpStatusCode = HttpStatusCode.NoContent;
        }
        public NoContentException(string message) : base(message)
        {
            httpStatusCode = HttpStatusCode.NoContent;
        }
        public NoContentException(string message, Exception innerException) : base(message, innerException)
        {
            httpStatusCode = HttpStatusCode.NoContent;
        }
    }
}
