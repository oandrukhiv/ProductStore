using System.ComponentModel.DataAnnotations;

namespace ProductStore.Services.Requests
{
    public class UserLoginRequest
    {
        [Required(ErrorMessage = "Login must be specified")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password must be specified")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
