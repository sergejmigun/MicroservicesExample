using System.Collections.Generic;

namespace MicroservicesExample.Orders.Models
{
    public class OrderCreationData
    {
        public OrderCustomer Customer { get; set; }

        public IEnumerable<OrderItemManagementData> Items { get; set; }
    }
}
