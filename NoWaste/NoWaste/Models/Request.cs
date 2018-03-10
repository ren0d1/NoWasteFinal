using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NoWaste.Models
{
    public class Request
    {
        public int Id { get; set; }
        public Boolean IsAcquitted { get; set; }

        public User User { get; set; }
        public Advert Advert { get; set; }
        public List<Message> Message  { get; set; }
    }
}
