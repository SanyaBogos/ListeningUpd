namespace Listening.Infrastructure.Exceptions
{
    public class AuthenticationException : ApiException
    {
        public AuthenticationException(string message) : base(message) { }

    }
}
