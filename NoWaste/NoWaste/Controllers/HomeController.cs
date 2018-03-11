﻿using System;
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
        public IActionResult Index()
        {
             List<Advert> l = new List<Advert>();

             for (int i = 0; i < 5; i++)
             {
                 Advert a = new Advert();
                 a.Title = "Test" + i;
                 a.Description = "AAAAAA AAAAAAA AAAAAA AAAAA AAAAAA AAAAAAA AAAAAAAA AAAAAA AAAAAAAA AAAAAAAAAA AAAAAAAAA";
                 a.Picture = "http://www.bricotheque-chalon.fr/wp-content/uploads/2016/10/Vélo-rose.png"; /*DevSkim: ignore DS137138*/
                 l.Add(a);
             }
             return View(new AdvertListViewModel()
             {
                 List = l
             });
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


        public IActionResult CreateAdvert()
        {
            return View();
        }

        public async Task<IActionResult> Create(Advert advert)
        {
            if (User.Identity.Name == null)
                return RedirectToAction("Error");

            var user =  unitOfWork.Users.GetUserByName(User.Identity.Name);
            advert.Owner = user;
            await unitOfWork.Adverts.Add(advert);
            await unitOfWork.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public IActionResult Advert()
        {
            return View();
        }

        public IActionResult RequestMade()
        {
            return View();
        }
        public IActionResult RequestReceived()
        {
            return View();
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
