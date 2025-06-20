namespace Microservice_Name_Token.Application.Services;
using Microservice_Name_Token.Contracts;
using Microservice_Name_Token.Domain.Entities;
using Microservice_Name_Token.Infrastructure.Repository;
using ErrorOr;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;

public interface IMicroservice_Name_TokenService
{
    Task<ErrorOr<Entity>> CreateNewMicroservice_Name_Token(Microservice_Name_TokenRequest application);
    Task<ErrorOr<Entity>> GetMicroservice_Name_Token(Guid id);
}

public class Microservice_Name_TokenService : IMicroservice_Name_TokenService
{
    private readonly IRepository<Entity> repository;

    public Microservice_Name_TokenService(IRepository<Entity> repository)
    {
        this.repository = repository;
    }

    public async Task<ErrorOr<Entity>> CreateNewMicroservice_Name_Token(Microservice_Name_TokenRequest request)
    {
        var created = Entity.Create(request.Id);

        if (created.IsError)
        {
            return created.Errors;
        }

        await this.repository.UpsertAll(new List<Entity> { created.Value });

        return created.Value;
    }

    public Task<ErrorOr<Entity>> GetMicroservice_Name_Token(Guid id) =>
        this.repository.Get(id.ToString());
}