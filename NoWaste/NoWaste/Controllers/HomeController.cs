using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NoWaste.Models;
using NoWaste.Models.HomeViewModels;
using NoWaste.Repositories;

namespace NoWaste.Controllers
{
    public class HomeController : Controller
    {
        private readonly UnitOfWork unitOfWork;

        public HomeController(UnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public async Task<IActionResult> Index()
        {
            var adverts = await unitOfWork.Adverts.GetAllAsync();
            return View(adverts);
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
            if (User.Identity.Name == null)
                return RedirectToAction("Error");
            var user = unitOfWork.Users.GetUserByName(User.Identity.Name);
            advert.Owner = user;
            //advert.Date = new DateTime(2000,12,01);
            await unitOfWork.Adverts.Add(advert);
            await unitOfWork.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Advert()
        {
            return View();
        }
        public IActionResult ListAroundMe(string Lat, string Lng)
        {
            GPSCoord userCoord = new GPSCoord()
            {
                Lat = double.Parse(Lat, System.Globalization.CultureInfo.InvariantCulture),
                Lng = double.Parse(Lng, System.Globalization.CultureInfo.InvariantCulture)
            };
            var adv = unitOfWork.Adverts.GetAdvertsInUserRange(userCoord);
            return View(adv);
        }
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public async Task<IActionResult> AdvertDetails(int id)
        {
            var advert = await unitOfWork.Adverts.GetWithUserById(id);
            return View(advert);
        }
    }
}
