using System.Linq;
using MicroservicesExample.Areas.Cart.ApiModels;
using MicroservicesExample.Areas.Orders.ApiModels;
using MicroservicesExample.Core.Orders.Models;
using MicroservicesExample.Core.Orders.Services;
using MicroservicesExample.Core.Products.Models;
using MicroservicesExample.Core.Products.Services;
using MicroservicesExample.Core.Common.Extensions;
using Microsoft.AspNetCore.Http;

namespace MicroservicesExample.Core.Cart.Services
{
    internal class CartService : ICartService
    {
        private readonly IProductsService productsService;
        private readonly IOrdersService ordersService;

        public CartService(
            IProductsService productsService,
            IOrdersService ordersService)
        {
            this.productsService = productsService;
            this.ordersService = ordersService;
        }

        public CartModel GetCart(HttpContext context)
        {
            return this.GetCartModel(context);
        }

        public void AddToCart(HttpContext context, int productId, int? quantity)
        {
            CartModel cart = this.GetCartModel(context);
            var productInCart = cart.Items.FirstOrDefault(x => x.ProductId == productId);

            if (productInCart == null)
            {
                Product product = this.productsService.GetById(productId).Result;

                productInCart = new CartItemModel
                {
                    Price = product.Price,
                    ProductId = product.Id,
                    ProductName = product.Title
                };

                cart.Items.Add(productInCart);
            }

            if (quantity.HasValue)
            {
                productInCart.Quantity = quantity.Value;
            }
            else
            {
                productInCart.Quantity++;
            }

            this.SaveCart(context, cart);
        }

        public void DeleteFromCart(HttpContext context, int productId)
        {
            CartModel cart = this.GetCartModel(context);

            var productInCart = cart.Items.FirstOrDefault(x => x.ProductId == productId);
            cart.Items.Remove(productInCart);

            this.SaveCart(context, cart);
        }

        public void ProcessOrder(HttpContext context, OrderSubmitModel submitModel)
        {
            CartModel cart = this.GetCartModel(context);

            this.ordersService.Create(new OrderCreationData(new OrderCustomer
            {
                Name = submitModel.Customer.Name,
                Phone = submitModel.Customer.Phone
            }, submitModel.Items.Select(x => new OrderItemManagementData
            {
                ProductId = x.ProductId,
                Quantity = x.Quantity
            })));

            cart.Items.Clear();
            this.SaveCart(context, cart);
        }

        private CartModel GetCartModel(HttpContext context)
        {
            if (context.Session.Keys.Contains("cart"))
            {
                return context.Session.Get<CartModel>("cart");
            }

            var cart = new CartModel();

            this.SaveCart(context, cart);

            return cart;
        }

        private void SaveCart(HttpContext context, CartModel cart)
        {
            context.Session.Set("cart", cart);
        }
    }
}