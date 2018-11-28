using System.Collections.Generic;

namespace MicroservicesExample.Core.Orders.Models
{
    public class OrderCreationData
    {
        public OrderCreationData(OrderCustomer customer, IEnumerable<OrderItemManagementData> items)
        {
            this.Customer = customer;
            this.Items = items;
        }

        public OrderCustomer Customer { get; private set; }

        public IEnumerable<OrderItemManagementData> Items { get; private set; }
    }
}
