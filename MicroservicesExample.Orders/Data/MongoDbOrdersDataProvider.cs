using System;
using System.Collections.Generic;
using System.Linq;
using MicroservicesExample.Orders.Data.Helpers;
using MicroservicesExample.Orders.Data.Models;
using MongoDB.Driver;
using OrderModels = MicroservicesExample.Orders.Models;

namespace MicroservicesExample.Orders.Data
{
    internal class MongoDbOrdersDataProvider : IOrdersDataProvider
    {
        public IEnumerable<Order> GetAll()
        {
            return GetOrdersSet().FindSync(Builders<Order>.Filter.Empty).ToList();
        }

        public Order GetById(int orderId)
        {
            var filter = Builders<Order>.Filter.Where(x => x.Id == orderId);

            return GetOrdersSet().FindSync(filter).First();
        }

        public int Create(OrderCreationData order)
        {
            int id = new Random().Next();

            GetOrdersSet().InsertOne(new Order
            {
                Id = id,
                CreationDate = order.CreationDate,
                Customer = order.Customer,
                Items = order.Items.Select(x => new OrderModels.OrderItem
                {
                    Price = x.Price,
                    ProductId = x.ProductId,
                    ProductTitle = x.ProductTitle,
                    Quantity = x.Quantity
                }).ToList(),
                Status = order.Status
            });

            return id;
        }

        public void Update(OrderUpdatingData order)
        {
            var database = MongoDbHelper.GetDatabase();

            var filter = Builders<Order>.Filter.Where(x => x.Id == order.OrderId);
            var update = Builders<Order>.Update
                .Set(x => x.Status, order.Status)
                .Set(x => x.Items, order.Items.Select(x => ToOrderItem(x)).ToList());

            GetOrdersSet().UpdateOne(filter, update);
        }

        public void Delete(int orderId)
        {
            var filter = Builders<Order>.Filter.Where(x => x.Id == orderId);
            GetOrdersSet().DeleteOne(filter);
        }

        private static OrderModels.OrderItem ToOrderItem(OrderItemManagementData data)
        {
            return new OrderModels.OrderItem
            {
                Price = data.Price,
                ProductId = data.ProductId,
                ProductTitle = data.ProductTitle,
                Quantity = data.Quantity
            };
        }

        private static IMongoCollection<Order> GetOrdersSet()
        {
            var database = MongoDbHelper.GetDatabase();

            return database.GetCollection<Order>("Orders");
        }
    }
}
