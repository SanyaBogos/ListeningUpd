using Listening.Core.Entities.Custom;
using Listening.Infrastructure;
using Listening.Infrastructure.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Configuration;
using System;
using System.Threading.Tasks;

namespace Listening.Web.SignalR
{
    [Authorize]
    public class Chat : Hub
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly UserConnections _userConnections;
        private readonly IChatService _chatService;
        private readonly IConfiguration _configuration;

        public Chat(
            UserManager<ApplicationUser> userManager,
            UserConnections userConnections,
            IChatService chatService,
            IConfiguration configuration)
        {
            _userManager = userManager;
            _userConnections = userConnections;
            _chatService = chatService;
            _configuration = configuration;
        }

        public override async Task OnConnectedAsync()
        {
            _userConnections.ConnectedIds.Add(Context.ConnectionId);
            var user = await _userManager.GetUserAsync(Context.User);
            user.SignalRId = Context.ConnectionId;
            user.AppId = Convert.ToInt32(AppSettings.AppId);
            await _userManager.UpdateAsync(user);
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            _userConnections.ConnectedIds.Remove(Context.ConnectionId);
            var user = await _userManager.GetUserAsync(Context.User);
            user.SignalRId = null;
            user.AppId = null;
            await _userManager.UpdateAsync(user);
            await base.OnDisconnectedAsync(exception);
        }


        // removed invokation from front-end (`case of cancellation token issue)
        public async Task Send(string message, string hubReceiverId)
        {
            var user = await _userManager.GetUserAsync(Context.User);
            var messageTransfer = await _chatService.GetMessageTransferSignalR(message, hubReceiverId, user);
            await Clients.Client(hubReceiverId).SendAsync("Send", messageTransfer);
        }
    }
}