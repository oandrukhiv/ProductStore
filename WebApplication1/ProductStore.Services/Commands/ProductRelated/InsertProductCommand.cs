using MediatR;
using ProductStore.Entities.Models;
using ProductStore.Entities.Repos;
using System.Threading;
using System.Threading.Tasks;

namespace ProductStore.Services.Commands.ProductRelated
{
    public class InsertProductCommand : IRequest
    {
        public Product Product { get; set; }
    }

    public class InsertProductCommandHandler : IRequestHandler<InsertProductCommand>
    {
        readonly IRepository<Product> _repository;

        public InsertProductCommandHandler(IRepository<Product> repository)
        {
            _repository = repository;
        }

        public async Task<Unit> Handle(InsertProductCommand request, CancellationToken cancellationToken)
        {
            await Task.Run(() => _repository.Create(request.Product));
            return Unit.Value;
            //Task.FromResult(Unit.Value);
        }
    }
}
