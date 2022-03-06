using Listening.Core.ViewModels.Chat;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Listening.Infrastructure.Repositories.Abstract
{
    public interface IChatRepository
    {
        Task InsertMessageAsync(MessageForSignalRSaveDto messageForSaveDto);
        Task<long> InsertMessageReturnsIdAsync(MessageForSignalRSaveDto messageForSaveDto);
        Task<string> InsertMessageReturnSignalRReceiverIdAsync(MessageForSaveDto messageForSaveDto);
        Task<UserMessageDto[]> GetDialogueMessagesAsync(MessagesParamsDto lastMessagesParamsDto);
    }
}
