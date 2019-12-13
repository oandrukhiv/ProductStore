using MediatR;
using ProductStore.Entities.Models;
using ProductStore.Entities.Repos;
using System;
using System.Runtime.Serialization;
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
            if (request.Product.Price <= 0)
            {
                throw new InvalidProductException("price is not correct");
            }

            _repository.Create(request.Product);
            return Unit.Value;
            //Task.FromResult(Unit.Value);
        }
    }

    [Serializable]
    public class InvalidProductException : Exception
    {
        public InvalidProductException()
        {
        }

        public InvalidProductException(string message) : base(message)
        {
        }

        public InvalidProductException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected InvalidProductException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
