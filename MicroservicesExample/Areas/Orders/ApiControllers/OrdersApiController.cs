using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MicroservicesExample.Areas.Orders.ApiModels;
using MicroservicesExample.Areas.Orders.Mappers;
using MicroservicesExample.Core.Orders.Models;
using MicroservicesExample.Core.Orders.Services;
using Microsoft.AspNetCore.Mvc;

namespace MicroservicesExample.Areas.Orders.ApiControllers
{
    public class OrdersApiController : Controller
    {
        private readonly IOrdersService ordersService;
        private readonly IOrderItemsService orderItemsService;

        public OrdersApiController(
            IOrdersService ordersService,
            IOrderItemsService orderItemsService)
        {
            this.ordersService = ordersService;
            this.orderItemsService = orderItemsService;
        }

        public async Task<IEnumerable<OrderModel>> GetOrders()
        {
            return (await this.ordersService.GetAll()).Select(x => x.ToOrderModel());
        }

        [HttpGet]
        public async Task<OrderModel> GetOrder(int id)
        {
            return (await this.ordersService.GetById(id)).ToOrderModel();
        }

        [HttpPut]
        public async Task CompleteOrder(int id)
        {
            await this.ordersService.CompleteOrder(id);
        }

        [HttpDelete]
        public async Task DeleteOrder(int id)
        {
            await this.ordersService.Delete(id);
        }

        [HttpPut]
        public async Task AddProduct(int orderId, int productId, int quantity)
        {
            await this.orderItemsService.AddToOrder(new OrderItemManagementData
            {
                OrderId = orderId,
                ProductId = productId,
                Quantity = quantity
            });
        }

        [HttpPut]
        public async Task UpdateProduct(int orderId, int productId, int quantity)
        {
            await this.orderItemsService.AddToOrder(new OrderItemManagementData
            {
                OrderId = orderId,
                ProductId = productId,
                Quantity = quantity
            });
        }

        [HttpDelete]
        public async Task DeleteProduct(int orderId, int productId)
        {
            await this.orderItemsService.DeleteFromOrder(new OrderItemManagementData
            {
                OrderId = orderId,
                ProductId = productId
            });
        }
    }
}