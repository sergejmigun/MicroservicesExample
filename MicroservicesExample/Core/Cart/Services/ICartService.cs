using MicroservicesExample.Areas.Cart.ApiModels;
using MicroservicesExample.Areas.Orders.ApiModels;
using Microsoft.AspNetCore.Http;

namespace MicroservicesExample.Core.Cart.Services
{
    public interface ICartService
    {
        CartModel GetCart(HttpContext context);

        void AddToCart(HttpContext context, int productId, int? quantity);

        void DeleteFromCart(HttpContext context, int productId);

        void ProcessOrder(HttpContext context, OrderSubmitModel submitModel);
    }
}