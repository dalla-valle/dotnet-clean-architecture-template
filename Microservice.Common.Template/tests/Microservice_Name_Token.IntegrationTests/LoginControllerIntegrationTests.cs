namespace Microservice_Name_Token.IntegrationTests
{
    using System.Net;
    using System.Net.Http;
    using System.Text;
    using System.Threading.Tasks;
    using FluentAssertions;
    using Microsoft.AspNetCore.Mvc.Testing;
    using NUnit.Framework;
    using Newtonsoft.Json;

    [TestFixture]
    public class LoginControllerIntegrationTests
    {
        private WebApplicationFactory<Api.Program>? factory;

        [SetUp]
        public void SetUp()
        {
            this.factory = new WebApplicationFactory<Api.Program>();
        }

        [Test]
        public async Task Login_WithValidCredentials_ReturnsJwtToken()
        {
            var client = this.factory!.CreateClient();
            var request = new { Username = "admin", Password = "password" };
            var content = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
            var response = await client.PostAsync("/auth/login", content);
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var responseBody = await response.Content.ReadAsStringAsync();
            responseBody.Should().Contain("token");
        }

        [Test]
        public async Task Login_WithInvalidCredentials_ReturnsUnauthorized()
        {
            var client = this.factory!.CreateClient();
            var request = new { Username = "user", Password = "wrongpassword" };
            var content = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
            var response = await client.PostAsync("/auth/login", content);
            response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        }
    }
}
