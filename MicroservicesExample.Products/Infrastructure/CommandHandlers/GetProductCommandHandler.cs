using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MicroservicesExample.Products.Data;
using MicroservicesExample.Products.Infrastructure.Commands;
using MicroservicesExample.Products.Models;

namespace MicroservicesExample.Products.Infrastructure.CommandHandlers
{
    public class GetProductCommandHandler : IRequestHandler<GetProductCommand, Product>
    {
        private readonly IProductsDataProvider productsDataProvider;

        public GetProductCommandHandler(IProductsDataProvider productsDataProvider)
        {
            this.productsDataProvider = productsDataProvider;
        }

        public Task<Product> Handle(GetProductCommand request, CancellationToken cancellationToken)
        {
            return Task.FromResult(this.productsDataProvider.GetById(request.ProductId));
        }
    }
}
