using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YTubers.Web.Core.IRepository;
using YTubers.Web.Models;

namespace YTubers.Web.Hubs
{
    public class ChatHub:Hub
    {
        public async Task SendMessageToUser(string receiverId,string sender,string message)
        {
            var user = Clients.User(receiverId);
            await user.SendAsync("SendUserMessage", sender, message);
        }
    }
}
