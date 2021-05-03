using Kinetix.Dto.Request;
using System.Threading.Tasks;

namespace Kinetix.Business.Abstract
{
    public interface IOrderManager
    {
        Task AddAsync(OrderRequest model);
    }
}
