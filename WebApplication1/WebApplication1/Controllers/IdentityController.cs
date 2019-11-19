using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProductStore.Services.Interfaces;
using ProductStore.Services.Requests;
using ProductStore.Services.Responses;
using System.Threading.Tasks;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IdentityController: ControllerBase
    {
        private readonly IIdentityService _identityService;

        public IdentityController(IIdentityService identityService)
        {
            _identityService = identityService;
        }

        [HttpPost]
        [Route("Login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] UserLoginRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var authResponse = await _identityService.LoginAsync(request.Password, request.Email);
            if (!authResponse.Success)
            {
                return BadRequest(new AuthFailedResponse
                {
                    Errors = authResponse.ErrorMessage
                });
            }
            return Ok(new AuthSuccessResponse
            {
                Tocken = authResponse.Token
            });
        }

        [HttpPost]
        [Route("Register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] UserRegistrationRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var authResponse = await _identityService.RegisterAsync(request.FirstName, request.Password, request.LastName, request.Email, request.CellNumber);
            if (!authResponse.Success)
            {
                return BadRequest(new AuthFailedResponse
                {
                    Errors = authResponse.ErrorMessage
                });
            }
            return Ok(new AuthSuccessResponse 
            { 
                Tocken = authResponse.Token
            });
        }
    }
}
