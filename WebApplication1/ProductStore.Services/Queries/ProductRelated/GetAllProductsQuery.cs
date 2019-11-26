using MediatR;
using ProductStore.Entities.Models;
using ProductStore.Entities.Repos;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;


namespace ProductStore.Services.Queries.ProductRelated
{
    public class GetAllProductsQuery : IRequest<IEnumerable<Product>>
    {
    }

    public class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQuery, IEnumerable<Product>>
    {
        IRepository<Product> _repository;
        public GetAllProductsQueryHandler(IRepository<Product> repository)
        {
            _repository = repository;
        }
        public async Task<IEnumerable<Product>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
        {
            return await _repository.GetAsync();
        }
    }
}
