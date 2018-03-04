using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Courier.Api;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Newtonsoft.Json;

namespace Courier.Tests.EndToEnd.Controllers
{
    public abstract class TestControllerBase
    {
        protected readonly TestServer Server;
        protected readonly HttpClient HttpClient;

        public TestControllerBase()
        {
            Server = new TestServer(WebHost.CreateDefaultBuilder()
                .UseStartup<Startup>());
            HttpClient = Server.CreateClient();
        }

        protected async Task<T> DeserializeAsync<T>(HttpResponseMessage response)
        {
            var content = await response.Content.ReadAsStringAsync();
            
            return JsonConvert.DeserializeObject<T>(content);            
        }

        protected StringContent GetPayload(object data)
            => new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
    }
}