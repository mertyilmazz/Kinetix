using Kinetix.Business.Abstract;
using Kinetix.Dto;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Kinetix.Business.Concrete
{
    public class OrderManagementClient : IOrderManagementClient
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IXmlManager<OrderModelDto> _xmlManager;
        public OrderManagementClient(IHttpClientFactory httpClientFactory, IXmlManager<OrderModelDto> xmlManager)
        {
            _httpClientFactory = httpClientFactory;
            _xmlManager = xmlManager;
        }

        public async Task AddOrder(OrderModelDto model)
        {
            var client = _httpClientFactory.CreateClient("OrderManagement");

            var xmlContent = _xmlManager.Serialize(model);
            var httpContent = new StringContent(xmlContent, Encoding.UTF8,"application/xml");
            await client.PostAsync("/orders", httpContent);
        }
    }
}
