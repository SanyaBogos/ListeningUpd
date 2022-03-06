namespace Listening.Infrastructure.Exceptions
{
    public class WebPageException : ApiException
    {
        public WebPageException(string message) : base(message) { }
    }
}
