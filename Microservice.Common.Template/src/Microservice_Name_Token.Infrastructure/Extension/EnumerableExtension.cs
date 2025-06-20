namespace Microservice_Name_Token.Infrastructure.Extension;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public static class EnumerableExtension
{
    public static async Task<T[]> WhenAll<T>(this IEnumerable<Task<T>> tasks) => await Task.WhenAll(tasks);

    public static IEnumerable<T> Flatten<T>(this IEnumerable<IEnumerable<T>> list) =>
        list.Aggregate(new List<T>(), (a, i) =>
        {
            a.AddRange(i);
            return a;
        });
}