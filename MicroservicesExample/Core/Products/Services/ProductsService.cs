using System.Collections.Generic;
using System.Threading.Tasks;
using MicroservicesExample.Core.Api;
using MicroservicesExample.Core.Common.Models;
using MicroservicesExample.Core.Products.Models;

namespace MicroservicesExample.Core.Products.Services
{
    public class ProductsService : IProductsService
    {
        private readonly IRestClient restClient;
        private readonly string productsServiceApiUrl;

        public ProductsService(IRestClient restClient)
        {
            this.restClient = restClient;
            this.productsServiceApiUrl = "http://localhost:5166/api/products";
        }

        public async Task<IEnumerable<Product>> GetAll()
        {
            return await this.restClient.Get<List<Product>>($"{this.productsServiceApiUrl}/GetAll");
        }

        public async Task<Product> GetById(int productId)
        {
            return await this.restClient.Get<Product>($"{this.productsServiceApiUrl}/GetById/{productId}");
        }

        public async Task<int> Create(Product product)
        {
            Identity identity = await this.restClient.Post<Identity>($"{this.productsServiceApiUrl}/Create", product);

            return int.Parse(identity.Id);
        }

        public async Task Update(Product product)
        {
            await this.restClient.Put($"{this.productsServiceApiUrl}/Update", product);
        }

        public async Task Delete(int productId)
        {
            await this.restClient.Delete($"{this.productsServiceApiUrl}/Delete/{productId}");
        }
    }
}