using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NoWaste.Models
{
    public class Request
    {
        public Boolean IsAcquitted { get; set; }

        public string UserId { get; set; }
        public int AdvertId { get; set; }

        public string Message { get; set; }
    }
}
