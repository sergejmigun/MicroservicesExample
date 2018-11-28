using System.Collections.Generic;

namespace MicroservicesExample.Orders.Data.Models
{
    public class OrderUpdatingData
    {
        public int OrderId { get; set; }

        public short Status { get; set; }

        public IEnumerable<OrderItemManagementData> Items { get; set; }
    }
}