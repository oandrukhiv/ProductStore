using MediatR;
using ProductStore.Entities.Models;
using ProductStore.Entities.Repos;
using System.Threading;
using System.Threading.Tasks;

namespace ProductStore.Services.Queries.ProductRelated
{
    public class GetProductByIdQuery : IRequest <Product>
    {
        public int ProductId { get; set; }
    }
    public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, Product>
    {
        readonly IRepository<Product> _repository;
        public GetProductByIdQueryHandler(IRepository<Product> repository)
        {
            _repository = repository;
        }
        public async Task<Product> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            return await _repository.GetByIdAsync(request.ProductId);
        }
    }
}
