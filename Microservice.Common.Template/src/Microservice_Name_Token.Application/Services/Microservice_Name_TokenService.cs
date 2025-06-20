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
    Task<IEnumerable<Entity>> GetAllMicroservice_Name_Token();
    Task<IEnumerable<Entity>> UpsertAllMicroservice_Name_Token(IEnumerable<Entity> entities);
    Task<ErrorOr<bool>> DropMicroservice_Name_Token(Guid id);
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

    public async Task<IEnumerable<Entity>> GetAllMicroservice_Name_Token()
    {
        return await this.repository.GetAll();
    }

    public async Task<IEnumerable<Entity>> UpsertAllMicroservice_Name_Token(IEnumerable<Entity> entities)
    {
        await this.repository.UpsertAll(entities);
        return entities;
    }

    public async Task<ErrorOr<bool>> DropMicroservice_Name_Token(Guid id)
    {
        var entity = await this.repository.Get(id.ToString());
        if (entity.IsError)
        {
            return entity.Errors;
        }

        await this.repository.Drop(id.ToString());
        return true;
    }
}