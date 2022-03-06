namespace Listening.Infrastructure.Exceptions
{
    public class TextException : ApiException
    {
        public TextException(string message) : base(message) { }
    }
}
