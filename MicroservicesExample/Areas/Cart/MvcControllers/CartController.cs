using MicroservicesExample.Areas.Cart.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace MicroservicesExample.Areas.Cart.MvcControllers
{
    [Area("Cart")]
    public class CartController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return this.View(new CartViewModel());
        }
    }
}