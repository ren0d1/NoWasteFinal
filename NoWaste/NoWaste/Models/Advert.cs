using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NoWaste.Models
{
    public class Advert
    {
        public int Id { get; set; }

        //String is default id type for IdentityUser
        public User Owner { get; set; }

        public String Title { get; set; }

        public String Description { get; set; }

        public String Picture { get; set; } //Ref name for blob storage
        public String Address { get; set; }
        public DateTime Date { get; set; }
        public String KeyWords { get; set; }
        public String Location { get; set; }
        public Boolean IsVisible { get; set; }
    }
}
