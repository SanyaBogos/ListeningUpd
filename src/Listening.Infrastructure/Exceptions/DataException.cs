namespace Listening.Infrastructure.Exceptions
{
    public class DataException : ApiException
    {
        public DataException(string message) : base(message) { }
    }
}
