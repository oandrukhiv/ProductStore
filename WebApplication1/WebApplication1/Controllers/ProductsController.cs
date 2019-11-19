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
    public class ProductsController : ControllerBase
    {
        private readonly IRepository<Product> _productDataSource;
        public ProductsController(IRepository<Product> productDataSource)
        {
            _productDataSource = productDataSource;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Product>), 200)]
        public async Task<ActionResult<IEnumerable<Product>>> Get()
        {
            var products = await _productDataSource.GetAsync();
            return Ok(products);
        }

        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(typeof(Product), 200)]
        [Authorize]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var product = await _productDataSource.GetByIdAsync(id);
            return Ok(product);
        }

        [HttpPut]
        [Authorize]
        public IActionResult Put(Product product)
        {
            _productDataSource.Update(product);
            return NoContent();
        }

        [HttpPost]
        [Route("Add")]
        [Authorize]
        public IActionResult Post(Product product)
        {
            _productDataSource.Create(product);
            return Ok(product);
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            var product = await _productDataSource.GetByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            _productDataSource.Delete(product);
            return NoContent();
        }
    }
}
