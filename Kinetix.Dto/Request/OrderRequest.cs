using Microsoft.AspNetCore.Http;

namespace Kinetix.Dto.Request
{
    public class OrderRequest
    {
        public IFormFile OrderFile { get; set; }
    }
}
