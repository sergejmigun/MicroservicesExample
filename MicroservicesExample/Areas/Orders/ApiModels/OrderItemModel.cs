namespace MicroservicesExample.Areas.Orders.ApiModels
{
    public class OrderItemModel
    {
        public int? ProductId { get; set; }

        public string ProductName { get; set; }

        public double Price { get; set; }

        public int Quantity { get; set; }

        public double Total
        {
            get
            {
                return this.Price * this.Quantity;
            }
        }
    }
}
