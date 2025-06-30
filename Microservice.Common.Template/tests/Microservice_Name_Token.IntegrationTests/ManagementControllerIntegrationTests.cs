namespace Microservice_Name_Token.IntegrationTests
{
    using System.Net;
    using System.Threading.Tasks;
    using FluentAssertions;
    using Microsoft.AspNetCore.Mvc.Testing;
    using NUnit.Framework;

    [TestFixture]
    public class ManagementControllerIntegrationTests
    {
        private WebApplicationFactory<Api.Program>? factory;

        [SetUp]
        public void SetUp()
        {
            this.factory = new WebApplicationFactory<Api.Program>();
        }

        [TestCase("/healthz", "Healthy")]
        [TestCase("/livez", "Alive")]
        public async Task HealthAndLivenessEndpoints_ReturnExpectedResponses(string url, string expectedContent)
        {
            var client = this.factory!.CreateClient();
            var response = await client.GetAsync(url);
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var content = await response.Content.ReadAsStringAsync();
            content.Should().Be(expectedContent);
        }

        [Test]
        public async Task ErrorEndpoint_ReturnsProblem()
        {
            var client = this.factory!.CreateClient();
            var response = await client.GetAsync("/error");
            response.StatusCode.Should().Be(HttpStatusCode.InternalServerError); // 500 is the default for Problem()
        }
    }
}
