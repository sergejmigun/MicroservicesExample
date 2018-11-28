using System.Collections.Generic;
using MicroservicesExample.Products.Data;
using MicroservicesExample.Products.Models;
using Microsoft.AspNetCore.Mvc;

namespace MicroservicesExample.Products.Controllers
{
    [Route("api/[controller]/[action]")]
    public class ProductsController : Controller
    {
        private readonly IProductsDataProvider productsDataProvider;

        public ProductsController(IProductsDataProvider productsDataProvider)
        {
            this.productsDataProvider = productsDataProvider;
        }

        [HttpGet]
        public IEnumerable<Product> GetAll()
        {
            return this.productsDataProvider.GetAll();
        }

        [HttpGet("{id}")]
        public Product GetById(int id)
        {
            return this.productsDataProvider.GetById(id);
        }

        [HttpPost]
        public object Create([FromBody]Product product)
        {
            int id = this.productsDataProvider.Create(product);

            return new
            {
                Id = id
            };
        }

        [HttpPut]
        public void Update([FromBody]Product product)
        {
            this.productsDataProvider.Update(product);
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            this.productsDataProvider.Delete(id);
        }
    }
}
