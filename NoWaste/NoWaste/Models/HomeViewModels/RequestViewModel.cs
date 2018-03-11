using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NoWaste.Models.HomeViewModels
{
    public class RequestViewModel
    {
        public Request Request { get; set; }
        public Advert Advert { get; set; }
        public User User { get; set; }
    }
}
