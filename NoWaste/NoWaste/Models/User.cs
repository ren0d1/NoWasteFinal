using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace NoWaste.Models
{
    // Add profile data for application users by adding properties to the user class
    public class User : IdentityUser
    {
        public String FirstName { get; set; }

        public String LastName { get; set; }

        public DateTime Birthday { get; set; }

        public String Location { get; set; }

        public String Photo { get; set; }

        public List<Advert> Adverts { get; set; }
    }
}
