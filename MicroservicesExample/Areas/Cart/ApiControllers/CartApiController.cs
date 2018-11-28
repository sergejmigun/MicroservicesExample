using Microsoft.AspNetCore.Mvc;
using MicroservicesExample.Areas.Cart.ApiModels;
using MicroservicesExample.Areas.Orders.ApiModels;
using MicroservicesExample.Core.Cart.Services;

namespace MicroservicesExample.Areas.Orders.ApiControllers
{
    public class CartApiController : Controller
    {
        private readonly ICartService cartService;

        public CartApiController(ICartService cartService)
        {
            this.cartService = cartService;
        }

        [HttpGet]
        public CartModel GetCart()
        {
            return this.cartService.GetCart(this.HttpContext);
        }

        [HttpPut]
        public void AddProduct(int productId, int? quantity = null)
        {
            this.cartService.AddToCart(this.HttpContext, productId, quantity);
        }

        [HttpPut]
        public void UpdateCartItem(int productId, int quantity)
        {
            this.cartService.AddToCart(this.HttpContext, productId, quantity);
        }

        [HttpDelete]
        public void DeleteCartItem(int productId)
        {
            this.cartService.DeleteFromCart(this.HttpContext, productId);
        }

        [HttpPost]
        public void ProcessOrder([FromBody]OrderSubmitModel submitModel)
        {
            this.cartService.ProcessOrder(this.HttpContext, submitModel);
        }
    }
}