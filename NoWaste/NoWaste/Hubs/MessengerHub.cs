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

        public async Task Message(String message)
        {
            await Clients.All.SendAsync("Message", message);
        }
    }
}
