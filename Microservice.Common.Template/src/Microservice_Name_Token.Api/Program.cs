namespace Microservice_Name_Token.Api;

using System.Text.Json.Serialization;
using Microservice_Name_Token.Application.Services;
using Microservice_Name_Token.Domain.Entities;
using Microservice_Name_Token.Infrastructure.Extension;
using Microservice_Name_Token.Infrastructure.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

/// <summary>
/// The Program class.
/// </summary>
public class Program
{
    /// <summary>
    /// The entry point of API.
    /// </summary>
    /// <param name="args">Standard input.</param>
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder
            .Services
            .AddFusionCache();

        builder
            .Services
            .AddCustomSwagger()
            .AddCustomJwtAuthentication(builder.Configuration)
            // .AddHttpClient<IServiceClient, ServiceClient>()
            .AddTransient<IMicroservice_Name_TokenService, Microservice_Name_TokenService>()
            .AddSingleton<IApplicationBuilder, ApplicationBuilder>()
            .AddSingleton<IRepository<Entity>, MemoryRepositoryCache<Entity>>()
            .AddControllers()
            .AddJsonOptions(_ => _.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));

        var application = builder.Build();

        application
            .UseRouting()
            .UseExceptionHandler("/error")
            .UseHttpsRedirection()
            .UseSwagger()
            .UseSwaggerUI()
            .UseAuthentication()
            .UseAuthorization()
            .UseEndpoints(endpoint => endpoint.MapControllers());

        application.Run();
    }
}