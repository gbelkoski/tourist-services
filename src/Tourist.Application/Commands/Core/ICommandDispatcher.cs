using System.Threading.Tasks;

namespace Tourist.Application.Commands;
public interface ICommandDispatcher
{
    Task SendAsync<T>(T command) where T : class, ICommand;
}