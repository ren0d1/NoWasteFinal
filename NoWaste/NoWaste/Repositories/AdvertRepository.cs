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
        public List<Advert> GetAdvertsInUserRange(GPSCoord userCoord)
        {
            userCoord.Lat = userCoord.Lat / 10000000;
            userCoord.Lng = userCoord.Lng / 10000000;
            var advInRange = Context.Adverts.Include(a => a.Owner).Where(a => GpsHelper.GetDistanceBetweenCorrds(userCoord, JsonConvert.DeserializeObject<GPSCoord>(a.Location)) < 5).ToList();
            return advInRange;
        }
    }
}
