namespace MicroservicesExample.Orders.Data.Models
{
    public class OrderItemManagementData
    {
        public int OrderId { get; set; }

        public int ProductId { get; set; }

        public string ProductTitle { get; set; }

        public double Price { get; set; }

        public int Quantity { get; set; }
    }
}