using System.Collections.Generic;

namespace MicroservicesExample.Areas.Orders.ApiModels
{
    public class OrderSubmitModel
    {
        public OrderCustomerModel Customer { get; set; }

        public IEnumerable<OrderItemSubmitModel> Items { get; set; }
    }
}