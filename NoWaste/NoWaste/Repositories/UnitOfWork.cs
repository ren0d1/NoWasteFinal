using NoWaste.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NoWaste.Repositories
{
    public class UnitOfWork : IDisposable
    {
        private readonly ApplicationDbContext context;
        private AdvertRepository advertRepository;
        private MessageRepository messageRepository;
        private RequestRepository requestRepository;
        private UserRepository userRepository;

        public UnitOfWork(ApplicationDbContext context)
        {
            this.context = context;
        }

        public AdvertRepository Adverts => advertRepository ?? (advertRepository = new AdvertRepository(context));

        public MessageRepository Messages => messageRepository ?? (messageRepository = new MessageRepository(context));

        public RequestRepository Requests => requestRepository ?? (requestRepository = new RequestRepository(context));

        public UserRepository Users => userRepository ?? (userRepository = new UserRepository(context));

        public async Task SaveChangesAsync()
        {
            await context.SaveChangesAsync();
        }

        //Respecting Pattern
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                context?.Dispose();
            }
        }
    }
}
