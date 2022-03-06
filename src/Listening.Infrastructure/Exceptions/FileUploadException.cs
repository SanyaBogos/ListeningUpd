namespace Listening.Infrastructure.Exceptions
{
    public class FileUploadException : ApiException
    {
        public FileUploadException(string message) : base(message) { }
    }
}
