namespace Microservice_Name_Token.Domain.Entities;
using ErrorOr;

public sealed class Entity
{
    public Entity(string id)
    {
        this.id = id;
    }

    public string id { get; set; }

    public static ErrorOr<Entity> Create(string id)
    {
        return new Entity(id);
    }
}