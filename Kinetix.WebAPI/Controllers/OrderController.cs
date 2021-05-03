using Kinetix.Business.Abstract;
using Kinetix.Dto.Request;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Kinetix.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderManager _orderManager;

        public OrderController(IOrderManager orderManager)
        {
            _orderManager = orderManager;
        }

        [Route("")]
        [HttpPost]
        public async Task<IActionResult> AddOrder([FromForm] OrderRequest model)
        {            
            await _orderManager.AddAsync(model);
            return Ok();
        }
    }
}
