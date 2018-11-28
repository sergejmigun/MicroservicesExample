using System.Collections.Generic;
using System.Threading.Tasks;
using MicroservicesExample.Core.Products.Models;

namespace MicroservicesExample.Core.Products.Services
{
    public interface IProductsService
    {
        Task<IEnumerable<Product>> GetAll();

        Task<Product> GetById(int productId);

        Task<int> Create(Product product);

        Task Update(Product product);

        Task Delete(int productId);
    }
}
