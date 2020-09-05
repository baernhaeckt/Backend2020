using System;
using System.Threading.Tasks;
using Backend.Infrastructure.Abstraction.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace Backend.Core.Features.Newsfeed
{
    [Authorize]
    public class NotificationHub : Hub
    {
        public override Task OnConnectedAsync()
        {
            Guid userId = Context.User.Id();

            Groups.AddToGroupAsync(Context.ConnectionId, userId.ToString());

            return base.OnConnectedAsync();
        }
    }
}