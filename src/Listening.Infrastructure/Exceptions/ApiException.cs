using System;
using System.Collections.Generic;

namespace Listening.Infrastructure.Exceptions
{

    public class ApiException : Exception
    {
        public int StatusCode { get; set; }

        public List<ValidationError> Errors { get; set; }

        public ApiException(string message, int statusCode = 400, List<ValidationError> errors = null) :
            base(message)
        {
            StatusCode = statusCode;
            Errors = errors;
        }

        public ApiException(Exception ex, int statusCode = 400) : base(ex.Message)
        {
            StatusCode = statusCode;
        }
    }

}