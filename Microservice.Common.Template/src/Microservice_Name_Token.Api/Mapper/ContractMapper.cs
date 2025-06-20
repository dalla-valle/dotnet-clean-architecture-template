namespace Microservice_Name_Token.Api.Mapper;

/// <summary>
/// Provides extension methods for mapping between contract and domain entities.
/// </summary>
public static class ContractMapper
{
    /// <summary>
    /// Maps a <see cref="Contracts.Microservice_Name_TokenRequest"/> to a <see cref="Domain.Entities.Entity"/>.
    /// </summary>
    /// <param name="request">The contract request to map from.</param>
    /// <returns>An Entity created from the request.</returns>
    public static Domain.Entities.Entity ToEntity(this Contracts.Microservice_Name_TokenRequest request) =>
        Domain.Entities.Entity.Create(request.Id).Value;

    /// <summary>
    /// Maps a <see cref="Domain.Entities.Entity"/> to a <see cref="Contracts.Microservice_Name_TokenResponse"/>.
    /// </summary>
    /// <param name="entity">The domain entity to map from.</param>
    /// <returns>A Microservice_Name_TokenResponse mapped from the entity.</returns>
    public static Contracts.Microservice_Name_TokenResponse ToMicroservice_Name_TokenResponse(this Domain.Entities.Entity entity) =>
        new Contracts.Microservice_Name_TokenResponse(entity.id);
}