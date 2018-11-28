using MicroservicesExample.Areas.Products.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace MicroservicesExample.Areas.Products.MvcControllers
{
    [Area("Products")]
    public class ProductsController: Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return this.View(new ProductsViewModel());
        }
    }
}