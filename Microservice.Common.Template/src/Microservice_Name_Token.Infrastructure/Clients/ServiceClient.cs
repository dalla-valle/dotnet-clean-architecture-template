namespace Microservice_Name_Token.Infrastructure.Clients;

using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using ErrorOr;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;

public interface IServiceClient
{
    Task<ErrorOr<T>> Get<T>(string route, string id)
        where T : new();
    ServiceClient SetUpAuthorization(string bearerToken);
    Task<ErrorOr<byte[]>> Stream(string route, string id);
}

public class ServiceClient : IServiceClient
{
    private readonly HttpClient httpClient;
    private readonly IMemoryCache memoryCache;

    public ServiceClient(
        HttpClient httpClient,
        IMemoryCache memoryCache)
    {
        this.httpClient = httpClient;
        this.memoryCache = memoryCache;
    }

    public async Task<ErrorOr<T>> Get<T>(string route, string id)
        where T : new()
    {
        var cached = this.memoryCache.Get<T>($"{route}:{id}");
        if (cached is not null)
        {
            return cached;
        }

        var response = await this.httpClient.SendAsync(new HttpRequestMessage(HttpMethod.Get, $"{route}{id}"));
        if (response.StatusCode == HttpStatusCode.NotFound)
        {
            return Error.NotFound(description: $"Retrieving {route}: '{id}' is not found");
        }

        if (response.StatusCode == HttpStatusCode.BadRequest)
        {
            return Error.Validation(description: $"Retrieving {route}: '{id}' is bad request");
        }

        if (response.StatusCode != HttpStatusCode.OK)
        {
            return Error.Unexpected(description: $"Status code of {route} with CompanyId: {id} is {response.StatusCode} Content: {response.Content}");
        }

        var contentString = await response.Content.ReadAsStringAsync();
        var result = JsonConvert.DeserializeObject<T>(contentString);
        if (result is null)
        {
            return Error.Unexpected(description: $"Deserializing {route} with CompanyId: {id} returned null");
        }

        this.memoryCache.Set($"{route}:{id}", result, new TimeSpan(1, 0, 0));
        return result;
    }

    public ServiceClient SetUpAuthorization(string bearerToken)
    {
        this.httpClient.DefaultRequestHeaders.Authorization = AuthenticationHeaderValue.Parse($"Bearer {bearerToken}");
        return this;
    }

    public async Task<ErrorOr<byte[]>> Stream(string route, string id)
    {
        var response = await this.httpClient.SendAsync(new HttpRequestMessage(HttpMethod.Get, $"{route}{id}"));
        if (!response.IsSuccessStatusCode)
        {
            return Error.Unexpected(description: $"Streaming {route} {id} response: {await response.Content.ReadAsStringAsync()}");
        }

        return await response.Content.ReadAsByteArrayAsync();
    }
}