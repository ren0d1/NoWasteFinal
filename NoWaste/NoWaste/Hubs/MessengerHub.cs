using Microsoft.AspNetCore.SignalR;
using NoWaste.Models;
using NoWaste.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NoWaste.Hubs
{
    public class MessengerHub : Hub
    {
        private readonly UnitOfWork unitOfWork;

        public MessengerHub(UnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task Message(Message message)
        {
            Message save = new Message
            {
                Seen = message.Seen,
                Time = message.Time,
                Sender = message.Sender,
                Request = message.Request,
                Advert = message.Advert,
                MessageContent = message.MessageContent
            };
            await unitOfWork.Messages.Add(save);
            await unitOfWork.SaveChangesAsync();

            await Clients.All.SendAsync("Message", save);
        }
    }
}
