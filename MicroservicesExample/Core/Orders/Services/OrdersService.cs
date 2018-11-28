using System.Collections.Generic;
using System.Threading.Tasks;
using MicroservicesExample.Core.Api;
using MicroservicesExample.Core.Common.Models;
using MicroservicesExample.Core.Orders.Models;

namespace MicroservicesExample.Core.Orders.Services
{
    public class OrdersService : IOrdersService
    {
        private readonly IRestClient restClient;
        private readonly string ordersServiceApiUrl;

        public OrdersService(IRestClient restClient)
        {
            this.restClient = restClient;
            this.ordersServiceApiUrl = "http://localhost:5169/api/orders";
        }

        public async Task<IEnumerable<Order>> GetAll()
        {
            return await this.restClient.Get<List<Order>>($"{this.ordersServiceApiUrl}/GetAll");
        }

        public async Task<Order> GetById(int orderId)
        {
            return await this.restClient.Get<Order>($"{this.ordersServiceApiUrl}/GetById/{orderId}");
        }

        public async Task CompleteOrder(int orderId)
        {
            await this.restClient.Post($"{this.ordersServiceApiUrl}/CompleteOrder/{orderId}");
        }

        public async Task<int> Create(OrderCreationData order)
        {
            Identity identity = await this.restClient.Post<Identity>($"{this.ordersServiceApiUrl}/Create", order);

            return int.Parse(identity.Id);
        }

        public async Task Delete(int orderId)
        {
            await this.restClient.Delete($"{this.ordersServiceApiUrl}/Delete/{orderId}");
        }
    }
}
