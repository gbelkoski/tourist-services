using System.Threading.Tasks;

namespace Tourist.Application.Queries;
public interface IQueryDispatcher
{
    Task<TResult> QueryAsync<TResult>(IQuery<TResult> query);
    Task<TResult> QueryAsync<TQuery, TResult>(TQuery query) where TQuery : class, IQuery<TResult>;
    Task<TResult> QueryAsync<TResult, TQuery>(TQuery query) where TQuery : class;
}
