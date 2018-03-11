using Microsoft.EntityFrameworkCore;
using NoWaste.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NoWaste.Repositories
{
    public class RequestRepository: BaseModelRepository<Request>
    {
        public RequestRepository(ApplicationDbContext context) : base(context)
        {
        }

        public List<Request> GetRequestByUser(string userId)
        {
            return Context.Requests.Where(r => r.UserId == userId).ToList();
        }

        public List<Request> GetRequestFromUserAdvert(string userId)
        {
            var adverts = Context.Adverts.Include(a => a.Owner).Where(a => a.Owner.Id == userId).Select(a=>a.Id).ToList();
            var requests = Context.Requests.Where(r => adverts.Contains(r.AdvertId)).ToList();

            return requests;
        }
    }
}
