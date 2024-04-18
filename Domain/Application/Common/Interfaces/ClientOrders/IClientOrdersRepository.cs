using Contracts.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Interfaces.ClientOrders
{
    public interface IClientOrdersRepository
    {
        Task<List<ClientOrdersModel>> ClientOrdersHistory(Guid UserId);

        Task<bool> AddNewOrder(ClientOrdersAddModel order);

    }
}
