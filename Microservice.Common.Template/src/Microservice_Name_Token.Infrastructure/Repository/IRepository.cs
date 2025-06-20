namespace Microservice_Name_Token.Infrastructure.Repository;
using ErrorOr;
using System.Collections.Generic;
using System.Threading.Tasks;

public interface IRepository<T>
{
    Task<IEnumerable<T>> GetAll();
    Task<IEnumerable<T>> GetByQuery(string query);
    Task<ErrorOr<T>> Get(string id);
    Task UpsertAll(IEnumerable<T> list);
    Task Drop(string id);
}