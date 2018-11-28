using MediatR;
using MicroservicesExample.Products.Models;

namespace MicroservicesExample.Products.Infrastructure.Commands
{
    public class GetProductCommand : IRequest<Product>
    {
        public int ProductId { get; set; }
    }
}