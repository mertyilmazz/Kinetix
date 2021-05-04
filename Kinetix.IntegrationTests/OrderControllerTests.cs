using Microsoft.AspNetCore.Mvc.Testing;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace Kinetix.IntegrationTests
{
    public class OrderControllerTests : IClassFixture<TestFactory<WebAPI.Startup>>
    {
        private readonly HttpClient _client;
        private readonly TestFactory<WebAPI.Startup> _factory;

        public OrderControllerTests(TestFactory<WebAPI.Startup> factory)
        {
            _factory = factory;
            _client = factory.CreateClient(new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = false
            });
        }

        [Fact]
        public async Task Add_Order_ReturnOk()
        {
            // Act
            HttpResponseMessage response;
            using (var file = File.OpenRead(@"SampleFile\order.sdf"))
            using (var content = new StreamContent(file))
            using (var formData = new MultipartFormDataContent())
            {

                formData.Add(content, "OrderFile", "order.sdf");
                response = await _client.PostAsync("/api/order/", formData);
            }          

            // Assert
            var responseString = await response.Content.ReadAsStringAsync();         
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);  
        }
    }
}
