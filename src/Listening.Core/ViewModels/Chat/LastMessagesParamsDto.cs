namespace Listening.Core.ViewModels.Chat
{
    public class MessagesParamsDto
    {
        public long FromUserId { get; set; }
        public long ToUserId { get; set; }
        public int CountOfMessages { get; set; }
        public int LastId { get; set; }
    }
}
