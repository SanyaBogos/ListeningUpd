using AutoMapper;
using Listening.Core.Entities.Custom;
using Listening.Core.ViewModels.Chat;
using Listening.Infrastructure.Extensions;
using Listening.Infrastructure.Services.Contracts;
using Listening.Server.Filters;
using Listening.Web.Controllers.api.Custom;
using Listening.Web.SignalR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Listening.Web.Controllers.api
{
    [Route("api/[controller]")]
    [Authorize]
    public class ChatController : BaseController
    {
        private readonly IHubContext<Chat> _hubContext;
        private readonly IChatService _chatService;
        private readonly IMapper _mapper;

        public ChatController(
            UserManager<ApplicationUser> userManager,
            IChatService chatService,
            IHubContext<Chat> hubContext,
            IMapper mapper
            ) : base(userManager)
        {
            _hubContext = hubContext;
            _chatService = chatService;
            _mapper = mapper;
        }

        [LogFilter]
        [HttpGet("usersForChat/{filter}")]
        public async Task<ChatAvailableUsersListDto> GetUsersForChat(string filter)
        {
            var currentUser = await GetCurrentUserAsync();
            Func<ApplicationUser, bool> searchCondition =
                (user) =>
                    user.Id != currentUser.Id &&
                    ((!string.IsNullOrEmpty(user.Email) && user.Email.ContainsIgnoringCase(filter))
                    || (!string.IsNullOrEmpty(user.FirstName) && user.FirstName.ContainsIgnoringCase(filter))
                    || (!string.IsNullOrEmpty(user.LastName) && user.LastName.ContainsIgnoringCase(filter)));

            Expression<Func<ApplicationUser, bool>> conditionInactive =
                (user) => string.IsNullOrEmpty(user.SignalRId) && searchCondition(user);

            Expression<Func<ApplicationUser, bool>> conditionActive =
                (user) => !string.IsNullOrEmpty(user.SignalRId) && searchCondition(user);

            var inactive = await _userManager.Users.Where(conditionInactive).ToArrayAsync();
            var active = await _userManager.Users.Where(conditionActive).ToArrayAsync();

            var inactiveDtos = inactive.Length != 0 ? _mapper.Map<ChatAvailableUserDto[]>(inactive) : new ChatAvailableUserDto[0];
            var activeDtos = active.Length != 0 ? _mapper.Map<ChatAvailableUserDto[]>(active) : new ChatAvailableUserDto[0];

            return new ChatAvailableUsersListDto
            {
                Active = activeDtos,
                Inactive = inactiveDtos
            };
        }

        [HttpGet("prevMeesages/{toUserId}/{messageCount}/{lastId}")]
        public async Task<UserMessageDto[]> GetPreviousMessages(long toUserId, int messageCount, int lastId)
        {
            var lastMessagesParamsDto = new MessagesParamsDto
            {
                FromUserId = (await GetCurrentUserAsync()).Id,
                ToUserId = toUserId,
                LastId = lastId,
                CountOfMessages = messageCount
            };

            return await _chatService.GetMessagesAsync(lastMessagesParamsDto);
        }

        // made it as GET instead of POST due to performance reasons
        [HttpGet("send/{message}/{receiverId}")]
        public async Task<string> GetSignalRReceiverIdAndSend(string message, long receiverId)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var messageTransferredDto = await _chatService.GetMessageTransfer(message, receiverId, user);

            if (messageTransferredDto.ToUserSignalRId != null)
                await _hubContext.Clients.Client(messageTransferredDto.ToUserSignalRId)
                    .SendAsync("Send", messageTransferredDto);

            return messageTransferredDto.ToUserSignalRId;
        }
    }
}
