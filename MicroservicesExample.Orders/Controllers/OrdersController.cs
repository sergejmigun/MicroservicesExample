using System;
using System.Collections.Generic;
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
    public class OrdersController : Controller
    {
        private readonly IOrdersDataProvider ordersDataProvider;

        public OrdersController(IOrdersDataProvider ordersDataProvider)
        {
            this.ordersDataProvider = ordersDataProvider;
        }

        [HttpGet]
        public IEnumerable<Order> GetAll()
        {
            return this.ordersDataProvider.GetAll().Select(x => x.ToOrder()).ToList();
        }

        [HttpGet("{id}")]
        public Order GetById(int id)
        {
            return this.ordersDataProvider.GetById(id).ToOrder();
        }

        [HttpPost]
        public int Create([FromBody]OrderCreationData order)
        {
            Dal.OrderCreationData dalOrderData = order.ToDalOrderCreationData();

            dalOrderData.CreationDate = DateTime.Now;
            dalOrderData.Status = (short)OrderStatus.Pending;

            foreach (Dal.OrderItemManagementData item in dalOrderData.Items)
            {
                Product product = MessageQueue.SendMessage<Product>("GetProduct", new
                {
                    ProductId = item.ProductId
                }).Result;

                item.ProductTitle = product.Title;
                item.Price = product.Price;
            }

            return this.ordersDataProvider.Create(dalOrderData);
        }

        [HttpPost("{id}")]
        public void CompleteOrder(int id)
        {
            Order order = this.GetById(id);

            this.ordersDataProvider.Update(new Dal.OrderUpdatingData
            {
                OrderId = id,
                Status = (short)OrderStatus.Processed,
                Items = order.Items.Select(x => x.ToDalOrderItemManagementData(id))
            });
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            this.ordersDataProvider.Delete(id);
        }
    }
}
