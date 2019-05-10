using FluentValidation.Results;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace MMWebAPI.Domain.Exceptions
{
    public class ValidationException : BusinessException
    {

        public ValidationResult ValidationResult{ get; set; }
        public ValidationException() : base()
        {
            httpStatusCode = HttpStatusCode.BadRequest;
        }
        public ValidationException(string message) : base(message)
        {
            httpStatusCode = HttpStatusCode.BadRequest;
        }
        public ValidationException(string message, Exception innerException) : base(message, innerException)
        {
            httpStatusCode = HttpStatusCode.BadRequest;
        }
      
    }
}
