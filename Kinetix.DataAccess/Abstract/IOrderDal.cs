using Kinetix.DataAccess.BaseRepository;
using Kinetix.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Kinetix.DataAccess.Abstract
{
    public interface IOrderDal : IRepository<Order>
    {
        Task AddOrderAsync(Order order);
    }
}
