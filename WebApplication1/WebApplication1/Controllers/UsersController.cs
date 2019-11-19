using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProductStore.Entities.Models;
using ProductStore.Entities.Repos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IRepository<User> _userDataSource;
        public UsersController(IRepository<User> userDataSource)
        {
            _userDataSource = userDataSource;
        }
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<User>), 200)]
        [Authorize(Roles = "user")]
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
