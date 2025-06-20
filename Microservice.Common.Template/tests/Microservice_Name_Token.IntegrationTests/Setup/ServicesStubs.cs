namespace Microservice_Name_Token.IntegrationTests.Setup
{
    using WireMock.RequestBuilders;
    using WireMock.ResponseBuilders;
    using WireMock.Server;

    public class ServicesStubs
    {
        public ServicesStubs()
        {
            this.ExternalService = WireMockServer.Start(port: 55555);
        }

        protected WireMockServer ExternalService { get; }

        public void Reset()
        {
            this.ExternalService.Reset();
        }

        public void Stop()
        {
            this.ExternalService.Stop();
        }

        public void StubCompanyService(int statusCode, Guid guid, string responseBody)
        {
            this.ExternalService
               .Given(
                    Request.Create()
                        .WithPath($"/route/{guid}")
                        .UsingGet())
               .RespondWith(
                    Response.Create()
                        .WithStatusCode(statusCode)
                        .WithHeader("Content-Type", "application/json")
                        .WithBody(responseBody));
        }
    }
}