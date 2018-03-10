using Microsoft.EntityFrameworkCore;
using NoWaste.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NoWaste.Repositories
{
    public class RequestRepository : BaseModelRepository<Request>
    {
        public RequestRepository(ApplicationDbContext context) : base(context)
        {
        }
        public override async Task<Request> GetByIdAsync(int id)
        {
            return await Context.Requests.Include(r => r.Advert).Include(r => r.Messages).Include(r => r.User).FirstOrDefaultAsync(r => r.Id  == id);
        }
        public async Task<List<Request>> GetRequestByOwnerId(String ownerId)
        {
            return await Context.Requests.Include(r => r.Advert).ThenInclude(a => a.Owner).Include(r => r.User).Include(r => r.Messages).Where(r=>r.Advert.Owner.Id == ownerId).ToListAsync();
        }
        public async Task<List<Request>> GetRequestByUserId(string userId)
        {
            return await Context.Requests.Include(r => r.Advert).ThenInclude(a => a.Owner).Include(r => r.User).Include(r => r.Messages).Where(r => r.User.Id == userId).ToListAsync();
        }
    }
}
