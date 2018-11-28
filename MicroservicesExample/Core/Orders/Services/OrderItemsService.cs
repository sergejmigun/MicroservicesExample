using System.Threading.Tasks;
using MicroservicesExample.Core.Api;
using MicroservicesExample.Core.Orders.Models;

namespace MicroservicesExample.Core.Orders.Services
{
    public class OrderItemsService : IOrderItemsService
    {
        private readonly IRestClient restClient;
        private readonly string ordersServiceApiUrl;

        public OrderItemsService(IRestClient restClient)
        {
            this.restClient = restClient;
            this.ordersServiceApiUrl = "http://localhost:5169/api/orderItems";
        }

        public async Task AddToOrder(OrderItemManagementData data)
        {
            await this.restClient.Post($"{this.ordersServiceApiUrl}/AddToOrder/", data);
        }

        public async Task DeleteFromOrder(OrderItemManagementData data)
        {
            await this.restClient.Delete($"{this.ordersServiceApiUrl}/DeleteFromOrder/", data);
        }
    }
}
