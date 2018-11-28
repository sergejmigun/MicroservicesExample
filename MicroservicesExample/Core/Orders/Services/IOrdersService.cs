using System.Collections.Generic;
using System.Threading.Tasks;
using MicroservicesExample.Core.Orders.Models;

namespace MicroservicesExample.Core.Orders.Services
{
    public interface IOrdersService
    {
        Task<IEnumerable<Order>> GetAll();

        Task<Order> GetById(int orderId);

        Task<int> Create(OrderCreationData order);

        Task CompleteOrder(int orderId);

        Task Delete(int orderId);
    }
}
