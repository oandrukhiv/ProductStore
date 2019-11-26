using MediatR;
using ProductStore.Entities.Models;
using ProductStore.Entities.Repos;
using System.Threading;
using System.Threading.Tasks;

namespace ProductStore.Services.Commands.ProductRelated
{
    public class UpdateProductCommand : IRequest
    {
        public Product Product { get; set; }
    }

    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand>
    {
        readonly IRepository<Product> _repository;

        public UpdateProductCommandHandler(IRepository<Product> repository)
        {
            _repository = repository;
        }

        public async Task<Unit> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            await Task.Run(() => _repository.Update(request.Product));
            return Unit.Value;
            //Task.FromResult(Unit.Value);
        }
    }
}
