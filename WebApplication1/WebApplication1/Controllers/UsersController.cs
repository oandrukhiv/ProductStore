using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using ProductStore.Entities.Models;
using ProductStore.Entities.Repos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Controllers
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    public class CustomAuthorizeAttribute : AuthorizeAttribute, IAuthorizationFilter
    {
        private readonly string[] _roles;

        public CustomAuthorizeAttribute(params string[] roles) : base()
        {
            _roles = roles;
        }

        public CustomAuthorizeAttribute() : base()
        { }


        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var user = context.HttpContext.User;

            if (!user?.Identity?.IsAuthenticated != false)
            {
                // it isn't needed to set unauthorized result 
                // as the base class already requires the user to be authenticated
                // this also makes redirect to a login page work properly
                context.Result = new UnauthorizedResult();
                return;
            }

            var role = user.Claims.FirstOrDefault(c => c.Type == "role");
            var isInCurrentRole =  _roles?.Any(r => string.Equals(role?.Value, r, StringComparison.InvariantCultureIgnoreCase)) ?? true;

            if (!isInCurrentRole)
            {
                context.Result = new StatusCodeResult((int)System.Net.HttpStatusCode.Forbidden);
                return;
            }
        }
    }

    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IRepository<User> _userDataSource;
        private IMediator _mediator { get; }

        public UsersController(IRepository<User> userDataSource, IMediator mediator)
        {
            _userDataSource = userDataSource;
            _mediator = mediator;
        }
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<User>), 200)]
        [CustomAuthorizeAttribute("user")]
        public async Task<ActionResult<IEnumerable<User>>> Get()
        {
            var users = await _userDataSource.GetAsync();
            return Ok(users);
        }

        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(typeof(User), 200)]
        [Authorize]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var user = await _userDataSource.GetByIdAsync(id);
            return Ok(user);
        }

        [HttpPut]
        [Authorize]
        public IActionResult Put(User user)
        {
            _userDataSource.Update(user);
            return NoContent();
        }

        [HttpPost]
        [Route("Add")]
        [Authorize]
        public IActionResult Post(User user)
        {
            _userDataSource.Create(user);
            return Ok(user);
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            var user = await _userDataSource.GetByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _userDataSource.Delete(user);
            return NoContent();
        }
    }
}
