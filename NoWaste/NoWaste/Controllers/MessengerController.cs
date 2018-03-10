using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NoWaste.Models;
using NoWaste.Repositories;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace NoWaste.Controllers
{
    [Authorize]
    public class MessengerController : Controller
    {
        private readonly UnitOfWork unitOfWork;

        public MessengerController(UnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            string UserId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            List<Request> Requests = await unitOfWork.Requests.GetRequestByOwnerId(UserId);
            return View(Requests);
        }

        public async Task<IActionResult> Conversation(int id)
        {
            Request conversation = await unitOfWork.Requests.GetByIdAsync(id);
            conversation.Advert = await unitOfWork.Adverts.GetByIdAsync(conversation.Advert.Id);
            conversation.Messages = await unitOfWork.Messages.GetMessagesByAdvertId(conversation.Advert.Id);
            return View(conversation);
        }
    }
}
