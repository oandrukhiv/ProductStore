using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using ProductStore.Entities.Models;
using ProductStore.Entities.Repos;
using ProductStore.Services.Queries.ProductRelated;
using WebApplication1.Controllers;
using Xunit;

namespace ProductStore.ApiTests
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            // Arrange
            var mockRepo = new Mock<IRepository<Product>>();
            var mockMediatr = new Mock<IMediator>();
            
            mockMediatr.Setup(mediatr => mediatr.Send(It.IsAny<GetAllProductsQuery>(), It.IsAny<CancellationToken>()))
                .Returns(A());
            var controller = new ProductsController(mockRepo.Object, mockMediatr.Object);
            //Act
            var result = controller.Get().Result;

            //Assert
            Assert.NotNull(result);
            Assert.IsAssignableFrom<ActionResult<IEnumerable<Product>>>(result);
        }
        private static async Task<IEnumerable<Product>> A()
        {
            return new List<Product>() {new Product() {Id = 1, Name = "lala", Price = 10}};
        }
    }
}
