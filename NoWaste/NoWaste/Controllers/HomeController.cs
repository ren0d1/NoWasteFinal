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
             List<Advert> l = new List<Advert>();

             for (int i = 0; i < 5; i++)
             {
                 Advert a = new Advert();
                 a.Title = "Jambon";
                 a.Description = "500g de jambon";
                 a.Picture = "https://upload.wikimedia.org/wikipedia/commons/7/74/Jambon_%C3%A0_la_californienne.jpg"; /*DevSkim: ignore DS137138*/
                 l.Add(a);
             }
             if(User.Identity.Name != null)
            {
                var user = unitOfWork.Users.GetUserByName(User.Identity.Name);
                var adverts = await unitOfWork.Adverts.GetAdvertsByUser(user.Id);
                return View(new AdvertListViewModel { List = adverts });
            }
            else
            {
                return View();
            }
            
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

        public async Task<IActionResult> MyAdverts()
        {
            string userId = unitOfWork.Users.GetUserByName(User.Identity.Name).Id;
            List<Advert> l = await unitOfWork.Adverts.GetAdvertsByUser(userId);

            return View(new AdvertListViewModel()
            {
                List = l
            });
        }

        public async Task<IActionResult> RequestMade()
        {
            var user = unitOfWork.Users.GetUserByName(User.Identity.Name);
            var requests = unitOfWork.Request.GetRequestByUser(user.Id);
            List<RequestViewModel> vm = new List<RequestViewModel>();
            requests.ForEach(r => vm.Add(new RequestViewModel
            {
                Request = r,
                User = unitOfWork.Users.GetById(r.UserId),
                Advert = unitOfWork.Adverts.GetById(r.AdvertId).Result
            }));
            return View(vm);
        }
        public IActionResult RequestReceived()
        {
            var user = unitOfWork.Users.GetUserByName(User.Identity.Name);
            var requests = unitOfWork.Request.GetRequestFromUserAdvert(user.Id);
            List<RequestViewModel> vm = new List<RequestViewModel>();
            requests.ForEach(r => vm.Add(new RequestViewModel
            {
                Request = r,
                User = unitOfWork.Users.GetById(r.UserId),
                Advert = unitOfWork.Adverts.GetById(r.AdvertId).Result

            }));
            return View(vm);
        }

        public async Task<IActionResult> RequestAdvert(int id)
        {
            await unitOfWork.Request.Add(new Request { AdvertId = id, UserId = unitOfWork.Users.GetUserByName(User.Identity.Name).Id });
            await unitOfWork.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> AcceptRequest(int advertId, string userId)
        {
            unitOfWork.Request.AcceptRequest(advertId, userId);
            await unitOfWork.SaveChangesAsync();

            return RedirectToAction("RequestReceived");
        }
        public async Task<IActionResult> RefuseRequest(int advertId, string userId)
        {
            unitOfWork.Request.DeleteRequest(advertId, userId);
            await unitOfWork.SaveChangesAsync();

            return RedirectToAction("RequestReceived");
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
        public IActionResult ListAroundMe(string Lat, string Lng)
        {
            GPSCoord userCoord = new GPSCoord()
            {
                Lat = double.Parse(Lat, System.Globalization.CultureInfo.InvariantCulture),
                Lng = double.Parse(Lng, System.Globalization.CultureInfo.InvariantCulture)
            };
            var adv = unitOfWork.Adverts.GetAdvertsInUserRange(userCoord);
            return View(new AdvertListViewModel { List=adv});
        }
    }
}
