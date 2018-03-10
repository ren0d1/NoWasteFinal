using NoWaste.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NoWaste.Repositories
{
    public class UserRepository : BaseModelRepository<User>
    {
        public UserRepository(ApplicationDbContext context) : base(context)
        {
        }
        public User GetUserByName(string name)
        {
            return SearchFor(u => u.UserName == name).Result.FirstOrDefault();
        }

        public virtual async Task<User> GetById(string id)
        {
            return await Context.Set<User>().FindAsync(id);
        }
    }
}
