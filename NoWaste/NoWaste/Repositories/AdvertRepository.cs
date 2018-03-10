using NoWaste.Models;
using System;
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
    }
}
