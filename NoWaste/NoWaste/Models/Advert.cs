using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace NoWaste.Models
{
    public class Advert
    {
        public int Id { get; set; }

        //String is default id type for IdentityUser
        [NotMapped]
        public User Owner { get; set; }

        [Required]
        public String Title { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        public String Description { get; set; }

        public String Picture { get; set; } //Ref name for blob storage
        [Required]
        public String Address { get; set; }
        [NotMapped]
        public DateTime Date { get; set; }
        [Required]
        [Display(Name = "Key words")]
        public String KeyWords { get; set; }
        public String Location { get; set; }
        [NotMapped]
        public Boolean IsVisible { get; set; }
    }
}
