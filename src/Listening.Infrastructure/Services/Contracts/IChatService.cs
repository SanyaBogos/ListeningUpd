using Listening.Core.Entities.Custom;
using Listening.Core.ViewModels.Chat;
using System.Threading.Tasks;

namespace Listening.Infrastructure.Services.Contracts
{
    public interface IChatService
    {
        //Task SaveMessageAsync(Task<ApplicationUser> fromUserTask, string toUserSignalRId, string message);
        //Task SaveMessageAsync(MessageForSignalRSaveDto messageForSaveDto);
        //Task SaveMessageAsync(MessageForSaveDto messageForSaveDto);
        Task<UserMessageDto[]> GetMessagesAsync(MessagesParamsDto lastMessagesParamsDto);
        Task<MessageTransferredDto> GetMessageTransferSignalR(string message, string receiverId, ApplicationUser user);
        Task<MessageTransferredDto> GetMessageTransfer(string message, long receiverId, ApplicationUser user);
    }
}
