using System.Threading.Tasks;
using MicroservicesExample.Core.Orders.Models;

namespace MicroservicesExample.Core.Orders.Services
{
    public interface IOrderItemsService
    {
        Task AddToOrder(OrderItemManagementData data);

        Task DeleteFromOrder(OrderItemManagementData data);
    }
}
