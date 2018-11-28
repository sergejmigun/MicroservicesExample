using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using MicroservicesExample.Areas.Products.ApiModels;
using MicroservicesExample.Core.Products.Services;
using MicroservicesExample.Areas.Products.Mappers;
using System.Threading.Tasks;

namespace MicroservicesExample.Areas.Products.ApiControllers
{
    public class ProductsApiController : Controller
    {
        private readonly IProductsService productsService;

        public ProductsApiController(IProductsService productsService)
        {
            this.productsService = productsService;
        }

        [HttpGet]
        public async Task<IEnumerable<ProductModel>> GetProducts()
        {
            return (await this.productsService.GetAll()).Select(x => x.ToProductModel());
        }

        [HttpGet]
        public async Task<ProductModel> GetProduct(int id)
        {
            return (await this.GetProducts()).First(x => x.Id == id);
        }

        [HttpPost]
        public async Task<int> CreateProduct([FromBody]ProductSubmitModel submitModel)
        {
            return await this.productsService.Create(submitModel.ToProduct());
        }

        [HttpPut]
        public async Task EditProduct([FromBody]ProductSubmitModel submitModel)
        {
            await this.productsService.Update(submitModel.ToProduct());
        }

        [HttpDelete]
        public async Task DeleteProduct(int id)
        {
            await this.productsService.Delete(id);
        }
    }
}