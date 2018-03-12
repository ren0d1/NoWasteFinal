using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NoWaste.Models.AccountViewModels
{
    public class RegisterViewModel
    {
        [Required, StringLength(100)]
        [DataType(DataType.Text)]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "The first name can only be composed of non-numeric characters.")]
        public string FirstName { get; set; }

        [Required, StringLength(100)]
        [DataType(DataType.Text)]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "The last name can only be composed of non-numeric characters.")]
        public string LastName { get; set; }

        public String Photo { get; set; } //Ref name for blob storage

        [Required]
        [EmailAddress]
        [DataType(DataType.EmailAddress)]
        [RegularExpression(@"^[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?$", ErrorMessage = "The email is not corresponding a correct email format.")]
        public string Email { get; set; }

        [Required]
        [EmailAddress]
        [DataType(DataType.EmailAddress)]
        [Compare(nameof(Email), ErrorMessage = "The email and confirmation email do not match.")]
        [Display(Name = "Confirm email")]
        public string EmailConfirm { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*[\d])(?=.*[^A-Za-z0-9])([^A-Za-z0-9]|[a-zA-Z\d]){8,}$", ErrorMessage = "The password needs to be at least 8 characters long and to contain at least one lower and one upper case character, one digit and one special character.")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare(nameof(Password), ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        [Required, StringLength(100)]
        [DataType(DataType.Text)]
        public String Address { get; set; }

        [Required]
        [DataType(DataType.PostalCode)]
        [Display(Name = "Postal code")]
        public int PostalCode { get; set; }

        [Required, StringLength(100)]
        [DataType(DataType.Text)]
        public String Town { get; set; }

        [Required]
        [Phone]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^[\d]{9,10}$", ErrorMessage = "The phone number can only be composed of numeric characters and is between 9 and 10 characters long.")]
        public string Phone { get; set; }
    }
}
