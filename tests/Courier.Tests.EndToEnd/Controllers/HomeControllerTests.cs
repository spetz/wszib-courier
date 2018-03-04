using System.Net.Http;
using System.Threading.Tasks;
using Courier.Api;
using FluentAssertions;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Xunit;

namespace Courier.Tests.EndToEnd.Controllers
{
    public class HomeControllerTests : TestControllerBase
    {
        [Fact]
        public async Task home_controller_get_should_return_string_content()
        {
            var response = await HttpClient.GetAsync("/");
            var content = await response.Content.ReadAsStringAsync();
            content.Should().Be("Welcome to Courier API.");
        }
    }
}