using System;
using System.Threading.Tasks;
using System.Xml.Linq;
using IMDB.Domain.DTOs;
using Microsoft.AspNetCore.SignalR;
using static System.Net.Mime.MediaTypeNames;

namespace ChatRoom
{
    public class ChatHub : Hub
    {
        [HubMethodName("OnConnectedAsync")]
        public override async Task OnConnectedAsync()
        {
            var message = new Message()
            {
                Name = "Support",
                Text = "How can i help you ? ...",
                Date = DateTime.Now.ToString("h:MM")
            };
            await Clients.Caller.SendAsync("ReciveMessage", message);
            await base.OnConnectedAsync();
        }
        [HubMethodName("SendMessage")]
        public async Task SendMessage(string name,string text)
        {
            var message = new Message() 
            {
                Name = name,
                Text = text,
                Date = DateTime.Now.ToString("h:MM")
            };
            await this.Clients.All.SendAsync("ReciveMessage",message);
        }


    }
}
