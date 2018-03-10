using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace NoWaste.Models
{
    public class Message
    {
        public int Id { get; set; }
        public Boolean Seen { get; set; }

        public DateTime Time { get; set; }

        public User Sender { get; set; }

        public Request Request { get; set; }

        public Advert Advert { get; set; }

        public string MessageContent { get; set; }
    }
}
