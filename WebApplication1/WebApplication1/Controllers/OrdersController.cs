using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProductStore.Entities.Models;
using ProductStore.Entities.Repos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IRepository<Order> _orderDataSource;
        public OrdersController(IRepository<Order> orderDataSource)
        {
            _orderDataSource = orderDataSource;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Order>), 200)]
        [Authorize]
        public async Task<ActionResult<IEnumerable<Order>>> Get()
        {
            var orders = await _orderDataSource.GetAsync();
            return Ok(orders);
        }

        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(typeof(Order), 200)]
        [Authorize]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var order = await _orderDataSource.GetByIdAsync(id);
            return Ok(order);
        }

        [HttpPut]
        public IActionResult Put(Order order)
        {
            _orderDataSource.Update(order);
            return NoContent();
        }

        [HttpPost]
        [Route("Add")]
        [Authorize]
        public IActionResult Post(Order order)
        {
            _orderDataSource.Create(order);
            return Ok(order);
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> Delete([FromRoute]int id)
        {
            var order = await _orderDataSource.GetByIdAsync(id);
            if (order == null)
            {
                return NotFound();
            }

            _orderDataSource.Delete(order);
            return NoContent();
        }
    }
}
