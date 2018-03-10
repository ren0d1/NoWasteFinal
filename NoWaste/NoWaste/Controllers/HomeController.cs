using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NoWaste.Models;
using NoWaste.Repositories;

namespace NoWaste.Controllers
{
    public class HomeController : Controller
    {
        private UnitOfWork unitOfWork;
        public HomeController(UnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        
        public async Task<IActionResult> CreateAdvert()
        {
            return View();
        }

        public async Task<IActionResult> Create(Advert advert)
        {
            if(User.Identity.Name != null)
            {
                var user = unitOfWork.Users.GetUserByName(User.Identity.Name);
                advert.Owner = user;
                await unitOfWork.Adverts.Add(advert);
                await unitOfWork.SaveChangesAsync();
                return RedirectToAction("/Home/Index");
            }
            return RedirectToAction("Error");
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
