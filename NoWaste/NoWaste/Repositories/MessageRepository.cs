using Microsoft.EntityFrameworkCore;
using NoWaste.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NoWaste.Repositories
{
    public class MessageRepository : BaseModelRepository<Message>
    {
        public MessageRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<List<Message>> GetMessagesByAdvertId(int advertId)
        {
            return await Context.Messages.Include(m => m.Advert).Where(m => m.Advert.Id == advertId).ToListAsync();
        }
    }
}
