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
        public IActionResult Index()
        {
             List<Advert> l = new List<Advert>();

             for (int i = 0; i < 5; i++)
             {
                 Advert a = new Advert();
                 a.Title = "Test" + i;
                 a.Description = "AAAAAA AAAAAAA AAAAAA AAAAA AAAAAA AAAAAAA AAAAAAAA AAAAAA AAAAAAAA AAAAAAAAAA AAAAAAAAA";
                 a.Picture = "http://www.bricotheque-chalon.fr/wp-content/uploads/2016/10/Vélo-rose.png";
                 l.Add(a);
             }
             return View(new AdvertListViewModel()
             {
                 List = l
             });
            return View(new AdvertListViewModel() { List = adverts });
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

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public IActionResult AdvertDetails(int id)
        {
            var advert = unitOfWork.Adverts.GetById(id);
            return View(advert);
        }
    }
}
