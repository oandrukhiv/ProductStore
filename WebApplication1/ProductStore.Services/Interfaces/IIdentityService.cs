using ProductStore.Entities.Models;
using ProductStore.Services.Services;
using System.Threading.Tasks;

namespace ProductStore.Services.Interfaces
{
    public interface IIdentityService
    {
        Task<AuthenticationResult> RegisterAsync(string FirstName,
            string Password, string LastName, string Email, string CellNumber);
        Task<AuthenticationResult> LoginAsync(string Password, string Email);
        Task<User> GetByEmailAsync(string email);
    }
}
