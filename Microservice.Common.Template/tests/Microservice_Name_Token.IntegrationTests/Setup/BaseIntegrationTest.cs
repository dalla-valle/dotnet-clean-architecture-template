namespace Microservice_Name_Token.IntegrationTests.Setup;

using System.Net.Http.Headers;
using System.Text;
using NUnit.Framework;

public class BaseIntegrationTest
{
    private static TestServer testServerInstance = new TestServer();
    protected static HttpClient Client => testServerInstance.CreateClient();
    protected ServicesStubs ServicesStubs { get; private set; } = null!;

    [OneTimeSetUp]
    public void GlobalSetup()
    {
        this.ServicesStubs = new ServicesStubs();
    }

    [SetUp]
    public void SetUp()
    {
    }

    [TearDown]
    public void Teardown()
    {
        this.ServicesStubs.Reset();
    }

    [OneTimeTearDown]
    public void GlobalTeardown()
    {
        this.ServicesStubs.Stop();
    }

    protected static HttpRequestMessage CreateRequest(HttpMethod method, string path, string body, Dictionary<string, string>? header = null)
    {
        var request = new HttpRequestMessage()
        {
            RequestUri = new Uri($"https://localhost/{path}"),
            Method = method,
            Content = new StringContent(body, Encoding.UTF8, "application/json"),
        };
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", "test-token");
        if (header is not null)
        {
            foreach (var item in header)
            {
                request.Headers.Add(item.Key, item.Value);
            }
        }

        return request;
    }
}