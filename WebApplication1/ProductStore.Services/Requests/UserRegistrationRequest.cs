using System;
using System.ComponentModel.DataAnnotations;

namespace ProductStore.Services.Requests
{
    public class UserRegistrationRequest
    {
        [Required(ErrorMessage = "FirstName is missing")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "LastName is missing")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Email is missing")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "CellNumber is missing")]
        public string CellNumber { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime Birthday { get; set; }

        [Required(ErrorMessage = "Password is missing")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Passwords are different")]
        public string ConfirmPassword { get; set; }
    }
}
