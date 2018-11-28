namespace MicroservicesExample.Areas.Products.ApiModels
{
    public class ProductSubmitModel
    {
        public int? Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public double Price { get; set; }
    }
}