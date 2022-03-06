namespace Listening.Core.ViewModels.Chat
{
    public class MessageForSignalRSaveDto
    {
        public long FromUserId { get; set; }
        public string ToUserSignalRId { get; set; }
        public string Message { get; set; }
    }
}
