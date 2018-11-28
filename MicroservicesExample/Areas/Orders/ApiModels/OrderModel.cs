using System.Collections.Generic;
using System.Linq;

namespace MicroservicesExample.Areas.Orders.ApiModels
{
    public class OrderModel
    {
        public int Id { get; set; }

        public string CreationDate { get; set; }

        public OrderCustomerModel Customer { get; set; }

        public string Status { get; set; }

        public IEnumerable<OrderItemModel> Items { get; set; }

        public double Total
        {
            get
            {
                return Items.Sum(x => x.Total);
            }
        }
    }
}