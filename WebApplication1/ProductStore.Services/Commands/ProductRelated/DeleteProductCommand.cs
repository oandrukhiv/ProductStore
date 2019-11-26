using MediatR;
using ProductStore.Entities.Models;
using ProductStore.Entities.Repos;
using System.Threading;
using System.Threading.Tasks;

namespace ProductStore.Services.Commands.ProductRelated
{
    public class DeleteProductCommand: IRequest
    {
        public Product Product { get; set; }
    }

    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand>
    {
        readonly IRepository<Product> _repository;

        public DeleteProductCommandHandler(IRepository<Product> repository)
        {
            _repository = repository;
        }
        public async Task<Unit> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            await Task.Run(() => _repository.Delete(request.Product));
            return Unit.Value;
            //Task.FromResult(Unit.Value);
        }
    }
}
