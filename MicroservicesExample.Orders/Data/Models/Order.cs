using System;
using System.Collections.Generic;
using MicroservicesExample.Orders.Models;

namespace MicroservicesExample.Orders.Data.Models
{
    public class Order
    {
        public int Id { get; set; }

        public DateTime CreationDate { get; set; }

        public short Status { get; set; }

        public OrderCustomer Customer { get; set; }

        public ICollection<OrderItem> Items { get; set; }
    }
}
