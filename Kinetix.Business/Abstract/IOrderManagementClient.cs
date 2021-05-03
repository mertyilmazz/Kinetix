using Kinetix.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Kinetix.Business.Abstract
{
    public interface IOrderManagementClient
    {
        Task AddOrder(OrderModelDto model);
    }
}
