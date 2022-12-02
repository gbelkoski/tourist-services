using Microsoft.Extensions.DependencyInjection;

namespace Tourist.Application
{
    public interface IServiceBuilder
    {
        IServiceCollection Services { get; }       
        IServiceProvider Build();
    }
}
