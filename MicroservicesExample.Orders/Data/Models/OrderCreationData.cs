using System;
using System.Collections.Generic;
using MicroservicesExample.Orders.Models;

namespace MicroservicesExample.Orders.Data.Models
{
    public class OrderCreationData
    {
        public DateTime CreationDate { get; set; }

        public OrderCustomer Customer { get; set; }

        public IEnumerable<OrderItemManagementData> Items { get; set; }

        public short Status { get; set; }
    }
}
