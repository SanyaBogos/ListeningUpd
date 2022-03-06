using System;
using System.Collections.Generic;
using System.Text;

namespace Listening.Infrastructure.Exceptions
{
    public class ConfirmException : ApiException
    {
        public ConfirmException(string message) : base(message) { }
    }
}
