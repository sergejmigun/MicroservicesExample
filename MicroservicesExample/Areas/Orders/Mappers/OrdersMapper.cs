using System.Linq;
using MicroservicesExample.Areas.Orders.ApiModels;
using MicroservicesExample.Core.Orders.Models;

namespace MicroservicesExample.Areas.Orders.Mappers
{
    public static class OrdersMapper
    {
        public static OrderModel ToOrderModel(this Order order)
        {
            return new OrderModel
            {
                Id = order.Id,
                CreationDate = order.CreationDate.ToString(),
                Customer = new OrderCustomerModel
                {
                    Name = order.Customer.Name,
                    Phone = order.Customer.Phone
                },
                Status = order.Status.ToString(),
                Items = order.Items.Select(x => new OrderItemModel
                {
                     Price = x.Price,
                     ProductId = x.ProductId,
                     ProductName = x.ProductTitle,
                     Quantity = x.Quantity
                })
            };
        }
    }
}