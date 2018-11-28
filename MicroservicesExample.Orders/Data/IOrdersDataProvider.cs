using System.Collections.Generic;
using MicroservicesExample.Orders.Data.Models;

namespace MicroservicesExample.Orders.Data
{
    public interface IOrdersDataProvider
    {
        IEnumerable<Order> GetAll();

        Order GetById(int orderId);

        int Create(OrderCreationData order);

        void Update(OrderUpdatingData order);

        void Delete(int orderId);
    }
}
