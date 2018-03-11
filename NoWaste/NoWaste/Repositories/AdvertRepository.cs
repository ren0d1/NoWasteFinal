using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using NoWaste.Models;
using NoWaste.Services;
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
        public List<Advert> GetAdvertsInUserRange(GPSCoord userCoord)
        {
           
            var advInRange = Context.Adverts.Include(a => a.Owner).Where(a => GpsHelper.GetDistanceBetweenCorrds(userCoord, JsonConvert.DeserializeObject<GPSCoord>(a.Location)) < 5.0).ToList();
            Console.WriteLine(advInRange);
            return advInRange;
        }
    }
}
