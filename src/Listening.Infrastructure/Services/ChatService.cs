using Listening.Core.Entities.Custom;
using Listening.Core.ViewModels.Chat;
using Listening.Infrastructure.Repositories.Abstract;
using Listening.Infrastructure.Services.Contracts;
using System.Threading.Tasks;

namespace Listening.Infrastructure.Services
{
    public class ChatService : IChatService
    {
        private readonly IChatRepository _chatRepository;

        public ChatService(IChatRepository chatRepository)
        {
            _chatRepository = chatRepository;
        }

        //public async Task SaveMessageAsync(Task<ApplicationUser> fromUserTask, string toUserSignalRId, string message)
        //{
        //    var userFrom = await fromUserTask;
        //    var userFromId = userFrom.Id;
        //    await _chatRepository.InsertMessageAsync(userFromId, toUserSignalRId, message);
        //}

        //public async Task SaveMessageAsync(MessageForSignalRSaveDto messageForSaveDto)
        //{
        //    //var userFrom = await fromUserTask;
        //    //var userFromId = userFrom.Id;
        //    await _chatRepository.InsertMessageAsync(messageForSaveDto);
        //}

        //public async Task<string> SaveMessageAsync(MessageForSaveDto messageForSaveDto)
        //{
        //    //var userFrom = await fromUserTask;
        //    //var userFromId = userFrom.Id;
        //    return await _chatRepository.InsertMessageReturnSignalrIdAsync(messageForSaveDto);
        //}

        public async Task<UserMessageDto[]> GetMessagesAsync(MessagesParamsDto lastMessagesParamsDto)
        {
            //var userFrom = await fromUserTask;
            //var userFromId = userFrom.Id;
            return await _chatRepository.GetDialogueMessagesAsync(lastMessagesParamsDto);
        }

        public async Task<MessageTransferredDto> GetMessageTransferSignalR(string message, string receiverId, ApplicationUser user)
        {
            var messageForSaveDto = new MessageForSignalRSaveDto
            {
                FromUserId = user.Id,
                ToUserSignalRId = receiverId,
                Message = message
            };
            await _chatRepository.InsertMessageAsync(messageForSaveDto);

            var messageTransfer = new MessageTransferredDto
            {
                FromUserId = user.Id,
                FromUserName = user.UserName,
                Message = message
            };

            return messageTransfer;
        }

        public async Task<MessageTransferredDto> GetMessageTransfer(string message, long receiverId, ApplicationUser user)
        {
            var messageForSaveDto = new MessageForSaveDto
            {
                FromUserId = user.Id,
                ToUserId = receiverId,
                Message = message
            };

            var signalrId = await _chatRepository.InsertMessageReturnSignalRReceiverIdAsync(messageForSaveDto);
            var messageTransfer = new MessageTransferredDto
            {
                FromUserId = user.Id,
                FromUserName = user.UserName,
                Message = message,
                ToUserSignalRId = signalrId
            };

            //await SaveMessageAsync(messageForSaveDto);
            return messageTransfer;
        }
    }
}
