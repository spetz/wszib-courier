using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Courier.Core.Commands.Users;
using Courier.Core.Dto;
using FluentAssertions;
using Xunit;

namespace Courier.Tests.EndToEnd.Controllers
{
    public class AccountControllerTests : TestControllerBase
    {
        [Fact]
        public async Task account_controller_sign_up_should_succeed()
        {
            var response = await SignUpAsync(Guid.NewGuid());
            response.IsSuccessStatusCode.Should().BeTrue();
        }

        [Fact]
        public async Task account_controller_sign_in_should_succeed()
        {
            var userId = Guid.NewGuid();
            await SignUpAsync(userId);
            var jwt = await SignInAsync(userId);
            jwt.Should().NotBeNull();
            jwt.AccessToken.Should().NotBeEmpty();
        }

        [Fact]
        public async Task account_controller_get_should_return_string_content()
        {
            var userId = Guid.NewGuid();
            await SignUpAsync(userId);
            var jwt = await SignInAsync(userId);
            HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwt.AccessToken);
            var response = await HttpClient.GetAsync("me");
            var content = await response.Content.ReadAsStringAsync();
            content.Should().StartWith("Hello user with id:");
        }

        private async Task<HttpResponseMessage> SignUpAsync(Guid id)
        {
            var email = $"{id}@email.com";
            var password = "secret";
            var command = new SignUp(email, password);
            
            return await HttpClient.PostAsync("sign-up", GetPayload(command));
        }      

        private async Task<JsonWebTokenDto> SignInAsync(Guid id)
        {
            var command = new SignIn($"{id}@email.com", "secret");
            var response = await HttpClient.PostAsync("sign-in", GetPayload(command));
            response.IsSuccessStatusCode.Should().BeTrue();

            return await DeserializeAsync<JsonWebTokenDto>(response);
        }
    }
}