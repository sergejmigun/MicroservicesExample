using System.Linq;
using MicroservicesExample.Infrastructure.Communication;
using MicroservicesExample.Orders.Data;
using MicroservicesExample.Orders.Infrastructure.Models;
using MicroservicesExample.Orders.Models;
using Microsoft.AspNetCore.Mvc;
using Dal = MicroservicesExample.Orders.Data.Models;

namespace MicroservicesExample.Orders.Controllers
{
    [Route("api/[controller]/[action]")]
    public class OrderItemsController : Controller
    {
        private readonly IOrdersDataProvider ordersDataProvider;

        public OrderItemsController(IOrdersDataProvider ordersDataProvider)
        {
            this.ordersDataProvider = ordersDataProvider;
        }

        [HttpPost]
        public void AddToOrder([FromBody]OrderItemManagementData data)
        {
            Dal.Order order = this.ordersDataProvider.GetById(data.OrderId);
            OrderItem item = order.Items.Where(x => x.ProductId == data.ProductId).FirstOrDefault();

            if (item == null)
            {
                Product product = MessageQueue.SendMessage<Product>("GetProduct", new
                {
                    ProductId = data.ProductId
                }).Result;

                order.Items.Add(new OrderItem
                {
                    ProductId = data.ProductId,
                    Price = product.Price,
                    ProductTitle = product.Title,
                    Quantity = data.Quantity
                });
            }
            else
            {
                item.Quantity = data.Quantity;
            }

            this.ordersDataProvider.Update(order.ToDalOrderUpdatingData());
        }

        [HttpDelete]
        public void DeleteFromOrder([FromBody]OrderItemManagementData data)
        {
            Dal.Order order = this.ordersDataProvider.GetById(data.OrderId);
            OrderItem item = order.Items.Where(x => x.ProductId == data.ProductId).FirstOrDefault();

            if (item != null)
            {
                order.Items.Remove(item);
                this.ordersDataProvider.Update(order.ToDalOrderUpdatingData());
            }
        }
    }
}
