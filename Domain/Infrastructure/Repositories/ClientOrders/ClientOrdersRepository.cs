using Application.Common.Interfaces.ClientOrders;
using Contracts.Orders;
using Domain.Entites;
using Domain.Orders;
using MapsterMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.ClientOrders
{
    public class ClientOrdersRepository : IClientOrdersRepository
    {
        private readonly IMapper _mapper;
        public ClientOrdersRepository( IMapper mapper)
        {
            _mapper = mapper;

        }
        public async Task<List<ClientOrdersModel>> ClientOrdersHistory(Guid UserId)
        {
            using (var _context = new ApplicationDbContext())
            {
                var order = _context.Orders.Include(x=>x.OrderDetails).FirstOrDefault();
                var orderModel = _mapper.Map<List<ClientOrdersModel>>(order);  
                if(orderModel != null)
                {
                    return orderModel;
                }
                return null;
            }
        }

        public async Task<bool> AddNewOrder(ClientOrdersAddModel order)
        {
            try
            {
                using (var _context = new ApplicationDbContext())
                {
                    var orderAdded = _mapper.Map<Domain.Entites.Order>(order);

                    var orderDetails = _mapper.Map<List<OrderDetails>>(order.OrderDetails);

                    _context.Orders.Add(orderAdded);
                    _context.AddRange(orderDetails);

                    _context.SaveChanges();
                    return true;
                }
            } catch (Exception ex)
            {
                return false;
            }
        }    


    }
}
