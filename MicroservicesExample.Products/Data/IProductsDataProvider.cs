using System.Collections.Generic;
using MicroservicesExample.Products.Models;

namespace MicroservicesExample.Products.Data
{
    public interface IProductsDataProvider
    {
        IEnumerable<Product> GetAll();

        Product GetById(int productId);

        int Create(Product product);

        void Update(Product product);

        void Delete(int productId);
    }
}
