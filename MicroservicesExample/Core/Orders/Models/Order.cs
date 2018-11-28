using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MicroservicesExample.Core.Orders.Models
{
    public class Order
    {
        public int Id { get; set; }

        public DateTime CreationDate { get; set; }

        public double Total { get; set; }

        public OrderStatus Status { get; set; }

        public OrderCustomer Customer { get; set; }

        public IEnumerable<OrderItem> Items { get; set; }
    }
}
