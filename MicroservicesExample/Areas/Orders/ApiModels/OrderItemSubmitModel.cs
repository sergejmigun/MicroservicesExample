namespace MicroservicesExample.Areas.Orders.ApiModels
{
    public class OrderItemSubmitModel
    {
        public int ProductId { get; set; }

        public int Quantity { get; set; }
    }
}