using System;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProductStore.Entities.Models;
using ProductStore.Entities.Repos;
using ProductStore.Services.Commands.ProductRelated;
using ProductStore.Services.Queries.ProductRelated;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IRepository<Product> _productDataSource;
        private IMediator Mediator { get; }

        public ProductsController(IRepository<Product> productDataSource, IMediator mediator)
        {
            _productDataSource = productDataSource;
            Mediator = mediator;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Product>), 200)]
        public async Task<ActionResult<IEnumerable<Product>>> Get()
        {
            var products = await Mediator.Send(new GetAllProductsQuery { });
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
        [Route("{Edit}")]
        [Authorize]
        public async Task<IActionResult> Put(Product product)
        {
            await Mediator.Send(new UpdateProductCommand { Product = product });
            return NoContent();
        }

        [HttpPost]
        [Route("Add")]
        public async Task<IActionResult> Post(Product product)
        {
            await Mediator.Send(new InsertProductCommand { Product = product });
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
            await Mediator.Send(new DeleteProductCommand { Product = product });

            return NoContent();
        }
    }
}
