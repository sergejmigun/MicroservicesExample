using MicroservicesExample.Areas.Orders.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace MicroservicesExample.Areas.Orders.MvcControllers
{
    [Area("Orders")]
    public class OrdersController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return this.View(new OrdersViewModel());
        }

        [HttpGet]
        public ActionResult Details(int id)
        {
            return this.View(new OrdersDetailsViewModel { OrderId = id });
        }
    }
}