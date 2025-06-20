namespace Microservice_Name_Token.IntegrationTests;

using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using NUnit.Framework;
using Microservice_Name_Token.Contracts;
using System;
using Microservice_Name_Token.IntegrationTests.Setup;

public class Microservice_Name_TokenControllerIntegrationTests : BaseIntegrationTest
{
    [Test]
    public async Task PostMicroservice_Name_Token_WithValidRequest_ShouldReturnCreated()
    {
        var id = Guid.NewGuid();
        var requestObj = new Microservice_Name_TokenRequest(id.ToString());
        var request = CreateRequest(HttpMethod.Post, $"Microservice_Name_Token/{id}", System.Text.Json.JsonSerializer.Serialize(requestObj));
        var response = await Client.SendAsync(request);
        response.StatusCode.Should().Be(HttpStatusCode.Created);
        var content = await response.Content.ReadFromJsonAsync<Microservice_Name_TokenResponse>();
        content.Should().NotBeNull();
        content!.Id.Should().Be(id.ToString());
    }

    [Test]
    public async Task GetMicroservice_Name_Token_WithValidId_ShouldReturnOk()
    {
        var id = Guid.NewGuid();
        var requestObj = new Microservice_Name_TokenRequest(id.ToString());
        var postRequest = CreateRequest(HttpMethod.Post, $"Microservice_Name_Token/{id}", System.Text.Json.JsonSerializer.Serialize(requestObj));
        await Client.SendAsync(postRequest);
        var getRequest = CreateRequest(HttpMethod.Get, $"Microservice_Name_Token/{id}", string.Empty);
        var response = await Client.SendAsync(getRequest);
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var content = await response.Content.ReadFromJsonAsync<Microservice_Name_TokenResponse>();
        content.Should().NotBeNull();
        content!.Id.Should().Be(id.ToString());
    }

    [Test]
    public async Task GetMicroservice_Name_Token_WithInvalidId_ShouldReturnNotFound()
    {
        var id = Guid.NewGuid();
        var request = CreateRequest(HttpMethod.Get, $"Microservice_Name_Token/{id}", string.Empty);
        var response = await Client.SendAsync(request);
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Test]
    public async Task PostMicroservice_Name_Token_WithInvalidRequest_ShouldReturnBadRequest()
    {
        var id = Guid.NewGuid();
        var request = CreateRequest(HttpMethod.Post, $"Microservice_Name_Token/{id}", "{}");
        var response = await Client.SendAsync(request);
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }
}