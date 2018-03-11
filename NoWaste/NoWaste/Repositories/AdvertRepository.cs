using Microsoft.EntityFrameworkCore;
using NoWaste.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NoWaste.Repositories
{
    public class AdvertRepository : BaseModelRepository<Advert>
    {
        public AdvertRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<Advert> GetWithUserById(int Id)
        {
            return await Context.Adverts.Include(a => a.Owner).FirstOrDefaultAsync(a => a.Id == Id);
        }
    }
}
