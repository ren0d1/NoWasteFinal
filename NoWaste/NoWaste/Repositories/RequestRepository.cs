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
    }
}
