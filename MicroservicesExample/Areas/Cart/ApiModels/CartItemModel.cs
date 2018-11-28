namespace MicroservicesExample.Areas.Cart.ApiModels
{
    public class CartItemModel
    {
        public int ProductId { get; set; }

        public string ProductName { get; set; }

        public double Price { get; set; }

        public int Quantity { get; set; }
    }
}