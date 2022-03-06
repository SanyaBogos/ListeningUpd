using System;
using System.Collections.Generic;
using System.Text;

namespace Listening.Infrastructure.Exceptions
{
    public class StegException : ApiException
    {
        public StegException(string message) : base(message) { }
    }
}
