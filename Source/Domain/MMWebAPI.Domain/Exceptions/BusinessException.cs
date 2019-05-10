using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace MMWebAPI.Domain.Exceptions
{
    public abstract class BusinessException : Exception
    {
        public HttpStatusCode httpStatusCode { get; set; }
        public BusinessException()
        {
        }

        public BusinessException(string message) : base(message)
        {

        }
        public BusinessException(string message, Exception innerException) : base( message, innerException)
        {

        }
    }
}
