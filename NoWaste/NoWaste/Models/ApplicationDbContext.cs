using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace NoWaste.Models
{
    //Inherits from IdentityDbContext -> DbSet User is available per default
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Request>().HasKey(r => new { r.AdvertId, r.UserId });
        }

        public DbSet<Advert> Adverts { get; set; }

        public DbSet<Request> Requests { get; set; }
    }
}
